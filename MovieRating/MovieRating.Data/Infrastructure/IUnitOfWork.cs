namespace MovieRating.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
