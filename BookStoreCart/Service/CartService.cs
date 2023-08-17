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

        /*public async Task<CartEntity> AddToCart(string token, int userId, int bookId, int cartQuantity)
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
            //newCart.Book.DiscountedPrice = _context.Add();
            _context.Cart.Add(newCart);
            _context.SaveChanges();
            return newCart;
        }*/

        public async Task<CartEntity> AddToCart(string token, int userId, int bookId, int cartQuantity)
        {
            BookEntity book = await _book.GetBookById(bookId); // Retrieving the book details
            UserEntity user = await _user.GetUser(token); // Retrieving the user details

            if (book != null && user != null)
            {
                CartEntity newCart = new CartEntity()
                {
                    CartId = Guid.NewGuid().ToString(),
                    BookId = bookId,
                    UserId = userId,
                    BookName = book.BookName, 
                    Price = book.DiscountedPrice,
                    Quantity = cartQuantity,
                    Book = book,
                    User = user
                };

                _context.Cart.Add(newCart);
                _context.SaveChanges();

                return newCart;
            }

            return null; 
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

        public IEnumerable<CartEntity> GetCartDetailsById(int id)
        {
            IEnumerable<CartEntity> cart = _context.Cart.Where(x => x.UserId == id);
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

        public CartEntity UpdateCartQuantity(int bookId, int newQuantity)
        {
            CartEntity cart = _context.Cart.FirstOrDefault(x => x.BookId == bookId);

            if (cart != null)
            {
                cart.Quantity = newQuantity;
                _context.Cart.Update(cart);
                _context.SaveChanges();
            }

            return cart;
        }

        public float CalculateTotalPrice(int userId)
        {
            float totalPrice = 0;

            var cartItems = _context.Cart.Where(c => c.UserId == userId).ToList();

            foreach (var cartItem in cartItems)
            {
                var book = _book.GetBookById(cartItem.BookId).Result; // 
                if (book != null)
                {
                    float bookPrice = cartItem.Quantity * book.DiscountedPrice;
                    totalPrice += bookPrice;
                }
            }

            return totalPrice;
        }

    }
}
