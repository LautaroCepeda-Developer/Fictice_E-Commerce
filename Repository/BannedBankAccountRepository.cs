using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class BannedBankAccountRepository(DatabaseContext context) : IRepository<BannedBankAccount>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<BannedBankAccount>> Get() => await _context.BannedBankAccounts.ToListAsync();

        // Get one record by ID (async)
        public async Task<BannedBankAccount?> GetById(int id)
        {
            var result = await _context.BannedBankAccounts.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(BannedBankAccount entity) => await _context.BannedBankAccounts.AddAsync(entity);

        // Update one record
        public void Update(BannedBankAccount entity)
        {
            _context.BannedBankAccounts.Attach(entity);
            _context.BannedBankAccounts.Entry(entity).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(BannedBankAccount entity) => _context.BannedBankAccounts.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<BannedBankAccount> Search(Func<BannedBankAccount, bool> filter) => _context.BannedBankAccounts.Where(filter);
    }
}
