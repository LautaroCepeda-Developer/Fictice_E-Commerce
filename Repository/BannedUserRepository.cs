using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class BannedUserRepository(DatabaseContext context) : IRepository<BannedUser>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<BannedUser>> Get() => await _context.BannedUsers.ToListAsync();

        // Get one record by ID (async)
        public async Task<BannedUser?> GetById(int id)
        {
            var result = await _context.BannedUsers.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(BannedUser entity) => await _context.BannedUsers.AddAsync(entity);

        // Update one record
        public void Update(BannedUser entity)
        {
            _context.BannedUsers.Attach(entity);
            _context.BannedUsers.Entry(entity).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(BannedUser entity) => _context.BannedUsers.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<BannedUser> Search(Func<BannedUser, bool> filter) => _context.BannedUsers.Where(filter);
    }
}
