using System.Text.Json.Serialization;

namespace MyLibraryAPI
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        public List<Book> Books { get; set; }

    }
}
