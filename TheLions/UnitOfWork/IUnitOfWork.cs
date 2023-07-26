using TheLions.Repositories.IRepositories;

namespace TheLions.UnitOfWork
{
    public interface IUnitOfWork
    {
        AppDbContext GetDbContext();
        Task<int> CompleteAsync();
        void Dispose();
        IUserRepository UserRepository { get; }
    }
}
