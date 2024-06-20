using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Server.Models
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Copies { get; set; }

    }
}
