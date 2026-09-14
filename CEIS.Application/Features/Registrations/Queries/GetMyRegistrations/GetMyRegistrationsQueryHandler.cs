using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries.GetMyRegistrations
{
    public class GetMyRegistrationsQueryHandler : IRequestHandler<GetMyRegistrationsQuery, Result<IEnumerable<RegistrationDto>>>
    {
        private readonly IUnitOfWork _uow;
        public GetMyRegistrationsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<IEnumerable<RegistrationDto>>> Handle(GetMyRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var registrations = await _uow.RegistrationRepository.GetByStudentAsync(request.StudentId);

            var dto = new List<RegistrationDto>();

            foreach(var reg in registrations)
            {
                var ev = await _uow.EventRepository.GetByIdAsync(reg.Id);

                dto.Add(new RegistrationDto
                {
                    RegistrationId = reg.Id,
                    EventId = reg.EventId,
                    EventTitle = ev?.Title ?? "Unknown Event",
                    EventDate = ev?.Date ?? default,
                    EventTime = ev?.Time ?? default,
                    Venue = ev?.Venue ?? string.Empty,
                    Status = reg.Status.ToString()
                });
            }

            return Result<IEnumerable<RegistrationDto>>.Success(dto);
        }
    }
}
