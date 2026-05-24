using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.configurations;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;
using TaskMangement.Domain.Models;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjectsV2
{
    public class GetAllProjectsQueryHandler
         : IRequestHandler<GetAllProjectsQueryV2, Result<IEnumerable<ProjectTasksResponse>>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<IEnumerable<ProjectTasksResponse>>> Handle(GetAllProjectsQueryV2 request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetProjectsWithTasks(cancellationToken);
            if (result == null || !result.Any())
                return Result<IEnumerable<ProjectTasksResponse>>.Failure("No projects found.");
            return Result<IEnumerable<ProjectTasksResponse>>.Success(result.Adapt<IEnumerable<ProjectTasksResponse>>());
        }
    }
}
