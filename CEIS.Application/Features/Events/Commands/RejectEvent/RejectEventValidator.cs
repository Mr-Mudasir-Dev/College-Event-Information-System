using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.RejectEvent
{
    public class RejectEventValidator :AbstractValidator<RejectEventCommand>
    {
        public RejectEventValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Rejection reason is required.")
                .MaximumLength(1000).WithMessage("MaximumLength is 1000");
        }
    }
}
