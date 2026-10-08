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

namespace CEIS.Application.Features.Registrations.Queries.GetEventAttendance
{
    public class GetEventAttendanceQueryHandler : IRequestHandler<GetEventAttendanceQuery, Result<IEnumerable<EventAttendanceDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityService _identity;
        public GetEventAttendanceQueryHandler(IUnitOfWork uow, IIdentityService identity)
        {
            _uow = uow;
            _identity = identity;
        }

        public async Task<Result<IEnumerable<EventAttendanceDto>>> Handle(GetEventAttendanceQuery request, CancellationToken cancellationToken)
        {
            var ev = await _uow.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if(ev.OrganizerId != request.OrganizerId)
                throw new ForbiddenException("You are not allowed to view attendance for this event.");

            var registrations = await _uow.RegistrationRepository.GetByEventAsync(request.EventId, cancellationToken);

            var dtos = new List<EventAttendanceDto>();

            foreach(var reg in registrations)
            {
                var student = await _identity.GetUserByIdAsync(reg.StudentId);

                dtos.Add(new EventAttendanceDto
                {
                    RegistrationId = reg.Id,
                    StudentId = reg.StudentId,
                    StudentName = student?.FullName ?? "Unknown",
                    StudentEmail = student?.Email ?? string.Empty,
                    Department = student?.Department ?? string.Empty,
                    EnrollmentNo = student?.EnrollmentNo,
                    Status = reg.Status.ToString(),
                    RegisteredOn = reg.CreatedAt,
                    IsAttended = reg.IsAttended,
                    AttendedAt = reg.AttendedAt
                });
            }

            return Result<IEnumerable<EventAttendanceDto>>.Success(dtos, "fetced successfully");
        }
    }
}
