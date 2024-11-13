using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class CompanyDataRepository(DatabaseContext context) : ICompanyDataRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<CompanyData>> Get() => await _context.CompanyData.ToListAsync();

        // Get one record by ID (async)
        public async Task<CompanyData?> GetById(int id)
        {
            var result = await _context.CompanyData.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Get tax value (async)
        public async Task<decimal?> GetTax()
        {
            var result = await GetById(1);

            if (result is null) return null;

            return result.Tax;
        }

        // Add one record (async)
        public async Task Add(CompanyData entity) => await _context.CompanyData.AddAsync(entity);

        // Update one record
        public void Update(CompanyData entity) => _context.CompanyData.Remove(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
