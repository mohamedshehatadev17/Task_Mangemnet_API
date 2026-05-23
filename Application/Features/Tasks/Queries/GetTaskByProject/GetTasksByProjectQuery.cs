using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Tasks.Queries.GetTaskByProject
{
    public class GetTasksByProjectQuery: IRequest<Result<ProjectTasksResponse>>
    {
        public Guid ProjectId { get; set; }
    }
}
