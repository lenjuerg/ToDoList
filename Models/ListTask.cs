namespace Models
{
    public class ListTask
    {
        public int Id { get; set; }
        public int ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; } = null!;
        public required string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public bool Finished { get; set; }
    }
}
