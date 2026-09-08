using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetMyEvents
{
    public class GetMyEventsCommandHandler : IRequestHandler<GetMyEventsCommand, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetMyEventsCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<IEnumerable<EventDto>>> Handle(GetMyEventsCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizerId))
                throw new UnauthorizedException("Organizer information is missing.");

            var events = await _uow.EventRepository.GetEventsByOrganizerAsync(request.OrganizerId, cancellationToken);

            var dtos = events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Category = e.Category.ToString(),
                Date = e.Date,
                Time = e.Time,
                Venue = e.Venue,
                MaxParticipants = e.MaxParticipants,
                BannerImageUrl = e.BannerImageUrl,
                Status = e.Status.ToString()
            });

            return Result<IEnumerable<EventDto>>.Success(dtos);
        }
    }
}
