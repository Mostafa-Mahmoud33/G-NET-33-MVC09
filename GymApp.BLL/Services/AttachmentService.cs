using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace GymApp.BLL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ILogger<AttachmentService> _logger;
        private readonly IWebHostEnvironment _environment;
        private const long MAXFILSIZE = 2* 1024 * 1024;
        private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png"];

        public AttachmentService(ILogger<AttachmentService> logger, IWebHostEnvironment environment) 
        {
            _logger = logger;
            _environment = environment;
        }
        public async Task<string?> UploadAsync(Stream stream, string fileName, string folderName, CancellationToken cancellationToken = default)
        {
            try
            {
                if (stream is null || !stream.CanRead)
                    return null;

                if (stream.Length == 0 || stream.Length > MAXFILSIZE)
                    return null;

                var extension = Path.GetExtension(fileName);

                if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Contains(extension))
                    return null;

                var uploadFolderPath = Path.Combine(_environment.ContentRootPath, folderName);
                Directory.CreateDirectory(uploadFolderPath);

                // Unique File Name

                string uniqueFileName = $"{Guid.NewGuid()}-{extension}";

                var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                await fileStream.CopyToAsync(stream, cancellationToken);
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload file Failed");
                return null;
            }
        }

        public async Task<(Stream stream, string contentType)?> GetFileAsync(string fileName, string folderName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(folderName))
                return null;

            var filePath = Path.Combine(_environment.ContentRootPath, folderName, fileName);

            if (!File.Exists(filePath))
                return null;

             var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var contentType = Path.GetExtension(filePath).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
            return (stream, contentType);


            throw new NotImplementedException();
        }
    }
}
