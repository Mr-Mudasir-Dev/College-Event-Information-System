using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IEventRepository EventRepository { get; }
        public IRegistrationRepository RegistrationRepository { get; }
        public IFeedbackRepository FeedbackRepository { get; }
        public IMediaGalleryRepository MediaGalleryRepository { get; }
        public ICertificateRepository CertificateRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
