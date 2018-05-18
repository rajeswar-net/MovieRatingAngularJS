namespace MovieRating.Services.Abstract
{
    using Entities;
    using System.Collections.Generic;

    interface IMembershipService
    {
        MembershipContext ValidateUser(string userName, string password);
        User CreateUser(string userName, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string userName);
    }
}
