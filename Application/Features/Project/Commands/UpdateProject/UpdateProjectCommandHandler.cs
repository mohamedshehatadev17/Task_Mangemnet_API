using Mapster;
using MapsterMapper;
using MediatR;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.UpdateProject;

public class UpdateProjectCommandHandler
        : IRequestHandler<UpdateProjectCommand, Result<bool>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;
    public UpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdateProjectCommand request,CancellationToken cancellationToken)
    {
        var validationResult = new UpdateProjectCommandValidator().Validate(request);
        if (!validationResult.IsValid)
            return Result<bool>.Failure(validationResult.Errors.First().ErrorMessage);

        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null)
            return Result<bool>.Failure("Project not found.");

            // Fix CS8389 by specifying the type argument for Adapt<>
        var updatedProject = request.Adapt(project);
        _repository.Update(updatedProject);
        var result = await _repository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}