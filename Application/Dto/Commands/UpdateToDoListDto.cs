using Models;

namespace Application.Dto.Commands
{
    public class UpdateToDoListDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
