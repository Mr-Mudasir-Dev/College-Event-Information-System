using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetAllEvents
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, Result<IEnumerable<EventDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllEventsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<EventDto>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.EventRepository.GetAllAsync(cancellationToken);

            var dto = ev.Select(e => new EventDto
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
