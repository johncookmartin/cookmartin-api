using CookMartin.Data.Models.Oscar;

namespace CookMartin.Oscar.Services.Interfaces;

public interface IOscarService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesWithNomineesAsync();
    Task SubmitPicksAsync(string userName, IEnumerable<int> nomineeIds);
    Task SetWinnerAsync(int nomineeId);
    Task ClearWinnerAsync(int nomineeId);
    Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync();
    Task<IEnumerable<UserResultDto>> GetUserResultsAsync(string userName);
    Task<IEnumerable<SubmissionDto>> GetSubmissionsAsync();
}
