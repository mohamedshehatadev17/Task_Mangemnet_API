using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjectsV1;

public class GetAllProjectsQueryValidator : AbstractValidator<GetAllProjectsQueryV1>
{
    public GetAllProjectsQueryValidator()
    {

    }
    
}