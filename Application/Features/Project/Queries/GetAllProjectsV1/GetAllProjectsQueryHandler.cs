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

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjectsV1
{
    public class GetAllProjectsQueryHandler
         : IRequestHandler<GetAllProjectsQueryV1, Result<IEnumerable<ProjectResponse>>>
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

        public async Task<Result<IEnumerable<ProjectResponse>>> Handle(GetAllProjectsQueryV1 request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(page: request.page, pageSize: request.pageSize, x => x.IsDeleted == false, cancellationToken);
            if (result == null || !result.Any())
                return Result<IEnumerable<ProjectResponse>>.Failure("No projects found.");

            return Result<IEnumerable<ProjectResponse>>.Success(result.Adapt<IEnumerable<ProjectResponse>>());
        }
    }
}
