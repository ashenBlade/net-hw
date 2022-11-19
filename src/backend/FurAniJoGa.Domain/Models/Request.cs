using System.ComponentModel.DataAnnotations;

namespace FurAniJoGa.Domain.Models;

public class Request
{
    public Guid Id { get; set; }
    public Guid? FileId { get; set; }

    public Request(Guid id, Guid? fileId = null)
    {
        Id = id;
        FileId = fileId;
    }
}