using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.EventDelete
{
    public class EventDeleteCommandValidator : AbstractValidator<EventDeleteCommand>
    {
        public EventDeleteCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");
        }
    }
}
