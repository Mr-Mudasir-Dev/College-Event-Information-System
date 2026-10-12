using CEIS.Domain.Entity;
using CEIS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IEventRepository : IGenricRepository<Event>
    {
        Task<IReadOnlyList<Event>> FilterEventsAsync(string organizerId, string? titleVenue, string? category, string? date, string? status, CancellationToken cancellationToken = default);
        Task<Event?> GetTitleEvent(string title, CancellationToken cancellationToken = default);
        Task<IEnumerable<Event>> GetPendingEventsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(string organizerId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Event>> GetApprovedUpcomingEventsAsync(CancellationToken cancellationToken = default);
    }
}
