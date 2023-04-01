using System.ComponentModel.DataAnnotations;

namespace MyLibraryAPI
{
    public class CreateBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public Decimal Price { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
