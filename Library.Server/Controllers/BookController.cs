using Biblioteca.Server.DatabaseContext;
using Biblioteca.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Biblioteca.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly MyDatabaseContext _dbcontext;
        public BookController(MyDatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert(Book obj)
        {
            try
            {
                BookDataModel book = new BookDataModel();
                book.Title = obj.Title;
                book.Author = obj.Author;
                book.Copies = obj.Copies;

                await _dbcontext.Books.AddAsync(book);
                await _dbcontext.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                BookDataModel book = _dbcontext.Books.Where(t => t.Id == id).FirstOrDefault();
                _dbcontext.Books.Remove(book);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "El libro '" + book.Title + "' fue eliminado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }

        }

        [HttpGet]
        [Route("Lent/{id:int}")]
        public async Task<IActionResult> Lent(int id)
        {
            try
            {
                BookDataModel book = _dbcontext.Books.Where(t => t.Id == id).FirstOrDefault();

                if(book.Copies >= 1) book.Copies = book.Copies - 1;
                else throw new Exception(message: "El libro " + book.Title + " no tiene copias que puedan ser prestadas");

                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "El libro '" + book.Title + "' fue prestado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("Return/{id:int}")]
        public async Task<IActionResult> Return(int id)
        {
            try
            {
                BookDataModel book = _dbcontext.Books.Where(t => t.Id == id).FirstOrDefault();
                book.Copies = book.Copies + 1;
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "El libro '" + book.Title + "' fue devuelto");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(_dbcontext.Books.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }
    }
}
