using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Collection;
using CookMartin.Data.Models.NoteCard.Notecard;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.NoteCard.Services.Interfaces;

namespace CookMartin.NoteCard.Services;

public class NotecardService : INotecardService
{
    private readonly Random _rng = new Random();
    private readonly INotecardRepository _repository;
    private readonly IReadDb _readDb;
    private readonly ITransactionRunner _transactionRunner;
    private readonly ICollectionService _collectionService;

    public NotecardService(INotecardRepository repository, IReadDb readDb, ITransactionRunner transactionRunner, ICollectionService collectionService)
    {
        _repository = repository;
        _readDb = readDb;
        _transactionRunner = transactionRunner;
        _collectionService = collectionService;
    }

    public async Task<NotecardDto> CreateNotecardAsync(CreateNotecardDto dto)
    {
        int notecardId = await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            return await _repository.CreateAsync(writeDb, dto);

        });
        var notecard = await _repository.GetByIdAsync(notecardId);
        return notecard ?? throw new InvalidOperationException("Failed to create notecard");
    }

    public async Task<NotecardDto?> GetNotecardByIdAsync(int notecardId)
    {
        return await _repository.GetByIdAsync(notecardId);
    }

    public async Task<IEnumerable<NotecardDto>> GetNotecardsByCollectionAsync(CollectionDto collection)
    {
        IEnumerable<NotecardDto> notecards = await _repository.GetByCollectionAsync(collection.CollectionId);
        if (notecards.Count() < 5)
        {
            if (collection.UserId == "guest" && collection.Name == "Default Guest Collection")
            {
                int addedcards = await _collectionService.CreateGuestCollectionAsync();
                notecards = await _repository.GetByCollectionAsync(collection.CollectionId);
            }
        }
        return notecards;
    }

    public async Task<bool> UpdateNotecardAsync(int notecardId, UpdateNotecardDto dto)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.UpdateAsync(writeDb, dto);
            return rowsAffected > 0;
        });
    }

    public async Task<bool> DeleteNotecardAsync(int notecardId)
    {
        return await _transactionRunner.ExecuteAsync(async writeDb =>
        {
            var rowsAffected = await _repository.DeleteAsync(writeDb, notecardId);
            return rowsAffected > 0;

        });
    }

    public async Task<QuizNotecardDto?> GetNextNoteCardAsync(int quizId)
    {
        var notecards = await _repository.GetNotecardsByQuizIdAsync(quizId);
        if (notecards.Count() < 1) return null;

        var nextNotecard = notecards.ElementAt(_rng.Next(notecards.Count()));
        return nextNotecard;
    }
}
