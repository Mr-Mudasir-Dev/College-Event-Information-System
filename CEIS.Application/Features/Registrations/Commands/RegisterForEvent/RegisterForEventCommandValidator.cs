using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.RegisterForEvent
{
    public class RegisterForEventCommandValidator : AbstractValidator<RegisterForEventCommand>
    {
        public RegisterForEventCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
