using CookMartin.Data.Models.Budget.Collection;

namespace CookMartin.Budget.Services.Interfaces;

public interface IBudgetCollectionService
{
    Task<BudgetCollectionDto> CreateAsync(CreateBudgetCollectionDto dto);
    Task<BudgetCollectionDto?> GetByIdAsync(int collectionId);
    Task<IEnumerable<BudgetCollectionDto>> GetByUserAsync(string userId);
    Task<bool> UpdateAsync(UpdateBudgetCollectionDto dto);
    Task<bool> DeleteAsync(int collectionId);
    Task<int> AddUserAsync(int collectionId, string userId);
    Task<bool> RemoveUserAsync(int collectionId, string userId);
}
