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

namespace CEIS.Application.Features.Events.Commands.RejectEvent
{
    public class RejectEventHandler : IRequestHandler<RejectEventCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public RejectEventHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(RejectEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
            ?? throw new NotFoundException("Event", request.EventId);
            if (existingEvent.Status != EventStatus.Pending)
                throw new ConflictException($"Event is already '{existingEvent.Status}'. Only pending events can be rejected.");

            existingEvent.Status = EventStatus.Rejected;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            _uow.EventRepository.UpdateAsync(existingEvent);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success($"Event rejected. Reason: {request.Reason}");


        }
    }
}
