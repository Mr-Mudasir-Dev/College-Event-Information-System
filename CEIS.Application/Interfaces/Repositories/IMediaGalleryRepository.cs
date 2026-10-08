using CEIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IMediaGalleryRepository : IGenricRepository<MediaGallery>
    {
        Task<IEnumerable<MediaGallery>> GetByEventAsync(int eventId, CancellationToken cancellationToken = default);
    }
}
