using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Quiz;
using Dapper;
using System.Data;

namespace CookMartin.Data.SqlAccess.NoteCard.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly IReadDb _readDb;

    public QuizRepository(IReadDb readDb)
    {
        _readDb = readDb;
    }

    public async Task<int> CreateAsync(IWriteDb writeDb, CreateQuizDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CollectionId", dto.CollectionId);
        parameters.Add("@UserId", dto.UserId);
        parameters.Add("@QuizId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        var result = await writeDb.QueryAsync<int, dynamic>("note.stp_CreateQuiz", parameters);
        return parameters.Get<int>("@QuizId");
    }

    public async Task<IEnumerable<QuizInstanceDto>> GetInstancesByIdAsync(int quizId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@QuizId", quizId);
        var result = await _readDb.QueryAsync<QuizInstanceDto, dynamic>("note.stp_GetQuizInstancesByQuiz", parameters);
        return result;
    }

    public async Task<int> RecordAnswerAsync(IWriteDb writeDb, RecordQuizAnswerDto dto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NoteCardId", dto.NotecardId);
        parameters.Add("@QuizId", dto.QuizId);
        parameters.Add("@IsCorrect", dto.IsCorrect);
        var result = await writeDb.QueryAsync<int, dynamic>("note.stp_RecordQuizAnswer", parameters);
        return result.FirstOrDefault();
    }

    public async Task<int> CompleteAsync(IWriteDb writeDb, int quizId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@QuizId", quizId);

        var result = await writeDb.QueryAsync<dynamic, dynamic>("note.stp_CompleteQuiz", parameters);
        return result.FirstOrDefault()?.Score ?? 0;
    }

    public async Task<IEnumerable<QuizDto>> GetByUserAsync(string userID)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userID);

        return await _readDb.QueryAsync<QuizDto, dynamic>("note.stp_GetQuizzesByUser", parameters);
    }

    public async Task<QuizDto?> GetByIdAsync(int quizId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@QuizId", quizId);
        var result = await _readDb.QueryAsync<QuizDto, dynamic>("note.stp_GetQuizById", parameters);
        return result.FirstOrDefault();
    }

    public async Task<int> DeleteAsync(IWriteDb writeDb, int quizId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@QuizId", quizId);
        var result = await writeDb.QueryAsync<int, dynamic>("note.stp_DeleteQuiz", parameters);
        return result.FirstOrDefault();
    }
}
