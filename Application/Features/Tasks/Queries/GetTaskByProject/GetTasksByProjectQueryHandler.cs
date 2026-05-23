using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;
using TaskMangement.Domain.Models;

namespace TaskMangement.Application.Features.Tasks.Queries.GetTaskByProject
{
    public class GetTasksByProjectQueryHandler: IRequestHandler<GetTasksByProjectQuery,Result<ProjectTasksResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetTasksByProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<ProjectTasksResponse>> Handle(GetTasksByProjectQuery request,CancellationToken cancellationToken)
        {
            var validationResult = new GetTasksByProjectIdQueryValidator()
                .Validate(request);

            if (!validationResult.IsValid)
                return Result<ProjectTasksResponse>
                    .Failure(validationResult.Errors.First().ErrorMessage);

            var project = await _projectRepository
                .GetProjectWithTask(request.ProjectId, cancellationToken);

            if (project is null)
                return Result<ProjectTasksResponse>
                    .Failure("Project not found");

            var response = project.Adapt<ProjectTasksResponse>();

            return Result<ProjectTasksResponse>.Success(response);
        }
    }
}
