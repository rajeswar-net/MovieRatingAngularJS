namespace MovieRating.Data.Infrastructure
{
    using System;

    public interface IDbFactory:IDisposable
    {
        MovieRatingContext Init();
    }
}
