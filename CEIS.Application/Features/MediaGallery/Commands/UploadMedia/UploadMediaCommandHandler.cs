using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Entity;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.UploadMedia
{
    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        public UploadMediaCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<string>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            var ev = await _unitOfWork.EventRepository.GetByIdAsync(request.EventId, cancellationToken)
                ?? throw new NotFoundException("Event", request.EventId);

            if(!request.IsAdmin && ev.OrganizerId != request.UploadedBy)
                throw new ForbiddenException("You are not allowed to upload media for this event.");

            var fileUrl = await _fileStorageService.SaveFileAsync(request.File, "media", cancellationToken);

            var media = new Domain.Entity.MediaGallery
            {
                EventId = request.EventId,
                FileType = request.FileType,
                FileUrl = fileUrl,
                UploadedBy = request.UploadedBy,
                Caption = request.Caption
            };

            await _unitOfWork.MediaGalleryRepository.AddAsync(media, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(fileUrl);
        }
    }
}
