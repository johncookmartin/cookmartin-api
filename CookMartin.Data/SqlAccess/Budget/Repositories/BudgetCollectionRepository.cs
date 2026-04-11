using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Collection;
using CookMartin.Data.SqlAccess.Budget.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.Budget.Repositories;

public class BudgetCollectionRepository : IBudgetCollectionRepository
{
    private readonly IReadDb _readDb;

    public BudgetCollectionRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetCollectionDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@OwnerId",       dto.OwnerId);
        parameters.Add("@Name",          dto.Name);
        parameters.Add("@EmergencyFund", dto.EmergencyFund);
        parameters.Add("@CollectionId",  dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await writeDb.QueryAsync<dynamic, DynamicParameters>("budget.stp_CreateCollection", parameters);

        return parameters.Get<int>("@CollectionId");
    }

    public async Task<BudgetCollectionDto?> GetByIdAsync(int collectionId)
    {
        var rows = await _readDb.QueryAsync<dynamic, object>(
            "budget.stp_GetCollectionById",
            new { CollectionId = collectionId });

        return rows
            .GroupBy(r => (int)r.CollectionId)
            .Select(g =>
            {
                var first = g.First();
                return new BudgetCollectionDto
                {
                    CollectionId  = (int)first.CollectionId,
                    Name          = (string)first.Name,
                    EmergencyFund = (decimal)first.EmergencyFund,
                    OwnerId       = (string)first.OwnerId,
                    CreatedDate   = (DateTime)first.CreatedDate,
                    UpdatedDate   = (DateTime?)first.UpdatedDate,
                    IsDeleted     = (bool)first.IsDeleted,
                    Users         = g
                        .Where(r => r.CollectionUserId != null)
                        .Select(r => new BudgetCollectionUserDto
                        {
                            CollectionUserId = (int)r.CollectionUserId,
                            UserId           = (string)r.UserId
                        })
                        .ToList()
                };
            })
            .FirstOrDefault();
    }

    public async Task<IEnumerable<BudgetCollectionDto>> GetByUserAsync(string userId)
    {
        return await _readDb.QueryAsync<BudgetCollectionDto, object>(
            "budget.stp_GetCollectionsByUser",
            new { UserId = userId });
    }

    public async Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetCollectionDto dto)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_UpdateCollection",
            new
            {
                CollectionId  = dto.CollectionId,
                Name          = dto.Name,
                EmergencyFund = dto.EmergencyFund
            });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int collectionId)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_DeleteCollection",
            new { CollectionId = collectionId });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }

    public async Task<int> AddUserAsync(IWriteDb writeDb, int collectionId, string userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId",     collectionId);
        parameters.Add("@UserId",           userId);
        parameters.Add("@CollectionUserId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

        await writeDb.QueryAsync<dynamic, DynamicParameters>("budget.stp_AddCollectionUser", parameters);

        return parameters.Get<int>("@CollectionUserId");
    }

    public async Task<int> RemoveUserAsync(IWriteDb writeDb, int collectionId, string userId)
    {
        var result = await writeDb.QueryAsync<dynamic, object>(
            "budget.stp_RemoveCollectionUser",
            new { CollectionId = collectionId, UserId = userId });

        return result.FirstOrDefault()?.RowsAffected ?? 0;
    }
}
