using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Queries.GetEventMedia
{
    public class GetEventMediaQueryHandler : IRequestHandler<GetEventMediaQuery, Result<IEnumerable<MediaDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEventMediaQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<MediaDto>>> Handle(GetEventMediaQuery request, CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            var media = await _unitOfWork.MediaGalleryRepository.GetByEventAsync(request.EventId, cancellationToken);

            var dto = media.Select(m => new MediaDto
            {
                Id = m.Id,
                FileType = m.FileType.ToString(),
                FileUrl = m.FileUrl,
                Caption = m.Caption
            });

            return Result<IEnumerable<MediaDto>>.Success(dto);
        }
    }
}
