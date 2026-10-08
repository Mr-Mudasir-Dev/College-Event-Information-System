using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.DeleteMedia
{
    public class DeleteMediaCommandValidator : AbstractValidator<DeleteMediaCommand>
    {
        public DeleteMediaCommandValidator()
        {
            RuleFor(x => x.MediaId)
                .GreaterThan(0)
                .WithMessage("MediaId is invalid");
        }
    }
}
