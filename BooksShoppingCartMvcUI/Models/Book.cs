using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksShoppingCartMvcUI.Models
{
    [Table("book")]
    public class Book
    {

        public int id {  get; set; }
        [Required]
        [MaxLength(40)]
        public string? BookName { get; set; }
        [Required]
        [MaxLength(40)]
        public string? AuthorName { get; set; }
        [Required]
        public double Price { get; set; }

        public string? Image {  get; set; }
        [Required]      
        
        public int GenreId { get; set; }
        public Genre Genre {  get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetails> CartDetails { get; set; }

        [NotMapped]
        public string GenreName { get; set; }


    }
}
