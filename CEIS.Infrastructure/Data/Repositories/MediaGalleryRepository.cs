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
    public class MediaGalleryRepository : GenricRepository<MediaGallery>, IMediaGalleryRepository
    {
        private readonly ApplicationDbContext _context;
        public MediaGalleryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MediaGallery>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            return await _context.MediaGallerys
                .Where(x => x.EventId == eventId)
                .ToListAsync(cancellationToken);
        }
    }
}
