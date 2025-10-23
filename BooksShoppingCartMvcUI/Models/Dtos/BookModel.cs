namespace BooksShoppingCartMvcUI.Models.Dtos
{
    public class BookModel
    {
      public  IEnumerable<Book> Books { get; set; } 
       public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    }
}
