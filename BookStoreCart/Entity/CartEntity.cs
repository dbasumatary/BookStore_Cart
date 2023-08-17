using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreCart.Entity
{
    public class CartEntity
    {
        [Key]
        public string CartId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public float Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [NotMapped]
        public UserEntity User { get; set; }
        [NotMapped]
        public BookEntity Book { get; set; }
    }
}
