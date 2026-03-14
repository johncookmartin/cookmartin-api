using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Oscar;
using CookMartin.Data.SqlAccess.Oscar.Interfaces;
using CookMartin.Oscar.Services.Interfaces;

namespace CookMartin.Oscar.Services;

public class OscarService : IOscarService
{
    private readonly IOscarRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public OscarService(IOscarRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesWithNomineesAsync()
    {
        return await _repository.GetCategoriesWithNomineesAsync();
    }

    public async Task SubmitPicksAsync(string userName, IEnumerable<int> nomineeIds)
    {
        await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            foreach (var nomineeId in nomineeIds)
                await _repository.UpsertPickAsync(writeDb, new UpsertPickDto { UserName = userName, NomineeId = nomineeId });
        });
    }

    public async Task SetWinnerAsync(int nomineeId)
    {
        await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            await _repository.SetWinnerAsync(writeDb, nomineeId);
        });
    }

    public async Task ClearWinnerAsync(int nomineeId)
    {
        await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            await _repository.ClearWinnerAsync(writeDb, nomineeId);
        });
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync()
    {
        return await _repository.GetLeaderboardAsync();
    }

    public async Task<IEnumerable<UserResultDto>> GetUserResultsAsync(string userName)
    {
        return await _repository.GetUserResultsAsync(userName);
    }

    public async Task<IEnumerable<SubmissionDto>> GetSubmissionsAsync()
    {
        return await _repository.GetSubmissionsAsync();
    }
}
