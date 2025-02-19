namespace TaskProActive.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }
        Task<int> CompleteAsync();
    }
}
