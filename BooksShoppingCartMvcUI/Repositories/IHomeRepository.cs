

public interface IHomeRepository
{
    Task<IEnumerable<Genre>> Genres();
    Task<IEnumerable<Book>> GetBooks(string sTerm = " ", int genreId = 0);
}