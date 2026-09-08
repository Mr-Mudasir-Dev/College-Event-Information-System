using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Data.Repositories
{
    public class EventRepository : GenricRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Event>> GetApprovedUpcomingEventsAsync(CancellationToken cancellationToken = default)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _context.Events
                .Where(e => e.Status == Domain.Enum.EventStatus.Approved && e.Date == today)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(string organizerId, CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Where(e => e.OrganizerId == organizerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetPendingEventsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Where(e => e.Status == Domain.Enum.EventStatus.Pending)
                .ToListAsync(cancellationToken);
        }
    }
}
