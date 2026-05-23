using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
