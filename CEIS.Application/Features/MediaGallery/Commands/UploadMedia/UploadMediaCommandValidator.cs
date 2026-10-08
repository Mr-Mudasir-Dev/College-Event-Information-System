using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.UploadMedia
{
    internal class UploadMediaCommandValidator : AbstractValidator<UploadMediaCommand>
    {
        private readonly string[] _allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] _allowedVideoExtensions = { ".mp4", ".mov", ".avi" };
        private const long MaxFileSize = 50 * 1024 * 1024; // 20 MB

        public UploadMediaCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Invalid event id.");

            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.");

            RuleFor(x => x.File)
                .Must(f => f.Length <= MaxFileSize)
                .When(x => x.File != null)
                .WithMessage("File size must not exceed 20 MB.");

            RuleFor(x => x)
                .Must(HaveValidExtension)
                .When(x => x.File != null)
                .WithMessage("Invalid file type. Allowed: jpg, jpeg, png, gif, mp4, mov, avi.");


            RuleFor(x => x.Caption)
                .MaximumLength(500)
                .WithMessage("cannot 500 plus words");
        }

        private bool HaveValidExtension(UploadMediaCommand cmd)
        {
            var ext = Path.GetExtension(cmd.File.FileName).ToLowerInvariant();
            return _allowedImageExtensions.Contains(ext) || _allowedVideoExtensions.Contains(ext);
        }
    }
}
