namespace LibraryAPi
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public Decimal Price { get; set; } = 00;
        public List<Author> Authors { get; set; }
    }
}
