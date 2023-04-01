using System.ComponentModel.DataAnnotations;

namespace MyLibraryAPI
{
    public class CreateAuthorsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int PublisherId { get; set; }
    }
}