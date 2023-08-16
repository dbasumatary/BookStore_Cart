using BookStoreCart.Entity;
using System.Threading.Tasks;

namespace BookStoreCart.Interface
{
    public interface IUserService
    {
        Task<UserEntity> GetUser(string jwtToken);
    }
}
