using CookMartin.Data.Interfaces;
using CookMartin.Data.Models.NoteCard.Notecard;
using CookMartin.Data.SqlAccess.NoteCard.Interfaces;
using CookMartin.NoteCard.Services.Interfaces;

namespace CookMartin.NoteCard.Services;

public class NotecardService : INotecardService
{
    private readonly INotecardRepository _repository;
    private readonly IReadDb _readDb;
    private readonly ITransactionRunner _transactionRunner;

    public NotecardService(INotecardRepository repository, IReadDb readDb, ITransactionRunner transactionRunner)
    {
        _repository = repository;
        _readDb = readDb;
        _transactionRunner = transactionRunner;
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

    public async Task<IEnumerable<NotecardDto>> GetNotecardsByCollectionAsync(int collectionId)
    {
        return await _repository.GetByCollectionAsync(collectionId);
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
}
