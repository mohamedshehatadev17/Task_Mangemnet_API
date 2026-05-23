using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangement.Application.Features.Project.Commands.CreateProject;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;

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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(
                new GetAllProjectsQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
