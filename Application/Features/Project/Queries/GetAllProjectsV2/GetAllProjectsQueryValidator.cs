using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskMangement.Application.Features.Project.Queries.GetAllProjectsV2;

public class GetAllProjectsQueryValidator : AbstractValidator<GetAllProjectsQueryV2>
{
    public GetAllProjectsQueryValidator()
    {

    }
    
}