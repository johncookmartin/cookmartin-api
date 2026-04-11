using CookMartin.Data.Models.Budget.RecurringItem;

namespace CookMartin.Budget.Services.Interfaces;

public interface IRecurringItemService
{
    Task<RecurringItemDto> CreateAsync(CreateRecurringItemDto dto);
    Task<RecurringItemDto?> GetByIdAsync(int recurringItemId);
    Task<IEnumerable<RecurringItemDto>> GetByCollectionAsync(int collectionId);
    Task<bool> UpdateAsync(UpdateRecurringItemDto dto);
    Task<bool> DeleteAsync(int recurringItemId);
}
