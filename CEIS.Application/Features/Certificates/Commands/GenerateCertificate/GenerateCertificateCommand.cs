using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Certificates.Commands.GenerateCertificate
{
    public class GenerateCertificateCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }
}
