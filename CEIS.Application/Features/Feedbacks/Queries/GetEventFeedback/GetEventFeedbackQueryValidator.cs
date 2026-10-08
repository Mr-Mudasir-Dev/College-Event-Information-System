using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Feedbacks.Queries.GetEventFeedback
{
    public class GetEventFeedbackQueryValidator : AbstractValidator<GetEventFeedbackQuery>
    {
        public GetEventFeedbackQueryValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
