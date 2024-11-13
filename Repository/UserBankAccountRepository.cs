using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class UserBankAccountRepository(DatabaseContext context) : IRepository<UserBankAccount>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<UserBankAccount>> Get() => await _context.UserBankAccounts.ToListAsync();

        // Get one record by ID (async)
        public async Task<UserBankAccount?> GetById(int id) => await _context.UserBankAccounts.FindAsync(id);

        // Add one record (async)
        public async Task Add(UserBankAccount userBankAccount) => await _context.UserBankAccounts.AddAsync(userBankAccount);

        // Update one record
        public void Update(UserBankAccount userBankAccount)
        {
            _context.UserBankAccounts.Attach(userBankAccount);
            _context.UserBankAccounts.Entry(userBankAccount).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(UserBankAccount userBankAccount) => _context.UserBankAccounts.Remove(userBankAccount);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search conditions
        public IEnumerable<UserBankAccount> Search(Func<UserBankAccount, bool> filter) => _context.UserBankAccounts.Where(filter);
    }
}
