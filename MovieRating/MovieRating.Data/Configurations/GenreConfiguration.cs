namespace MovieRating.Data.Configurations
{
    using Entities;

    public class GenreConfiguration : EntityBaseConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(50);
    }
    }
}
