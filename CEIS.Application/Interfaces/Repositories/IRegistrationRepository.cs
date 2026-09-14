using CEIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IRegistrationRepository : IGenricRepository<Registration>
    {
        Task<Registration?> GetByEventAndStudentAsync(int eventId, string studentId, CancellationToken cancellationToken = default);
        Task<int> GetConfirmedCountByEventAsync(int eventId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Registration>> GetByStudentAsync(string studentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Registration>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default);
    }
}
