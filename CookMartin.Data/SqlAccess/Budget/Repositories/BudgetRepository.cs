using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Budget;
using CookMartin.Data.SqlAccess.Budget.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.Budget.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly IReadDb _readDb;

    public BudgetRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId",   dto.CollectionId);
        parameters.Add("@Name",           dto.Name);
        parameters.Add("@Type",           dto.Type);
        parameters.Add("@StartingAmount", dto.StartingAmount);
        parameters.Add("@BudgetId",       dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await writeDb.QueryAsync<dynamic, DynamicParameters>("budget.stp_CreateBudget", parameters);

        return parameters.Get<int>("@BudgetId");
    }

    public async Task<BudgetDto?> GetByIdAsync(int budgetId)
    {
        var result = await _readDb.QueryAsync<BudgetDto, object>(
            "budget.stp_GetBudgetById",
            new { BudgetId = budgetId });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<BudgetDto>> GetByCollectionAsync(int collectionId)
    {
        return await _readDb.QueryAsync<BudgetDto, object>(
            "budget.stp_GetBudgetsByCollection",
            new { CollectionId = collectionId });
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetDto dto)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_UpdateBudget",
            new
            {
                BudgetId       = dto.BudgetId,
                Name           = dto.Name,
                Type           = dto.Type,
                StartingAmount = dto.StartingAmount
            });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int budgetId)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_DeleteBudget",
            new { BudgetId = budgetId });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
