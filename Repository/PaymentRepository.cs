using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class PaymentRepository(DatabaseContext context) : IPaymentRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<Payment>> Get() => await _context.Payments.ToListAsync();

        // Get one record by ID (async)
        public async Task<Payment?> GetById(int id)
        {
            var result = await _context.Payments.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Get one record by TransactionNumber (async)
        public async Task<Payment?> GetByTransactionNumber(string transactionNumber)
        {
            var result = await _context.Payments.FirstOrDefaultAsync(p => p.TransactionNumber == transactionNumber);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(Payment entity) => await _context.Payments.AddAsync(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<Payment> Search(Func<Payment, bool> filter) => _context.Payments.Where(filter);
    }
}
