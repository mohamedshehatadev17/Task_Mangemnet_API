using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjects;

public class GetAllProjectsQueryValidator : AbstractValidator<GetAllProjectsQuery>
{
    public GetAllProjectsQueryValidator()
    {

    }
    
}