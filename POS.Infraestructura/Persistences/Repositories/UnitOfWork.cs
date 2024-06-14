using POS.Infraestructura.Persistences.Context;
using POS.Infraestructura.Persistences.Interfaces;

namespace POS.Infraestructura.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWorK
    {
        private readonly POSContext _context;

        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public UnitOfWork(POSContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Provider = new ProviderRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
