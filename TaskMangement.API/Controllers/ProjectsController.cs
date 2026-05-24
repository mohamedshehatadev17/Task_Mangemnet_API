using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangement.Application.Features.Project.Commands.CreateProject;
using TaskMangement.Application.Features.Project.Commands.DeleteProject;
using TaskMangement.Application.Features.Project.Commands.UpdateProject;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;
using TaskMangement.Application.Features.Project.Queries.GetProjectById;

namespace TaskMangement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(
                new GetAllProjectsQuery { page = page, pageSize = pageSize });

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand { Id = id });
            return Ok(result);
        }
    }
}
