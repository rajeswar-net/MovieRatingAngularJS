namespace MovieRating.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private MovieRatingContext dbContext;
        private readonly IDbFactory dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public MovieRatingContext DbContext { get { return dbContext ?? (dbContext = new MovieRatingContext()); } }
        public void Commit()
        {
            dbContext.Commit();
        }
    }
}
