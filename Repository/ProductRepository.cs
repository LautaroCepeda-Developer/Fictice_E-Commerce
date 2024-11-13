using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class ProductRepository(DatabaseContext context) : IRepository<Product>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<Product>> Get() => await _context.Products.ToListAsync();

        // Get one record by ID (async)
        public async Task<Product?> GetById(int id)
        {
            var result = await _context.Products.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(Product entity) => await _context.Products.AddAsync(entity);

        // Update one record
        public void Update(Product entity)
        {
            _context.Products.Attach(entity);
            _context.Products.Entry(entity).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(Product entity) => _context.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<Product> Search(Func<Product, bool> filter) => _context.Products.Where(filter);
    }
}
