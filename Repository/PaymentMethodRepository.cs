using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class PaymentMethodRepository(DatabaseContext context) : IRepository<PaymentMethod>
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<PaymentMethod>> Get() => await _context.PaymentMethods.ToListAsync();

        // Get one record by ID (async)
        public async Task<PaymentMethod?> GetById(int id)
        {
            var result = await _context.PaymentMethods.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(PaymentMethod entity) => await _context.PaymentMethods.AddAsync(entity);

        // Update one record
        public void Update(PaymentMethod entity)
        {
            _context.PaymentMethods.Attach(entity);
            _context.PaymentMethods.Entry(entity).State = EntityState.Modified;
        }

        // Delete one record
        public void Delete(PaymentMethod entity) => _context.PaymentMethods.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<PaymentMethod> Search(Func<PaymentMethod, bool> filter) => _context.PaymentMethods.Where(filter);
    }
}
