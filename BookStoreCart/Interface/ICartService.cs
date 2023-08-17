using BookStoreCart.Entity;

namespace BookStoreCart.Interface
{
    public interface ICartService
    {
        Task<CartEntity> AddToCart( string token, int userId, int bookId, int cartQuantity);
        CartEntity UpdateCart(int bookId, CartEntity updatedCart);
        CartEntity UpdateCartQuantity(int bookId, int newQuantity);
        bool DeleteFromCart(int bookId);
        IEnumerable<CartEntity> GetCartDetails();
        float CalculateTotalPrice(int userId);
    }
}
