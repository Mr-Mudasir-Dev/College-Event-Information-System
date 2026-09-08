using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.CancelEvent
{
    public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public CancelEventCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(CancelEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if (existingEvent.OrganizerId != request.OrganizerId)
                throw new ForbiddenException("You are not allowed to cancel this event.");

            if (existingEvent.Status == Domain.Enum.EventStatus.Cancelled || existingEvent.Status == Domain.Enum.EventStatus.Completed)
                throw new ConflictException($"Event is already '{existingEvent.Status}' and cannot be cancelled.");

            existingEvent.Status = Domain.Enum.EventStatus.Cancelled;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            _uow.EventRepository.UpdateAsync(existingEvent);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Event cancelled successfully.");
        }
    }
}
