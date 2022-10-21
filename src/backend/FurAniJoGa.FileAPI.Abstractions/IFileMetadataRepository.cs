namespace FurAniJoGa.FileAPI.Abstractions;

public interface IFileMetadataRepository
{
    Task<FileMetadata?> GetMetadataByIdAsync(Guid fileId, CancellationToken token = default);
    Task AddMetadataAsync(FileMetadata metadata, CancellationToken token = default);
}