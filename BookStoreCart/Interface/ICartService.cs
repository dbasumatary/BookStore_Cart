using BookStoreCart.Entity;

namespace BookStoreCart.Interface
{
    public interface ICartService
    {
        Task<CartEntity> AddToCart( string token, int userId, int bookId, int cartQuantity);
        CartEntity UpdateCart(int bookId, CartEntity updatedCart);
        bool DeleteFromCart(int bookId);
        IEnumerable<CartEntity> GetCartDetails();
    }
}
