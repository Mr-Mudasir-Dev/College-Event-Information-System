using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries.GetEventRegistrations
{
    public class GetEventRegistrationsQueryValidator : AbstractValidator<GetEventRegistrationsQuery>
    {
        public GetEventRegistrationsQueryValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
