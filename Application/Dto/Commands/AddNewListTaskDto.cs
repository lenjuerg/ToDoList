namespace Application.Dto.Commands
{
    public record AddNewListTaskDto
    {
        public int ToDoListId { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset DueDateTime { get; set; }
        public bool Finished { get; set; }
    }
}
