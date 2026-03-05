using CookMartin.Data.Models.NoteCard;
using CookMartin.Data.SqlAccess.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public class NotecardRepository : INotecardRepository
{
    private readonly ICookMartinDataAccess _dataAccess;

    public NotecardRepository(ICookMartinDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<int> CreateAsync(CreateNotecardDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId", dto.CollectionId);
        parameters.Add("@FrontDescription", dto.FrontDescription);
        parameters.Add("@BackDescription", dto.BackDescription);
        parameters.Add("@NotecardId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        var result = await _dataAccess.QueryDataAsync<dynamic, DynamicParameters>(
            "note.stp_CreateNotecard",
            parameters);

        return parameters.Get<int>("@NotecardId");
    }

    public async Task<IEnumerable<NotecardDto>> GetByCollectionAsync(int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        return await _dataAccess.QueryDataAsync<NotecardDto, object>(
            "note.stp_GetNotecardsByCollection",
            parameters);
    }

    public async Task<NotecardDto?> GetByIdAsync(int notecardId)
    {
        var parameters = new { NotecardId = notecardId };

        var result = await _dataAccess.QueryDataAsync<NotecardDto, object>(
            "note.stp_GetNotecardById",
            parameters);

        return result.FirstOrDefault();
    }

    public async Task<int> UpdateAsync(UpdateNotecardDto dto)
    {
        var parameters = new
        {
            NotecardId = dto.NotecardId,
            FrontDescription = dto.FrontDescription,
            BackDescription = dto.BackDescription
        };

        var result = await _dataAccess.QueryDataAsync<dynamic, object>(
            "note.stp_UpdateNotecard",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(int notecardId)
    {
        var parameters = new { NotecardId = notecardId };

        var result = await _dataAccess.QueryDataAsync<dynamic, object>(
            "note.stp_DeleteNotecard",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
