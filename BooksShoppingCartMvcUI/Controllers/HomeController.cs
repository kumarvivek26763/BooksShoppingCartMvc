using System.Diagnostics;
using BooksShoppingCartMvcUI.Models;
using BooksShoppingCartMvcUI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BooksShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository=homeRepository;
        }


        public async Task<IActionResult> Index(string strm="", int  genreId=0)
        {
            IEnumerable<Book> books = await _homeRepository.GetBooks(strm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            BookModel bookModel = new BookModel
            {
                Books = books,
                Genres= genres,
                STerm= strm,
                GenreId = genreId


            };
           
            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
