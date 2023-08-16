using BookStoreCart.Entity;

namespace BookStoreCart.Interface
{
    public interface IBookService
    {
        Task<BookEntity> GetBookById(int id);
    }
}
