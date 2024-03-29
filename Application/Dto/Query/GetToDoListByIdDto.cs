﻿using Models;

namespace Application.Dto.Query
{
    public class GetToDoListByIdDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public List<GetListTaskDto>? Tasks { get; set; }
    }
}
