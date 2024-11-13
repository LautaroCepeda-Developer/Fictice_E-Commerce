using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class CategoryRepository(DatabaseContext context) : IRepository<Category>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<Category>> Get() => await _context.Categories.ToListAsync();

        // Get one record by ID (async)
        public async Task<Category?> GetById(int id)
        {
            var result = await _context.Categories.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(Category entity) => await _context.Categories.AddAsync(entity);

        // Update one record
        public void Update(Category entity)
        {
            _context.Categories.Attach(entity);
            _context.Categories.Entry(entity).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(Category entity) => _context.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Save the changes (async)
        public IEnumerable<Category> Search(Func<Category, bool> filter) => _context.Categories.Where(filter);
    }
}
