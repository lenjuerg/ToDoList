namespace Application.Dto.Request
{
    public class CreateNewToDoListDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
