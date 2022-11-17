using MediatR;

namespace FurAniJoGa.Worker.MongoUpdater.Commands;

public class MoveToPersistentBucketCommand: INotification
{
    /// <summary>
    /// File Id to move from temporary bucket to persistent bucket
    /// </summary>
    public Guid FileId { get; set; }
}