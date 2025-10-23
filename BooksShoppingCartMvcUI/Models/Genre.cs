using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksShoppingCartMvcUI.Models
{
    [Table("Genre")]
    public class Genre
    {
        public int id { get; set; }
        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
    }
}
