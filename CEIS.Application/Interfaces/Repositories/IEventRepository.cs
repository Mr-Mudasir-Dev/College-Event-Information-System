using CEIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IEventRepository : IGenricRepository<Event>
    {
        Task<IEnumerable<Event>> GetPendingEventsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(string organizerId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Event>> GetApprovedUpcomingEventsAsync(CancellationToken cancellationToken = default);
    }
}
