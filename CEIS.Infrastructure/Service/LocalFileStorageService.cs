using CEIS.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CEIS.Infrastructure.Service
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void DeleteFile(string fileUrl)
        {
            var filePath = Path.Combine(_environment.WebRootPath, fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folderName);

            if(!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return $"/uploads/{folderName}/{uniqueFileName}";
        }
    }
}
