using System.ComponentModel.DataAnnotations;

namespace FurAniJoGa.WebHost.FileAPI.Dto;

public class UploadMetadataDto
{
    [Required]
    public Guid RequestId { get; set; }
    
    [Required]
    public Dictionary<string, string> Metadata { get; set; }
}