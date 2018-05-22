namespace MovieRating.Services
{
    using Abstract;
    using System;
    using System.Collections.Generic;
    using Entities;
    using Data.Repositories;
    using Data.Infrastructure;
    using Data.Extensions;
    using System.Linq;
    using System.Security.Principal;

    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public MembershipService(IEntityBaseRepository<User> userRepository,
            IEntityBaseRepository<Role> roleRepository,
            IEntityBaseRepository<UserRole> userRoleRepository,
            IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _encryptionService = encryptionService;
            _unitOfWork = unitOfWork;
        }
        public User CreateUser(string userName, string email, string password, int[] roles)
        {
            var existingUser = _userRepository.GetSingleByUsername(userName);
            if (existingUser != null)
            {
                throw new Exception("Username is already in use!");
            }
            var passwordSalt = _encryptionService.CreateSalt();
            var user = new User()
            {
                UserName = userName,
                Email = email,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                IsLocked = false,
                DateCreated = DateTime.Now
            };
            _userRepository.Add(user);
            _unitOfWork.Commit();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }
            _unitOfWork.Commit();
            return user;
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRoles(string userName)
        {
            List<Role> _result = new List<Role>();
            var existingUser = _userRepository.GetSingleByUsername(userName);
            return existingUser != null ? existingUser.UserRoles.Select(x => x.Role).Distinct().ToList<Role>() : new List<Role>();
        }

        public MembershipContext ValidateUser(string userName, string password)
        {
            var membershipCtx = new MembershipContext();
            var user = _userRepository.GetSingleByUsername(userName);
            if(user!=null && isUserValid(user,password))
            {
                var userRoles = GetUserRoles(user.UserName);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.UserName);
                membershipCtx.Principal = new GenericPrincipal(identity, userRoles.Select(x => x.Name).ToArray());
            }
            return membershipCtx;
        }

        private void addUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist");

            var userRole = new UserRole()
            {
                RoleId = roleId,
                UserId = user.ID
            };
            _userRoleRepository.Add(userRole);
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }
    }
}
