using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class ProductSaleRepository(DatabaseContext context) : IProductSaleRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<ProductSale>> Get() => await _context.ProductsSales.ToListAsync();

        // Get records by ProductId (async)
        public async Task<IEnumerable<ProductSale>> GetAllByProductId(int id) => await _context.ProductsSales.Where(ps => ps.ProductId == id).ToListAsync();

        // Get records by SaleId (async)
        public async Task<IEnumerable<ProductSale>> GetAllBySaleId(int id) => await _context.ProductsSales.Where(ps => ps.SaleId == id).ToListAsync();

        // Get one record by ID (async)
        public async Task<ProductSale?> GetById(int id)
        {
            var result = await _context.ProductsSales.FindAsync(id);

            if (result is null) return null;

            return result;
        }

        // Get one record by SaleId
        public async Task<ProductSale?> GetBySaleId(int id)
        {
            var result = await _context.ProductsSales.FirstOrDefaultAsync(ps => ps.SaleId == id);

            if (result is null) return null;

            return result;
        }

        // Add one record (async)
        public async Task Add(ProductSale entity) => await _context.ProductsSales.AddAsync(entity);

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<ProductSale> Search(Func<ProductSale, bool> filter) => _context.ProductsSales.Where(filter);
    }
}
