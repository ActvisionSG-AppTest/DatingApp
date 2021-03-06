using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Persistence.IRepository
{
    public interface IAuthRepository
    {

        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> IsRegistered(string username);


    }
}