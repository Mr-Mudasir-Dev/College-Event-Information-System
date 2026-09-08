using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;

        public UpdateEventCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if (existingEvent.OrganizerId != request.OrganizerId)
                throw new ForbiddenException();

            if(existingEvent.Status != Domain.Enum.EventStatus.Pending)
                throw new ConflictException("Only pending events can be edited.");

            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.Category = request.Category;
            existingEvent.Date = request.Date;
            existingEvent.Time = request.Time;
            existingEvent.Venue = request.Venue;
            existingEvent.MaxParticipants = request.MaxParticipants;
            existingEvent.BannerImageUrl = request.BannerImageUrl;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            _uow.EventRepository.UpdateAsync(existingEvent);
            await _uow.SaveChangesAsync();

            return Result<string>.Success("Event updated successfully.");
        }
    }
}
