using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommandValidator : AbstractValidator<ToggleUserStatusCommand>
    {
        public ToggleUserStatusCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
