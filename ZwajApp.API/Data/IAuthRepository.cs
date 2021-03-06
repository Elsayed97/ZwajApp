using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user , string password);
         Task<User> Login(string userName , string password);
         Task<bool> isUserExists(string userName);
    }
}