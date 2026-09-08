using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using CEIS.Domain.Enum;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        public CreateEventCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<int>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizerId))
                throw new UnauthorizedException();

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Date = request.Date,
                Time = request.Time,
                Venue = request.Venue,
                MaxParticipants = request.MaxParticipants,
                BannerImageUrl = request.BannerImageUrl,
                OrganizerId = request.OrganizerId,
                Status = EventStatus.Pending
            };

            await _uow.EventRepository.AddAsync(newEvent, cancellationToken);
            await _uow.SaveChangesAsync();

            return Result<int>.Success(newEvent.Id);

        }
    }
}
