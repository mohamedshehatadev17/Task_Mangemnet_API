using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjectsV1
{
    public class GetAllProjectsQueryV1 : IRequest<Result<IEnumerable<ProjectResponse>>>
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
