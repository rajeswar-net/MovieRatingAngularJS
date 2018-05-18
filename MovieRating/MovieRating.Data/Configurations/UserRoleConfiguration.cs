﻿namespace MovieRating.Data.Configurations
{
    using Entities;

    public class UserRoleConfiguration:EntityBaseConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(ur => ur.UserId).IsRequired();
            Property(ur => ur.RoleId).IsRequired();
        }
    }
}
