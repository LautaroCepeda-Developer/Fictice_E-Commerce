using DTOs.Sale;

namespace Services.Interfaces
{
    public interface ISaleService
    {

        public Task<IEnumerable<SaleDTO>> Get();

        public Task<SaleDTO?> GetById(int id);
        public Task<SaleDTO?> GetByOrderNumber(string orderNumber);
        public Task<object?> Add(SaleInsertDTO insertDTO, IEnumerable<Models.ProductInfo> productList, int paymentMethodId);
        public Task<SaleDTO?> Delete(int id);
    }
}
