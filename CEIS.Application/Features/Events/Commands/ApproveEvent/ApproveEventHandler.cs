using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Enum;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.ApproveEvent
{
    public class ApproveEventHandler : IRequestHandler<ApproveEventCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public ApproveEventHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(ApproveEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if (existingEvent.Status != EventStatus.Pending)
                throw new ConflictException($"Event is already '{existingEvent.Status}'. Only pending events can be approved.");

            existingEvent.Status = EventStatus.Approved;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            _uow.EventRepository.UpdateAsync(existingEvent);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Event approved successfully.");
        }
    }
}
