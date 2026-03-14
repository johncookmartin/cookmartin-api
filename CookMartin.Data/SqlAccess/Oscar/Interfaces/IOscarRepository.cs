using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Oscar;

namespace CookMartin.Data.SqlAccess.Oscar.Interfaces;

public interface IOscarRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesWithNomineesAsync();
    Task UpsertPickAsync(IWriteDb writeDb, UpsertPickDto dto);
    Task SetWinnerAsync(IWriteDb writeDb, int nomineeId);
    Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync();
    Task<IEnumerable<UserResultDto>> GetUserResultsAsync(string userName);
    Task<IEnumerable<SubmissionDto>> GetSubmissionsAsync();
}
