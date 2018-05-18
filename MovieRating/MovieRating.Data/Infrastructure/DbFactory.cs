namespace MovieRating.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        MovieRatingContext dbContext;
        public MovieRatingContext Init()
        {
            return dbContext ?? (dbContext = new MovieRatingContext());
        }
        public override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
