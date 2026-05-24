using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMangement.Application.Features.Tasks.Commands.CreateTask;
using TaskMangement.Application.Features.Tasks.Commands.DeleteTask;
using TaskMangement.Application.Features.Tasks.Commands.UpateTask;
using TaskMangement.Application.Features.Tasks.Queries.GetTaskByProject;

namespace TaskMangement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(UpdateTaskStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(
            Guid projectId)
        {
            var result = await _mediator.Send(
                new GetTasksByProjectQuery
                {
                    ProjectId = projectId
                });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(
                new DeleteTaskCommand
                {
                    Id = id
                });

            return Ok(result);
        }
    }
}