using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMangement.Application.Features.Project.Commands.CreateProject;
using TaskMangement.Application.Features.Project.Commands.DeleteProject;
using TaskMangement.Application.Features.Project.Commands.UpdateProject;
using TaskMangement.Application.Features.Project.Queries.GetAllProjectsV1;
using TaskMangement.Application.Features.Project.Queries.GetAllProjectsV2;
using TaskMangement.Application.Features.Project.Queries.GetProjectById;

namespace TaskMangement.API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllV1(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllProjectsQueryV1 { page = page, pageSize = pageSize });

            return Ok(result);
        }
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAllV2()
        {
            var result = await _mediator.Send(new GetAllProjectsQueryV2());

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdV1(Guid id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
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
