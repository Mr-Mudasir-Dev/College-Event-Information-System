using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        private readonly string[] _validRoles = { "Participant", "Organizer", "Admin" };
        public AssignRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.NewRole)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => _validRoles.Contains(role))
                .WithMessage("Invalid role. Must be one of: Participant, Organizer, Admin.");

        }
    }
}
