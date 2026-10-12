using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.FilterEvents
{
    public class FilterEventsQueryHandler : IRequestHandler<FilterEventsQuery, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FilterEventsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<EventDto>>> Handle(FilterEventsQuery request, CancellationToken cancellationToken)
        {
            var filterEvents = await _unitOfWork.EventRepository.FilterEventsAsync(request.OrganizerId, request.titleVenue, request.Category, request.Date, request.Status, cancellationToken);

            var dto = filterEvents.Select(e => new EventDto
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
            }).ToList();

            return Result<IEnumerable<EventDto>>.Success(dto);
        }
    }
}
