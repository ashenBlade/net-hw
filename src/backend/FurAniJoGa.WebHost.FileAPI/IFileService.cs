namespace FurAniJoGa.WebHost.FileAPI;

public interface IFileService
{
    Task<Guid> SaveFileAsync(IFormFile file, CancellationToken token = default);
    Task<File?> DownloadFileAsync(Guid fileId, CancellationToken token = default);
}