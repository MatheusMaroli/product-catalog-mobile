using Domain.Entities;
using Domain.Infra.Contexts;
using Domain.Repositories;
using System.Linq;

namespace Domain.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ProductCatalogContext context) : base(context)
        {
        }

        public bool Login(string email, string password)
        {
            return _context.Users.Any(f => f.Email == email && f.Password == password);
        }
    }
}
