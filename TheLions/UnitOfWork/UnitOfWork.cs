using System.ComponentModel.DataAnnotations;
using TheLions.Repositories;
using TheLions.Repositories.IRepositories;

namespace TheLions.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);

        }
        public IUserRepository UserRepository { get; set; }
        public AppDbContext GetDbContext()
    {
        return _context;
    }
        public async Task<int> CompleteAsync()
        {
            try
            {
                var t= _context.SaveChanges();
                return t;
                var result= await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
