using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskMangement.Application.Features.Tasks.Queries.GetTaskByProject
{
    public class GetTasksByProjectIdQueryValidator : AbstractValidator<GetTasksByProjectQuery>
    {
        public GetTasksByProjectIdQueryValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("Project Id is required.");
        }
    }
}
