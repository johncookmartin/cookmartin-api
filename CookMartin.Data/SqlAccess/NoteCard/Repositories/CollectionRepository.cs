using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly IReadDb _readDb;

    public CollectionRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateCollectionDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", dto.UserId);
        parameters.Add("@Name", dto.Name);
        parameters.Add("@CollectionId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        var result = await writeDb.QueryAsync<dynamic, DynamicParameters>(
            "note.stp_CreateCollection",
            parameters);

        return parameters.Get<int>("@CollectionId");
    }

    public async Task<IEnumerable<CollectionDto>> GetByUserAsync(string userId)
    {
        var parameters = new { UserId = userId };

        return await _readDb.QueryAsync<CollectionDto, object>(
            "note.stp_GetCollectionsByUser",
            parameters);
    }

    public async Task<CollectionDto?> GetByIdAsync(int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        var result = await _readDb.QueryAsync<CollectionDto, object>(
            "note.stp_GetCollectionById",
            parameters);

        return result.FirstOrDefault();
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateCollectionDto dto)
    {
        var parameters = new
        {
            CollectionId = dto.CollectionId,
            Name = dto.Name
        };

        var result = await writeDb.QueryAsync<dynamic, object>(
            "note.stp_UpdateCollection",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        var result = await writeDb.QueryAsync<dynamic, object>(
            "note.stp_DeleteCollection",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
