using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Features.Project.Queries.GetAllProjects;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandler: IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public CreateProjectCommandHandler(IProjectRepository projectRepository, IHttpContextAccessor contextAccessor)
        {
            _projectRepository = projectRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<Guid>> Handle(CreateProjectCommand request,CancellationToken cancellationToken)
        {
            var validationResult = new CreateProjectCommandValidator().Validate(request);
            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors.First().ErrorMessage);
            var userIdClaim = _contextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Result<Guid>.Failure("User is not authenticated");

            var existingProject = await _projectRepository.GetByIdAsync(userId);
            if (existingProject != null)
                return Result<Guid>.Failure("A project with the same name already exists for this user.");
            var project = new Domain.Models.Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return Result<Guid>.Success(project.Id);
        }
    }
}
