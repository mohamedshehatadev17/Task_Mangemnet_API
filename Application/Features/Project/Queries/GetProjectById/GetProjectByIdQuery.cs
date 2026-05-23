using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskMangement.Application.DTOs;
using TaskMangement.Application.Shared;

namespace TaskMangement.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<Result<ProjectResponse>?>
    {
        public Guid Id { get; set; }
    }
}
