using CEIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IFeedbackRepository : IGenricRepository<Feedback>
    {
        Task<Feedback?> GetEventFeedbackByUserAsync(int eventId, string UserId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Feedback>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default);
    }
}
