using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Enum;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.CancelRegistration
{
    public class CancelRegistrationCommandHandler : IRequestHandler<CancelRegistrationCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public CancelRegistrationCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(CancelRegistrationCommand request, CancellationToken cancellationToken)
        {
            var registration = await _uow.RegistrationRepository
                .GetByEventAndStudentAsync(request.EventId, request.StudentId, cancellationToken)
                ?? throw new NotFoundException("You are not registered for this event.");

            if (registration.Status == RegistrationStatus.Cancelled)
                throw new ConflictException("Registration is already cancelled.");

            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var eventDateTime = ev.Date.ToDateTime(ev.Time);
            if(eventDateTime <=  DateTime.UtcNow)
                throw new ConflictException("Cannot cancel registration after the event has started.");
            registration.Status = RegistrationStatus.Cancelled;
            registration.UpdatedAt = DateTime.UtcNow;

            _uow.RegistrationRepository.UpdateAsync(registration);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Registration cancelled successfully.");
        }
    }
}
