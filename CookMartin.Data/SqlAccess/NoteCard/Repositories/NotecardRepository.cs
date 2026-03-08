using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Notecard;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public class NotecardRepository : INotecardRepository
{
    private readonly IReadDb _readDb;

    public NotecardRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }
    public async Task<int> CreateAsync(IWriteDb writeDb, CreateNotecardDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId", dto.CollectionId);
        parameters.Add("@FrontDescription", dto.FrontDescription);
        parameters.Add("@BackDescription", dto.BackDescription);
        parameters.Add("@NotecardId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        var result = await writeDb.QueryAsync<dynamic, DynamicParameters>(
            "note.stp_CreateNotecard",
            parameters);

        return parameters.Get<int>("@NotecardId");
    }

    public async Task<IEnumerable<NotecardDto>> GetByCollectionAsync(int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        return await _readDb.QueryAsync<NotecardDto, object>(
            "note.stp_GetNotecardsByCollection",
            parameters);
    }

    public async Task<NotecardDto?> GetByIdAsync(int notecardId)
    {
        var parameters = new { NotecardId = notecardId };

        var result = await _readDb.QueryAsync<NotecardDto, object>(
            "note.stp_GetNotecardById",
            parameters);

        return result.FirstOrDefault();
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateNotecardDto dto)
    {
        var parameters = new
        {
            NotecardId = dto.NotecardId,
            FrontDescription = dto.FrontDescription,
            BackDescription = dto.BackDescription
        };

        var result = await writeDb.QueryAsync<dynamic, object>(
            "note.stp_UpdateNotecard",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int notecardId)
    {
        var parameters = new { NotecardId = notecardId };

        var result = await writeDb.QueryAsync<dynamic, object>(
            "note.stp_DeleteNotecard",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
