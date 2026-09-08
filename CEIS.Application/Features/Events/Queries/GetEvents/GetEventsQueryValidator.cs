using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
    {
        public GetEventsQueryValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate == null || x.ToDate == null || x.FromDate <= x.ToDate)
                .WithMessage("FromDate must be earlier than or equal to ToDate.");
        }
    }
}
