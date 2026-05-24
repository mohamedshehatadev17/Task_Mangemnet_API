using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
