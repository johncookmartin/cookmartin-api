using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.Collection;

namespace CookMartin.Data.SqlAccess.Budget.Interfaces;

public interface IBudgetCollectionRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateBudgetCollectionDto dto);
    Task<BudgetCollectionDto?> GetByIdAsync(int collectionId);
    Task<IEnumerable<BudgetCollectionDto>> GetByUserAsync(string userId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateBudgetCollectionDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int collectionId);
    Task<int> AddUserAsync(IWriteDb writeDb, int collectionId, string userId);
    Task<int> RemoveUserAsync(IWriteDb writeDb, int collectionId, string userId);
}
