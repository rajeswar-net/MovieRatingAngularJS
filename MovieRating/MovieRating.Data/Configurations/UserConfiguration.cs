namespace MovieRating.Data.Configurations
{
    using Entities;

    public class UserConfiguration:EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.UserName).IsRequired().HasMaxLength(100);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.IsLocked).IsRequired();
            Property(u => u.DateCreated);
        }
    }
}
