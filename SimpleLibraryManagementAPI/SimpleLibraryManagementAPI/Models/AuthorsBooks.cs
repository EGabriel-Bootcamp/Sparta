namespace SimpleLibraryManagementAPI.Models
{
    public class AuthorsBooks
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
    }
}