namespace FurAniJoGa.WebHost.FileAPI;

public interface IFileService
{
    Task<Guid> SaveFileAsync(IFormFile file, CancellationToken token = default);
    Task<FileContent?> DownloadFileAsync(Guid fileId, CancellationToken token = default);
    Task<File?> GetFileInfoAsync(Guid fileId, CancellationToken token = default);
}