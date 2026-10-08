using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.MarkAttendance
{
    public class MarkAttendanceCommandValidator : AbstractValidator<MarkAttendanceCommand>
    {
        public MarkAttendanceCommandValidator()
        {
            RuleFor(x => x.RegistrationId)
                .GreaterThan(0).WithMessage("Invalid registration id");
        }
    }
}
