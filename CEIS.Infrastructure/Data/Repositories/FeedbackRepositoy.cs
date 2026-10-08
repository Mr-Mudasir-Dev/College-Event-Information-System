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
    public class FeedbackRepositoy : GenricRepository<Feedback>, IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepositoy(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            return await _context.Feedbacks
                .Where(x => x.EventId == eventId && x.Category == Domain.Enum.FeedbackCategory.EventFeedback)
                .ToListAsync(cancellationToken);
        }

        public Task<Feedback?> GetEventFeedbackByUserAsync(int eventId, string UserId, CancellationToken cancellationToken = default)
        {
            return _context.Feedbacks
                .FirstOrDefaultAsync(x =>
                x.EventId == eventId &&
                x.UserId == UserId &&
                x.Category == Domain.Enum.FeedbackCategory.EventFeedback,
                cancellationToken);
        }
    }
}
