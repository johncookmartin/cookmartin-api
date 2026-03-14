using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.Oscar;
using CookMartin.Data.SqlAccess.Oscar.Interfaces;
using Dapper;

namespace CookMartin.Data.SqlAccess.Oscar;

public class OscarRepository : IOscarRepository
{
    private readonly IReadDb _readDb;

    public OscarRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesWithNomineesAsync()
    {
        var rows = await _readDb.QueryAsync<dynamic, dynamic>("oscar.stp_GetCategoriesWithNominees", new { });

        return rows
            .GroupBy(r => (int)r.CategoryId)
            .Select(g =>
            {
                var first = g.First();
                return new CategoryDto
                {
                    CategoryId   = (int)first.CategoryId,
                    CategoryName = (string)first.CategoryName,
                    DisplayOrder = (int)first.DisplayOrder,
                    Nominees     = g.Select(r => new NomineeDto
                    {
                        NomineeId   = (int)r.NomineeId,
                        CategoryId  = (int)r.CategoryId,
                        NomineeName = (string)r.NomineeName,
                        IsWinner    = (bool)r.IsWinner
                    }).ToList()
                };
            })
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.CategoryId);
    }

    public async Task UpsertPickAsync(IWriteDb writeDb, UpsertPickDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserName",  dto.UserName);
        parameters.Add("@NomineeId", dto.NomineeId);

        await writeDb.ExecuteAsync("oscar.stp_UpsertPick", parameters);
    }

    public async Task SetWinnerAsync(IWriteDb writeDb, int nomineeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NomineeId", nomineeId);

        await writeDb.ExecuteAsync("oscar.stp_SetWinner", parameters);
    }

    public async Task ClearWinnerAsync(IWriteDb writeDb, int nomineeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NomineeId", nomineeId);
        await writeDb.ExecuteAsync("oscar.stp_ClearWinner", parameters);
    }

    public async Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync()
    {
        return await _readDb.QueryAsync<LeaderboardEntryDto, dynamic>("oscar.stp_GetLeaderboard", new { });
    }

    public async Task<IEnumerable<UserResultDto>> GetUserResultsAsync(string userName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserName", userName);

        return await _readDb.QueryAsync<UserResultDto, DynamicParameters>("oscar.stp_GetUserResults", parameters);
    }

    public async Task<IEnumerable<SubmissionDto>> GetSubmissionsAsync()
    {
        return await _readDb.QueryAsync<SubmissionDto, dynamic>("oscar.stp_GetSubmissions", new { });
    }
}
