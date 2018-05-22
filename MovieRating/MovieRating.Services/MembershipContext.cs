using MovieRating.Entities;
using System.Security.Principal;

namespace MovieRating.Services
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }

        public bool IsValid()
        {
            return Principal != null;
        }
    }
}