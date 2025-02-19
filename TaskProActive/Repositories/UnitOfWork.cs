using TaskProActive.Data;

namespace TaskProActive.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ITaskRepository TaskRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            TaskRepository = new TaskRepository(_context);
            UserRepository = new UserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
