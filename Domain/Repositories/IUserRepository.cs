
namespace Domain.Repositories
{
    public interface IUserRepository
    {
        bool Login(string email, string password);
    }
}
