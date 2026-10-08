using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Feedbacks.Commands.SubmitAttendanceFeedback
{
    public class SubmitAttendanceFeedbackCommandValidator : AbstractValidator<SubmitAttendanceFeedbackCommand>
    {
        public SubmitAttendanceFeedbackCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comments)
                .MaximumLength(1000).WithMessage("Comments cannot exceed 1000 characters.");
        }
    }
}
