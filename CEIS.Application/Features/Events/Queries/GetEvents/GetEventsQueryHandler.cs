using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetEventsQueryHandler(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<Result<IEnumerable<EventDto>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _uow.EventRepository.GetApprovedUpcomingEventsAsync(cancellationToken);

            var filtered = events.AsEnumerable();

            if (request.Category.HasValue)
                filtered = filtered.Where(e => e.Category == request.Category.Value);

            if (request.FromDate.HasValue)
                filtered = filtered.Where(e => e.Date >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                filtered = filtered.Where(e => e.Date <= request.ToDate.Value);

            var dto = filtered.Select(e => new EventDto
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

            return Result<IEnumerable<EventDto>>.Success(dto);
        }
    }
}
