using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Queries.GetEventMedia
{
    public class GetEventMediaQueryValidator : AbstractValidator<GetEventMediaQuery>
    {
        public GetEventMediaQueryValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0)
                .WithMessage("EventId is inValid");
        }
    }
}
