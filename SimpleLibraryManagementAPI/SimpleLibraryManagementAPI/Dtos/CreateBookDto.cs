namespace SimpleLibraryManagementAPI.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
