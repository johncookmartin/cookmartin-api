using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Quiz;
using CookMartin.Data.SqlAccess.NoteCard.Repositories;

namespace CookMartin.NoteCard.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _repository;
    private readonly ITransactionRunner _transactionRunner;

    public QuizService(IQuizRepository repository, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _transactionRunner = transactionRunner;
    }

    public async Task<int> CreateQuizAsync(CreateQuizDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            return await _repository.CreateAsync(writeDb, dto);
        });
    }

    public async Task<bool> RecordAnswerAsnyc(RecordQuizAnswerDto dto)
    {
        var isComplete = await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.RecordAnswerAsync(writeDb, dto);
            bool success = rowsAffected > 0;
            if (!success)
            {
                throw new InvalidOperationException("Failed to record answer");
            }

            IEnumerable<QuizInstanceDto> quizInstances = await _repository.GetInstancesByIdAsync(dto.quizId);
            int remainingQuestions = quizInstances.Count(i => i.AnsweredDate == null);

            if (remainingQuestions < 1)
            {
                int score = await _repository.CompleteAsync(writeDb, dto.quizId);
                return true;
            }

            return false;
        });
        return isComplete;
    }

    public async Task<IEnumerable<QuizInstanceDto>> GetInstancesByIdAsync(int quizId)
    {
        return await _repository.GetInstancesByIdAsync(quizId);
    }

    public async Task<IEnumerable<QuizDto>> GetQuizzesByUserAsync(string userId)
    {
        return await _repository.GetByUserAsync(userId);
    }

    public async Task<QuizDto?> GetQuizByIdAsync(int quizId)
    {
        return await _repository.GetByIdAsync(quizId);
    }

    public async Task<bool> DeleteQuizAsync(int quizId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, quizId);
            return rowsAffected > 0;
        });
    }
}
