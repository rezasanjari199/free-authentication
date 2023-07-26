using TheLions.Models.User;
using TheLions.Repositories.IRepositories;

namespace TheLions.Repositories
{
    public class UserRepository:Repository<User> ,IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
