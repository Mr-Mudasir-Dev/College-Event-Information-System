using AutoMapper.Configuration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.ApproveEvent
{
    public class ApproveEventValidator : AbstractValidator<ApproveEventCommand>
    {
        public ApproveEventValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
