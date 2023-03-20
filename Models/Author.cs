using static System.Reflection.Metadata.BlobBuilder;

namespace task_three.Models
{
    public class Authors
    {
        public int Id { get; set; } 
        public string Names_Of_Author { get; set; } = string.Empty;
        public string State_Of_Origin { get; set; } = string.Empty;
        public ICollection<Books> Books { get; set; } = new List<Books>();
        public int publisherId { get; set; }
    }
}
