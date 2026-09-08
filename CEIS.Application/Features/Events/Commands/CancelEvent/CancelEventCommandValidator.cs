using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.CancelEvent
{
    public class CancelEventCommandValidator : AbstractValidator<CancelEventCommand>
    {
        public CancelEventCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
