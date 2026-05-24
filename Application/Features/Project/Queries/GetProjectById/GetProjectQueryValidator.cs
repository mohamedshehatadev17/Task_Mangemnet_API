using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskMangement.Application.Features.Project.Queries.GetProjectById
{
    public class GetProjectQueryValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Project ID is required.");
        }
    }
}
