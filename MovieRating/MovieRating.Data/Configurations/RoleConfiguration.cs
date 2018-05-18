namespace MovieRating.Data.Configurations
{
    using Entities;
    public class RoleConfiguration:EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(ur => ur.Name).IsRequired().HasMaxLength(50);
        }
    }
}
