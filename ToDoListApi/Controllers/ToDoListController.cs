using Application.Commands;
using Application.Dto.Commands;
using Application.Dto.Query;
using Application.Dto.Request;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetToDoListsByUserIdDto>>> GetToDoListsByUserId()
        {
            var lists = await _mediator.Send(new GetToDoListsByUserIdQuery());
            return Ok(lists);
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<ActionResult<GetToDoListByIdDto>> GetToDoListById(int id)
        {
            var list = await _mediator.Send(new GetToDoListByIdQuery(id));
            return Ok(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateNewToDoList(CreateNewToDoListDto newList)
        {
            await _mediator.Send(new CreateNewToDoListCommand(newList));
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateToDoList(UpdateToDoListDto updatedList)
        {
            await _mediator.Send(new UpdateToDoListCommand(updatedList));
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteToDoList(int id)
        {
            await _mediator.Send(new DeleteToDoListCommand(id));
            return Ok();
        }
    }
}
