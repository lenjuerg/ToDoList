using Application.Commands;
using Application.Dto.Commands;
using Application.Dto.Query;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("listId")]
        [Authorize]
        public async Task<ActionResult<List<GetListTaskDto>>> GetListTasksByListId(int listId)
        {
            return Ok(await _mediator.Send(new GetListTasksByListIdQuery(listId)));
        }

        [HttpGet("taskId")]
        [Authorize]
        public async Task<ActionResult<GetListTaskDto>> GetListTaskById(int taskId)
        {
            return Ok(await _mediator.Send(new GetListTaskByIdQuery(taskId)));
        }

        [HttpGet("dueInHours")]
        [Authorize]
        public async Task<ActionResult<List<GetListTaskDto>>> GetListTasksByDueInTimeSpan(int dueInHours)
        {
            return Ok(await _mediator.Send(new GetListTasksByDueInTimeSpanQuery(dueInHours)));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddNewListTask(AddNewListTaskDto newTask)
        {
            await _mediator.Send(new AddNewListTaskCommand(newTask));
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateListTask(UpdateListTaskDto update)
        {
            await _mediator.Send(new UpdateListTaskCommand(update));
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteListTask(int id)
        {
            await _mediator.Send(new DeleteListTaskCommand(id));
            return Ok();
        }
    }
}
