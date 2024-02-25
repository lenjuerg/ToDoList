namespace Application.Dto.Commands
{
    public record UpdateListTaskDto
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset DueDateTime { get; set; }
        public bool Finished { get; set; }
    }
}
