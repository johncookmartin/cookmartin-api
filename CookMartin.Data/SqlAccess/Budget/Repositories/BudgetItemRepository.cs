using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.BudgetItem;
using CookMartin.Data.SqlAccess.Budget.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.Budget.Repositories;

public class BudgetItemRepository : IBudgetItemRepository
{
    private readonly IReadDb _readDb;

    public BudgetItemRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetItemDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@BudgetId",       dto.BudgetId);
        parameters.Add("@UserId",         dto.UserId);
        parameters.Add("@Label",          dto.Label);
        parameters.Add("@BudgetedAmount", dto.BudgetedAmount);
        parameters.Add("@DueDate",        dto.DueDate);
        parameters.Add("@Type",           dto.Type.ToString());
        parameters.Add("@BudgetItemId",   dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await writeDb.QueryAsync<dynamic, DynamicParameters>("budget.stp_CreateBudgetItem", parameters);

        return parameters.Get<int>("@BudgetItemId");
    }

    public async Task<BudgetItemDto?> GetByIdAsync(int budgetItemId)
    {
        var result = await _readDb.QueryAsync<BudgetItemDto, object>(
            "budget.stp_GetBudgetItemById",
            new { BudgetItemId = budgetItemId });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<BudgetItemDto>> GetByBudgetAsync(int budgetId)
    {
        return await _readDb.QueryAsync<BudgetItemDto, object>(
            "budget.stp_GetBudgetItemsByBudget",
            new { BudgetId = budgetId });
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetItemDto dto)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_UpdateBudgetItem",
            new
            {
                BudgetItemId   = dto.BudgetItemId,
                Label          = dto.Label,
                BudgetedAmount = dto.BudgetedAmount,
                DueDate        = dto.DueDate,
                ActualAmount   = dto.ActualAmount,
                ActualDate     = dto.ActualDate,
                Type           = dto.Type.ToString()
            });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int budgetItemId)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_DeleteBudgetItem",
            new { BudgetItemId = budgetItemId });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
