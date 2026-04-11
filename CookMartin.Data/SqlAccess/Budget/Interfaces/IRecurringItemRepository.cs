using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Budget.RecurringItem;

namespace CookMartin.Data.SqlAccess.Budget.Interfaces;

public interface IRecurringItemRepository
{
    Task<int> CreateAsync(IWriteDb writeDb, CreateRecurringItemDto dto);
    Task<RecurringItemDto?> GetByIdAsync(int recurringItemId);
    Task<IEnumerable<RecurringItemDto>> GetByCollectionAsync(int collectionId);
    Task<int> UpdateAsync(IWriteDb writeDb, UpdateRecurringItemDto dto);
    Task<int> DeleteAsync(IWriteDb writeDb, int recurringItemId);
}
