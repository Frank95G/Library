using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Server.Models
{
    public class BookDataModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
