using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Certificates.Commands.GenerateCertificate
{
    public class GenerateCertificateCommandHandler : IRequestHandler<GenerateCertificateCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICertificateGenerator _certificateGenerator;
        private readonly IIdentityService _identityService;
        public GenerateCertificateCommandHandler(IUnitOfWork uow,
            ICertificateGenerator certificateGenerator,
            IIdentityService identityService)
        {
            _uow = uow;
            _certificateGenerator = certificateGenerator;
            _identityService = identityService;

        }


        public async Task<Result<string>> Handle(GenerateCertificateCommand request, CancellationToken cancellationToken)
        {
            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var registration = await _uow.RegistrationRepository
                .GetByEventAndStudentAsync(request.EventId, request.StudentId, cancellationToken)
                ?? throw new NotFoundException("Registration was not found");

            if (registration == null || !registration.IsAttended)
                throw new ForbiddenException("Certificate is only available for events you have attended.");

            var existingCertificate = await _uow.CertificateRepository
                .GetByEventAndStudentAsync(request.EventId, request.StudentId, cancellationToken);

            if (existingCertificate != null)
                return Result<string>.Success(existingCertificate.CertificateUrl);

            var std = await _identityService.GetUserByIdAsync(request.StudentId)
                ?? throw new NotFoundException("Student", request.StudentId);

            var pdfBytes = _certificateGenerator.GenerateCertificate(std.FullName, ev.Title, ev.Date);

            var fileName = $"certificate_{request.EventId}_{request.StudentId}.pdf";
            var certificateFolder = Path.Combine("wwwroot", "uploads", "certificates");
            Directory.CreateDirectory(certificateFolder);
            var fileUrl = Path.Combine(certificateFolder, fileName);
            await File.WriteAllBytesAsync(fileUrl, pdfBytes, cancellationToken);

            var certificateUrl = $"/uploads/certificates/{fileName}";

            var certificate = new Certificate
            {
                EventId = request.EventId,
                StudentId = request.StudentId,
                CertificateUrl = certificateUrl,
                IssuedOn = DateTime.UtcNow
            };

            await _uow.CertificateRepository.AddAsync(certificate, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(certificateUrl);


        }
    }
}
