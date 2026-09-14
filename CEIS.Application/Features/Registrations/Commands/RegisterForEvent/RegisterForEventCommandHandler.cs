using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using CEIS.Domain.Enum;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.RegisterForEvent
{
    internal class RegisterForEventCommandHandler : IRequestHandler<RegisterForEventCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public RegisterForEventCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(RegisterForEventCommand request, CancellationToken cancellationToken)
        {

            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if (ev.Status != Domain.Enum.EventStatus.Approved)
                throw new ConflictException("Registration is only allowed for approved events.");

            var eventDatetime = ev.Date.ToDateTime(ev.Time);
            if (eventDatetime <= DateTime.UtcNow)
                throw new ConflictException("Cannot register for an event that has already passed.");

            var existingRegistration = await _uow.RegistrationRepository
                .GetByEventAndStudentAsync(request.EventId, request.StudentId, cancellationToken);

            if(existingRegistration != null)
            {
                if(existingRegistration.Status == Domain.Enum.RegistrationStatus.Confirmed)
                    throw new ConflictException("You are already registered for this event.");

                var confirmedCount = await _uow.RegistrationRepository
                    .GetConfirmedCountByEventAsync(request.EventId, cancellationToken);

                if(confirmedCount >= ev.MaxParticipants)
                    throw new ConflictException("This event has reached its maximum capacity.");

                existingRegistration.Status = Domain.Enum.RegistrationStatus.Confirmed;
                existingRegistration.UpdatedAt = DateTime.UtcNow;

                _uow.RegistrationRepository.UpdateAsync(existingRegistration);
                await _uow.SaveChangesAsync(cancellationToken);

                return Result<string>.Success("Successfully registered for the event.");
            }

            var currentConfirmedCount = await _uow.RegistrationRepository
                    .GetConfirmedCountByEventAsync(request.EventId, cancellationToken);

            if (currentConfirmedCount >= ev.MaxParticipants)
                throw new ConflictException("This event has reached its maximum capacity.");

            var register = new Registration
            {
                EventId = request.EventId,
                StudentId = request.StudentId,
                Status = RegistrationStatus.Confirmed
            };

            await _uow.RegistrationRepository.AddAsync(register, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Successfully registered for the event.");
        }
    }
}
