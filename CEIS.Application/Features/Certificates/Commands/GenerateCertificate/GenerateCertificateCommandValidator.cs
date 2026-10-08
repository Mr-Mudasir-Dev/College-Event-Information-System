using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Certificates.Commands.GenerateCertificate
{
    public class GenerateCertificateCommandValidator : AbstractValidator<GenerateCertificateCommand>
    {
        public GenerateCertificateCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0)
                .WithMessage("Eventid is invalid");
        }
    }
}
