
using CEIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface ICertificateRepository : IGenricRepository<Certificate>
    {
        Task<Certificate?> GetByEventAndStudentAsync(int eventId, string studentId, CancellationToken cancellationToken = default);
    }
}
