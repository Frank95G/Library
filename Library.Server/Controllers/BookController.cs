using Biblioteca.Server.DatabaseContext;
using Biblioteca.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly MyDatabaseContext _dbcontext;
        public BookController(MyDatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetBooksList")]
        public async Task<IActionResult> GetProductList()
        {
            return Ok(_dbcontext.Books.ToList());
        }

        [HttpPost]
        [Route("PostProduct")]
        public async Task<IActionResult> PostBook(Book obj)
        {
            BookDataModel book = new BookDataModel();
            book.Id = Guid.NewGuid();
            book.Title = obj.Title;
            book.Author = obj.Author;
            book.Status = 1;

            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();

            return Ok(book);
        }
    }
}
