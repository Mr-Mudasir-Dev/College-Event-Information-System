using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0)
                .WithMessage("Invalid event id.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid category.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Event date must be today or in the future.");

            RuleFor(x => x.Venue)
                .NotEmpty().WithMessage("Venue is required.")
                .MaximumLength(150);

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Max participants must be greater than zero.");
        }
    }
}
