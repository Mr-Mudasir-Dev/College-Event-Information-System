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

namespace CEIS.Application.Features.Feedbacks.Queries.GetEventFeedback
{
    public class GetEventFeedbackQueryHandler : IRequestHandler<GetEventFeedbackQuery, Result<IEnumerable<FeedbackDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityService _identity;
        public GetEventFeedbackQueryHandler(IUnitOfWork uow, IIdentityService identity)
        {
            _identity = identity;
            _uow = uow;
        }

        public async Task<Result<IEnumerable<FeedbackDto>>> Handle(GetEventFeedbackQuery request, CancellationToken cancellationToken)
        {
            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var feedback = await _uow.FeedbackRepository.GetByEventAsync(request.EventId, cancellationToken);

            var dtos = new List<FeedbackDto>();

            foreach(var fb in feedback)
            {
                var user = await _identity.GetUserByIdAsync(fb.UserId);

                dtos.Add(new FeedbackDto
                {
                    Rating = fb.Rating,
                    Comments = fb.Comments,
                    UserName = user?.FullName ?? "Anonymous",
                    SubmittedOn = fb.CreatedAt
                });
            }

            return Result<IEnumerable<FeedbackDto>>.Success(dtos, "Fetched successfully");
        }
    }
}
