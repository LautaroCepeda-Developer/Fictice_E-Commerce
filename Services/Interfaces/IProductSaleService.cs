using DTOs.ProductSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductSaleService
    {
        public Task<IEnumerable<ProductSaleDTO>> Get();
        public Task<IEnumerable<ProductSaleDTO>> GetAllByProductId(int id);

        public Task<ProductSaleDTO?> GetById(int id);
        public Task<IEnumerable<ProductSaleDTO>> GetAllBySaleId(int id);

        public Task<ProductSaleDTO> Add(ProductSaleInsertDTO insertDTO);
    }
}
