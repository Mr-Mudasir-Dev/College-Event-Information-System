using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using CEIS.Domain.Enum;
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

        public async Task<IReadOnlyList<Event>> FilterEventsAsync(string organizerId, string? titleVenue, string? category, string? date, string? status, CancellationToken cancellationToken = default)
        {
            var query = _context.Events.Where(e => e.OrganizerId == organizerId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(titleVenue))
            {
                query = query.Where(e => e.Title.Contains(titleVenue) || e.Venue.Contains(titleVenue));
            }

            if (!string.IsNullOrWhiteSpace(category) && 
                Enum.TryParse<EventCategory>(category, true, out var categoryValue))
            {
                query = query.Where(e => e.Category == categoryValue);
            }

            if(!string.IsNullOrWhiteSpace(status) && 
                Enum.TryParse<EventStatus>(status, true, out var eventStatus))
            {
                query = query.Where(e => e.Status == eventStatus);
            }

            // Date filter
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (!string.IsNullOrWhiteSpace(date))
            {
                switch (date.ToLower())
                {
                    case "today":
                        query = query.Where(e => e.Date == today);
                        break;

                    case "past":
                        query = query.Where(e => e.Date < today);
                        break;

                    case "upcoming":
                        query = query.Where(e => e.Date > today);
                        break;

                    case "week":
                        var weekEnd = today.AddDays(7);
                        query = query.Where(e =>
                            e.Date >= today && e.Date <= weekEnd);
                        break;

                    case "month":
                        var monthEnd = today.AddMonths(1);
                        query = query.Where(e =>
                            e.Date >= today && e.Date < monthEnd);
                        break;
                }
            }

            return await query
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync(cancellationToken);
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
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetPendingEventsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Where(e => e.Status == Domain.Enum.EventStatus.Pending)
                .ToListAsync(cancellationToken);
        }

        public Task<Event?> GetTitleEvent(string title, CancellationToken cancellationToken = default)
        {
            return _context.Events.FirstOrDefaultAsync(e => EF.Functions.Collate(e.Title, "SQL_Latin1_General_CP1_CI_AS") == title , cancellationToken);
        }
    }
}
