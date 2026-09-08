using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetPendingEvents
{
    public class GetPendingEventsQueryHandler : IRequestHandler<GetPendingEventsQuery, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetPendingEventsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<IEnumerable<EventDto>>> Handle(GetPendingEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _uow.EventRepository.GetPendingEventsAsync(cancellationToken);
            if (!events.Any())
                return Result<IEnumerable<EventDto>>.Success(new List<EventDto>(), "Record not founded");

            var dto = events.Select(e => new EventDto
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
