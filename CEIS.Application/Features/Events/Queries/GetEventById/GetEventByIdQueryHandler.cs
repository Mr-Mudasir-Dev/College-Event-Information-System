using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<EventDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetEventByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEvent = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var dto = new EventDto
            {
                Id = existingEvent.Id,
                Title = existingEvent.Title,
                Description = existingEvent.Description,
                Category = existingEvent.Category.ToString(),
                Date = existingEvent.Date,
                Time = existingEvent.Time,
                Venue = existingEvent.Venue,
                MaxParticipants = existingEvent.MaxParticipants,
                BannerImageUrl = existingEvent.BannerImageUrl,
                Status = existingEvent.Status.ToString()
            };

            return Result<EventDto>.Success(dto);
        }
    }
}
