using System.Text.Json.Serialization;

namespace Backoffice.Client.Models.Files;

public class SaveFileModel
{
    public Guid ClientId { get; set; }
    public Guid FileId { get; set; }
    public string FileName{ get; set; }
    public byte[] FileData { get; set; }
}