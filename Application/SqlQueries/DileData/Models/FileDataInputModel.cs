namespace Application.SqlQueries.DileData.Models;

public class FileDataInputModel
{
    public string ConnectionString { get; set; }
    public Guid FileId { get; set; }
    public byte[] FileData { get; set; }
}