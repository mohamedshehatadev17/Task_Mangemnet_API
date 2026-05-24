using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<Guid>> Handle(CreateProjectCommand request,CancellationToken cancellationToken)
        {
            var validationResult = new CreateProjectCommandValidator().Validate(request);
            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors.First().ErrorMessage);
            var existingProject = await _projectRepository.GetByIdAsync(request.UserId);
            if (existingProject != null)
                return Result<Guid>.Failure("A project with the same name already exists for this user.");
            var project = new Domain.Models.Project 
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return Result<Guid>.Success(project.Id);
        }
    }
}
