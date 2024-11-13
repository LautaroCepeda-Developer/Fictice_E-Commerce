using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class SaleRepository(DatabaseContext context) : ISaleRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<Sale>> Get() => await _context.Sales.ToListAsync();

        // Get one record by ID (async)
        public async Task<Sale?> GetById(int id)
        {
            var result = await _context.Sales.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Get one record by OrderNumber (async)
        public async Task<Sale?> GetByOrderNumber(string orderNumber)
        {
            var result = await _context.Sales.FirstOrDefaultAsync(s => s.OrderNumber == orderNumber);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(Sale entity) => await _context.Sales.AddAsync(entity);

        public void Update(Sale entity) 
        {
            _context.Sales.Attach(entity);
            _context.Sales.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Sale entity) => _context.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<Sale> Search(Func<Sale, bool> filter) => _context.Sales.Where(filter);
    }
}
