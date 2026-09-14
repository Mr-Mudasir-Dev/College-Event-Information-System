using CEIS.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEventRepository EventRepository { get; }
        public IRegistrationRepository RegistrationRepository { get; }

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context,
            IEventRepository eventRepository,
            IRegistrationRepository registrationRepository)
        {
            _context = context;
            EventRepository = eventRepository;
            RegistrationRepository = registrationRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        }
    }
}
