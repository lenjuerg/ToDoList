namespace Application.Dto.Query
{
    public class GetListTaskDto
    {
        public required int Id { get; set; }

        public required string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public TimeOnly DueTime { get; set; }
        public bool Finished { get; set; }
    }
}
