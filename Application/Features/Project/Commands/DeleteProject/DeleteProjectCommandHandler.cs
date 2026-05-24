using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler
          : IRequestHandler<DeleteProjectCommand, Result<bool>>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(
            IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Result<bool>> Handle(
            DeleteProjectCommand request,
            CancellationToken cancellationToken)
        {
            var validationResult = new DeleteProjectCommandValidator().Validate(request);
            if (!validationResult.IsValid)
                return Result<bool>.Failure(validationResult.Errors.First().ErrorMessage);

            var project = await _projectRepository.GetByIdAsync(request.Id);
            if (project is null)
                return Result<bool>.Failure("Project not found.");

            project.IsDeleted = true;
            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}

