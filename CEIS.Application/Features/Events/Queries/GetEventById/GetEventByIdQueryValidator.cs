using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQueryValidator : AbstractValidator<GetEventByIdQuery>
    {
        public GetEventByIdQueryValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
