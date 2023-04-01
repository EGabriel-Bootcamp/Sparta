namespace MyLibraryAPI
{
    public class AuthorsBook
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }     
    }
}
