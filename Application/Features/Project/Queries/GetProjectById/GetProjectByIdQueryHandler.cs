using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler
          : IRequestHandler<GetProjectByIdQuery, Result<ProjectResponse>?>
    {
        private readonly IGenericRepository<Domain.Models.Project> _repository;
        private readonly IMapper _mapper;
        public GetProjectByIdQueryHandler(IGenericRepository<Domain.Models.Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ProjectResponse>?> Handle(GetProjectByIdQuery request,CancellationToken cancellationToken)
        {
            var validationResult = new GetProjectQueryValidator().Validate(request);
            if (!validationResult.IsValid)
                return Result<ProjectResponse>.Failure(validationResult.Errors.First().ErrorMessage);
            var project = await _repository.GetByIdAsync(request.Id,x=>x.IsDeleted==false);
            if (project == null)
                return Result<ProjectResponse>.Failure("Project not found.");
            return Result<ProjectResponse>.Success(_mapper.Map<ProjectResponse>(project));
        }
    }
}
