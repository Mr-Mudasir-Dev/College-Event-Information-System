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

namespace CEIS.Application.Features.Feedbacks.Commands.SubmitAttendanceFeedback
{
    public class SubmitAttendanceFeedbackCommandHandler : IRequestHandler<SubmitAttendanceFeedbackCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        public SubmitAttendanceFeedbackCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<string>> Handle(SubmitAttendanceFeedbackCommand request, CancellationToken cancellationToken)
        {
            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var registration = await _uow.RegistrationRepository
                .GetByEventAndStudentAsync(request.EventId, request.UserId, cancellationToken);

            if (registration == null || !registration.IsAttended)
                throw new ForbiddenException("You can only submit feedback for events you have attended.");

            var existingFeedback = await _uow.FeedbackRepository
                .GetEventFeedbackByUserAsync(request.EventId, request.UserId, cancellationToken);

            if(existingFeedback != null)
                throw new ConflictException("You have already submitted feedback for this event.");

            var dto = new Feedback
            {
                UserId = request.UserId,
                Category = FeedbackCategory.EventFeedback,
                EventId = request.EventId,
                Rating = request.Rating,
                Comments = request.Comments
            };

            await _uow.FeedbackRepository.AddAsync(dto, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Feedback submitted successfully.");

        }
    }
}
