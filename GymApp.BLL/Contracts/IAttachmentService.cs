namespace GymApp.BLL.Contracts
{
    public interface IAttachmentService
    {
        Task<string?> UploadAsync(Stream stream, string fileName, string folderName, CancellationToken cancellationToken = default);

        Task<(Stream stream, string contentType)?> GetFileAsync(string fileName, string folderName, CancellationToken cancellationToken);
    }
}
