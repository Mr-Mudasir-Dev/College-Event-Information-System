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
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const long MaxFileSize = 25 * 1024 * 1024;
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

            RuleFor(x => x.BannerImage!.Length)
            .LessThanOrEqualTo(MaxFileSize)
            .When(x => x.BannerImage != null)
            .WithMessage("Banner image must not exceed 5 MB.");

            RuleFor(x => x)
                .Must(c => _allowedExtensions.Contains(Path.GetExtension(c.BannerImage!.FileName).ToLowerInvariant()))
                .When(x => x.BannerImage != null)
                .WithMessage("Invalid image format. Allowed: jpg, jpeg, png, gif.");
        }
    }
}
