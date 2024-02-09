using DataAccess.Models;

namespace DataAccess.Entities
{
    public class ListTask
    {
        public int Id { get; set; }
        public int ToDoListId { get; set; }
        public ToDoList ToDoList { get; set; } = null!;
        public required string Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public TimeOnly? DueTime {  get; set; }
        public bool Finished { get; set; }
    }
}
