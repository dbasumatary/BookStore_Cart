using BookStoreCart.Context;
using BookStoreCart.Entity;
using BookStoreCart.Interface;

namespace BookStoreCart.Service
{
    public class CartService : ICartService
    {
        public readonly CartContext _context;
        public readonly IBookService _book;
        public readonly IUserService _user;
        public CartService(CartContext context, IBookService book, IUserService user)
        {
            _context = context;
            _book = book;
            _user = user;

        }

        public async Task<CartEntity> AddToCart(string token, int userId, int bookId, int cartQuantity)
        {
            CartEntity newCart = new CartEntity()
            {
                CartId = Guid.NewGuid().ToString(),
                BookId = bookId,
                UserId = userId,
                //BookName = bookName,
                Quantity = cartQuantity,
                Book = await _book.GetBookById(bookId),
                User = await _user.GetUser(token)
            };
            _context.Cart.Add(newCart);
            _context.SaveChanges();
            return newCart;
        }


        

        public bool DeleteFromCart(int bookId)
        {
            CartEntity cart = _context.Cart.FirstOrDefault(x => x.BookId == bookId);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
                _context.SaveChanges();
                return true;
            }
            else
            { return false; }
        }

        

        public IEnumerable<CartEntity> GetCartDetails()
        {
            IEnumerable<CartEntity> cart = _context.Cart;
            return cart;
        }

        

        public CartEntity UpdateCart(int bookId, CartEntity updatedCart)
        {
            CartEntity cart = _context.Cart.FirstOrDefault(x => x.BookId == bookId);
            if (cart != null)
            {
                cart.CartId = updatedCart.CartId;
                cart.UserId = updatedCart.UserId;
                cart.BookId = updatedCart.BookId;
                //cart.BookName = updatedCart.BookName;
                cart.Quantity = updatedCart.Quantity;
                
                _context.Cart.Update(cart);
                _context.SaveChanges();
            }
            return cart;
        }
    }
}
