namespace Application.SqlQueries.FileDbs.Models;

public class FileInfoInputModel
{
    public string FileName { get; set; }
    public Guid ClientId { get; set; }
    public Guid FileId { get; set; }
    public string ConnectionString { get; set; }
}