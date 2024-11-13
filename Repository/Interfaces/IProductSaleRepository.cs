using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository.Interfaces
{
    public interface IProductSaleRepository
    {
        Task<IEnumerable<ProductSale>> Get();
        Task<IEnumerable<ProductSale>> GetAllByProductId(int id);
        Task<ProductSale?> GetById(int id);
        Task<IEnumerable<ProductSale>> GetAllBySaleId(int id);
        Task Add(ProductSale entity);
        Task Save();
        IEnumerable<ProductSale> Search(Func<ProductSale, bool> filter);
    }
}
