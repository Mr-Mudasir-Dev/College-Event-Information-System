using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.CancelRegistration
{
    public class CancelRegistrationCommandValidator : AbstractValidator<CancelRegistrationCommand>
    {
        public CancelRegistrationCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
