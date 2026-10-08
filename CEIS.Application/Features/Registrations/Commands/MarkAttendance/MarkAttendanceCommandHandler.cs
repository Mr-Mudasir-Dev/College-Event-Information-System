using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.MarkAttendance
{
    public class MarkAttendanceCommandHandler : IRequestHandler<MarkAttendanceCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public MarkAttendanceCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
        {
            var registration = await _uow.RegistrationRepository.GetByIdAsync(request.RegistrationId, cancellationToken)
                ?? throw new NotFoundException("Invalid registration code. No such registration found.");

            var ev = await _uow.EventRepository.GetByIdAsync(registration.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", registration.EventId);

            if (ev.OrganizerId != request.OrganizerId)
                throw new ForbiddenException("You are not allowed to mark attendance for this event.");

            if(registration.Status != Domain.Enum.RegistrationStatus.Confirmed)
                throw new ConflictException("Only confirmed registrations can be marked as attended.");

            if(registration.IsAttended)
                throw new ConflictException("Attendance already marked for this participant.");

            registration.IsAttended = true;
            registration.AttendedAt = DateTime.UtcNow;
            registration.UpdatedAt = DateTime.UtcNow;

            _uow.RegistrationRepository.UpdateAsync(registration);
            await _uow.SaveChangesAsync();

            return Result<string>.Success("Attendance marked successfully.");

        }
    }
}
