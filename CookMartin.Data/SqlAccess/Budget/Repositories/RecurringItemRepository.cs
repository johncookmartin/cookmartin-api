using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.RecurringItem;
using CookMartin.Data.SqlAccess.Budget.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.Budget.Repositories;

public class RecurringItemRepository : IRecurringItemRepository
{
    private readonly IReadDb _readDb;

    public RecurringItemRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateRecurringItemDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId",    dto.CollectionId);
        parameters.Add("@UserId",          dto.UserId);
        parameters.Add("@Label",           dto.Label);
        parameters.Add("@Amount",          dto.Amount);
        parameters.Add("@Type",            dto.Type.ToString());
        parameters.Add("@IsShared",        dto.IsShared);
        parameters.Add("@RecurringItemId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await writeDb.QueryAsync<dynamic, DynamicParameters>("budget.stp_CreateRecurringItem", parameters);

        return parameters.Get<int>("@RecurringItemId");
    }

    public async Task<RecurringItemDto?> GetByIdAsync(int recurringItemId)
    {
        var result = await _readDb.QueryAsync<RecurringItemDto, object>(
            "budget.stp_GetRecurringItemById",
            new { RecurringItemId = recurringItemId });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<RecurringItemDto>> GetByCollectionAsync(int collectionId)
    {
        return await _readDb.QueryAsync<RecurringItemDto, object>(
            "budget.stp_GetRecurringItemsByCollection",
            new { CollectionId = collectionId });
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateRecurringItemDto dto)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_UpdateRecurringItem",
            new
            {
                RecurringItemId = dto.RecurringItemId,
                Label           = dto.Label,
                Amount          = dto.Amount,
                Type            = dto.Type.ToString(),
                IsShared        = dto.IsShared
            });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int recurringItemId)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_DeleteRecurringItem",
            new { RecurringItemId = recurringItemId });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
