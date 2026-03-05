using CookMartin.Data.Models.NoteCard;
using CookMartin.Data.SqlAccess.Interfaces;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly ICookMartinDataAccess _dataAccess;

    public CollectionRepository(ICookMartinDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<int> CreateAsync(CreateCollectionDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", dto.UserId);
        parameters.Add("@Name", dto.Name);
        parameters.Add("@CollectionId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        var result = await _dataAccess.QueryDataAsync<dynamic, DynamicParameters>(
            "note.stp_CreateCollection",
            parameters);

        return parameters.Get<int>("@CollectionId");
    }

    public async Task<IEnumerable<CollectionDto>> GetByUserAsync(string userId)
    {
        var parameters = new { UserId = userId };

        return await _dataAccess.QueryDataAsync<CollectionDto, object>(
            "note.stp_GetCollectionsByUser",
            parameters);
    }

    public async Task<CollectionDto?> GetByIdAsync(int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        var result = await _dataAccess.QueryDataAsync<CollectionDto, object>(
            "note.stp_GetCollectionById",
            parameters);

        return result.FirstOrDefault();
    }

    public async Task<int> UpdateAsync(UpdateCollectionDto dto)
    {
        var parameters = new
        {
            CollectionId = dto.CollectionId,
            Name = dto.Name
        };

        var result = await _dataAccess.QueryDataAsync<dynamic, object>(
            "note.stp_UpdateCollection",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(int collectionId)
    {
        var parameters = new { CollectionId = collectionId };

        var result = await _dataAccess.QueryDataAsync<dynamic, object>(
            "note.stp_DeleteCollection",
            parameters);

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
