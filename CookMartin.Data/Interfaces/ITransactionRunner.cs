namespace CookMartin.Data.Interfaces;

public interface ITransactionRunner
{
    Task ExecuteAsync(Func<IWriteDb, Task> action);
    Task<T> ExecuteAsync<T>(Func<IWriteDb, Task<T>> action);
}