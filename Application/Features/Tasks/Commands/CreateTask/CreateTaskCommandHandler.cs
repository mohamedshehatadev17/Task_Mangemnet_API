using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapster;
using MapsterMapper;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<Guid>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public CreateTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
        }
        public async Task<Result<Guid>> Handle(CreateTaskCommand request,CancellationToken cancellationToken)
        {
            var validationResult = new CreateTaskCommandValidator().Validate(request);
            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors.First().ErrorMessage);
            var isExist = _projectRepository.IsExist(request.ProjectId);
            if (!isExist)
                return Result<Guid>.Failure("this project not found");
            Domain.Models.Task task = request.Adapt<Domain.Models.Task>();
            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return Result<Guid>.Success(task.Id);
        }
    }
}