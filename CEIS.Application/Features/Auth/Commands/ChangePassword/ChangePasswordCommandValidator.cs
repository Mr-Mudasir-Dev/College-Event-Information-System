using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters.")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from current password.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Please confirm your new password.")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
