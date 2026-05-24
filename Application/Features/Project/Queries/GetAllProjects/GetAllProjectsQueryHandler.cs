using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.configurations;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler
         : IRequestHandler<GetAllProjectsQuery, Result<IEnumerable<ProjectResponse>>>
    {
        private readonly IGenericRepository<Domain.Models.Project> _repository;
        private readonly IMapper _mapper;
        public GetAllProjectsQueryHandler(
            IGenericRepository<Domain.Models.Project> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProjectResponse>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(page: 1, pageSize: 10, x => x.IsDeleted == false, cancellationToken);
            if (result == null || !result.Any())
                return Result<IEnumerable<ProjectResponse>>.Failure("No projects found.");

            return Result<IEnumerable<ProjectResponse>>.Success(result.Adapt<IEnumerable<ProjectResponse>>());
        }
    }
}
