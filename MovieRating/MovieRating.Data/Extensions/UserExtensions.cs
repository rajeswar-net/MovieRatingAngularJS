namespace MovieRating.Data.Extensions
{
    using Entities;
    using Repositories;
    using System.Linq;

    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository,string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.UserName == username);
        }
    }
}
