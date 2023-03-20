namespace task_three.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Names_Of_Book { get; set; }
        public DateTime Date_Of_Production { get; set; }

        public int AuthorId { get; set; }
        public Authors Author { get; set; }





    }
}
