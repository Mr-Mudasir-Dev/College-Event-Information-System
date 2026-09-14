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
    public class RegistrationRepository : GenricRepository<Registration>, IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Registration?> GetByEventAndStudentAsync(int eventId, string studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Registrations
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.StudentId == studentId, cancellationToken);
        }

        public async Task<IEnumerable<Registration>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            return await _context.Registrations
                .Where(r => r.EventId == eventId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Registration>> GetByStudentAsync(string studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Registrations
                .Where(r => r.StudentId == studentId)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetConfirmedCountByEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            return await _context.Registrations
                .CountAsync(r => r.EventId == eventId && r.Status == Domain.Enum.RegistrationStatus.Confirmed, cancellationToken);
        }
    }
}
