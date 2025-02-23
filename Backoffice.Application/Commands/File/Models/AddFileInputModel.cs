namespace Backoffice.Application.Commands.File.Models;

public class AddFileInputModel
{
    public Guid ClientId { get; set; }
    public Guid FileId { get; set; }
    public string FileName{ get; set; }
    public byte[] FileData { get; set; }
    public string ConnectionString { get; set; }
}