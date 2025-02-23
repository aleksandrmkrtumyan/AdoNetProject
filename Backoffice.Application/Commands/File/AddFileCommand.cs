using Application.SqlQueries.DileData;
using Application.SqlQueries.DileData.Models;
using Application.SqlQueries.FileDbs;
using Application.SqlQueries.FileDbs.Models;
using Backoffice.Application.Commands.File.Models;

namespace Backoffice.Application.Commands.File;

public class AddFileCommand
{
    #region Fields
    
    private readonly AddFileInfoSqlQuery addFileInfoSqlQuery;
    private readonly AddFileDataQuery addFileDataQuery;

    #endregion Fields
    
    #region Constructor

    public AddFileCommand(
        AddFileInfoSqlQuery addFileInfoSqlQuery,
        AddFileDataQuery addFileDataQuery)
    {
        this.addFileInfoSqlQuery = addFileInfoSqlQuery;
        this.addFileDataQuery = addFileDataQuery;
    }
    
    #endregion Constructor
    
    #region Method

    public async Task Execute(AddFileInputModel inputModel)
    {
        await addFileInfoSqlQuery.Execute(new FileInfoInputModel
        {
            FileName = inputModel.FileName,
            ClientId = inputModel.ClientId,
            FileId = inputModel.FileId,
            ConnectionString = inputModel.ConnectionString
        });
        await addFileDataQuery.Execute(new FileDataInputModel
        {
            ConnectionString = inputModel.ConnectionString,
            FileId = inputModel.FileId,
            FileData = inputModel.FileData
        });

    }
    
    #endregion Method
}