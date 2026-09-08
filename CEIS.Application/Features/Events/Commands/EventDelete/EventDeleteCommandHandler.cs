using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.EventDelete
{
    public class EventDeleteCommandHandler : IRequestHandler<EventDeleteCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public EventDeleteCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(EventDeleteCommand request, CancellationToken cancellationToken)
        {
            var eventexist = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            _uow.EventRepository.DeleteAsync(eventexist);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("successfully deleted");
        }
    }
}
