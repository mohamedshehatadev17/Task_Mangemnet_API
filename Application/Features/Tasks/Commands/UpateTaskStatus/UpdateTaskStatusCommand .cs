using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Tasks.Commands.UpateTask
{
    public class UpdateTaskStatusCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public TaskStatus Status { get; set; }
    }
}
