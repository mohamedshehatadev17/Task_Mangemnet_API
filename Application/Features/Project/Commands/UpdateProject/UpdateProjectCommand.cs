using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}