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
    public class CertificateRepository : GenricRepository<Certificate>, ICertificateRepository
    {
        private readonly ApplicationDbContext _context;
        public CertificateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Certificate?> GetByEventAndStudentAsync(int eventId, string studentId, CancellationToken cancellationToken = default)
        {
            return await _context.Certificates
                .FirstOrDefaultAsync(x => x.EventId == eventId && x.StudentId == studentId, cancellationToken);
        }
    }
}
