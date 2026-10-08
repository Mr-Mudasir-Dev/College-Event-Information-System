using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.DeleteMedia
{
    public class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Result<string>>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _uow;
        public DeleteMediaCommandHandler(IFileStorageService fileStorageService, IUnitOfWork uow)
        {
            _fileStorageService = fileStorageService;
            _uow = uow;
        }

        public async Task<Result<string>> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var media = await _uow.MediaGalleryRepository.GetByIdAsync(request.MediaId, cancellationToken)
                ?? throw new NotFoundException("Media", request.MediaId);

            if (!request.IsAdmin && request.RequestedBy != media.UploadedBy)
                throw new ForbiddenException("You are not allowed to delete this media.");

            _fileStorageService.DeleteFile(media.FileUrl);
            _uow.MediaGalleryRepository.DeleteAsync(media);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<string>.Success("Media deleted successfully.");
        }
    }
}
