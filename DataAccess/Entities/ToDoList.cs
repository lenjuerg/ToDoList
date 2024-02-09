using DataAccess.Entities;

namespace DataAccess.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public ICollection<ListTask> Tasks { get; set; } = new List<ListTask>();
    }

    public enum Status
    {
        Open,
        OnHold,
        Archived
    }
}
