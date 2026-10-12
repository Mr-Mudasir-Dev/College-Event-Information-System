using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
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
        private readonly IFileStorageService _fileStorageService;

        public UpdateEventCommandHandler(IUnitOfWork uow, IFileStorageService fileStorageService)
        {
            _uow = uow;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if (existingEvent.OrganizerId != request.OrganizerId)
                throw new ForbiddenException();

            if(existingEvent.Status != Domain.Enum.EventStatus.Pending)
                throw new ConflictException("Only pending events can be edited.");

            var oldBannerUrl = existingEvent.BannerImageUrl;
            string? newBannerUrl = null;

            if (request.BannerImage != null)
                newBannerUrl = await _fileStorageService.SaveFileAsync(request.BannerImage, "events", cancellationToken);


            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.Category = request.Category;
            existingEvent.Date = request.Date;
            existingEvent.Time = request.Time;
            existingEvent.Venue = request.Venue;
            existingEvent.MaxParticipants = request.MaxParticipants;
            if (newBannerUrl != null)
                existingEvent.BannerImageUrl = newBannerUrl;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            _uow.EventRepository.UpdateAsync(existingEvent);
            await _uow.SaveChangesAsync();

            if (newBannerUrl != null && !string.IsNullOrEmpty(oldBannerUrl))
                _fileStorageService.DeleteFile(oldBannerUrl);

            return Result<string>.Success("Event updated successfully.");
        }
    }
}
