using AutoMapper;
using DTOs.Payment;
using DTOs.ProductSale;
using DTOs.Sale;
using Models;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.Sale
{
    public class SaleService(
        ISaleRepository saleRepository,
        IRepository<Models.Product> productRepository,
        IProductSaleService productSaleService, IPaymentService paymentService,
        IOrderNumberService orderNumberService, ITransactionNumberService transactionNumberService,
        ICompanyDataService companyDataService, IRepository<Models.UserBankAccount> bankAccountRepository,
        IMapper mapper) : ISaleService
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        private readonly IRepository<Models.Product> _productRepository = productRepository;

        private readonly IProductSaleService _productSaleService = productSaleService;
        private readonly IPaymentService _paymentService = paymentService;

        private readonly ICompanyDataService _companyDataService = companyDataService;
        private readonly IRepository<UserBankAccount> _bankAccountRepository = bankAccountRepository;

        private readonly IOrderNumberService _orderNumberService = orderNumberService;
        private readonly ITransactionNumberService _transactionNumberService = transactionNumberService;

        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<SaleDTO>> Get()
        {
            var result = await _saleRepository.Get();

            return result.Select(_mapper.Map<SaleDTO>);
        }

        public async Task<SaleDTO?> GetById(int id)
        {
            var result = await _saleRepository.GetById(id);

            if (result is null) return null;

            var saleDTO = _mapper.Map<SaleDTO>(result);

            return saleDTO;
        }

        public async Task<SaleDTO?> GetByOrderNumber(string orderNumber)
        {
            var result = await _saleRepository.GetByOrderNumber(orderNumber);

            if (result is null) return null;

            var saleDTO = _mapper.Map<SaleDTO>(result);

            return saleDTO;
        }

        public async Task<SaleDTO?> Delete(int id)
        {
            var sale = await _saleRepository.GetById(id);

            if (sale is null) return null;

            var saleDTO = _mapper.Map<SaleDTO>(sale);

            _saleRepository.Delete(sale);

            await _saleRepository.Save();

            return saleDTO;
        }

        private async Task<IEnumerable<ProductSaleInsertDTO>?> GetProductSaleList(int saleId, IEnumerable<Models.ProductInfo> productList)
        {
            IEnumerable<ProductSaleInsertDTO> productsOrdered = [];

            foreach (var product in productList)
            {
                var result = await _productRepository.GetById(product.Id);
                if (result is null) return null;

                // If the quantity to be purchased is less than the existing quantity
                if (result.Quantity < product.Quantity) return null;

                var productSale = new ProductSaleInsertDTO
                {
                    SaleId = saleId,
                    ProductId = product.Id,
                    PricePerUnit = result.Price,
                    ProductQuantity = product.Quantity
                };

                productsOrdered = productsOrdered.Append(productSale);
            }

            return productsOrdered;
        }

        private async Task<object?> MakeSale(int saleId, IEnumerable<Models.ProductInfo> productList, int paymentMethodId)
        {
            // Getting the tax
            var tax = await _companyDataService.GetTax();
            if (tax is null) // If true: Deletes the sale and returns an error in the request
            {
                await Delete(saleId);
                return "An error ocurred getting the tax.";
            }
            

            var productSaleList = await GetProductSaleList(saleId, productList);
            if (productSaleList is null) // If true: Deletes the sale and returns an error in the request
            {
                await Delete(saleId);
                return "An error ocurred validating the product list. (Invalid products or invalid amount of units)";
            }
            

            foreach (var productSaleOfList in productSaleList)
            {
                // Adding the sale to the db and saving (using service)
                var productSale = await _productSaleService.Add(productSaleOfList);

                // Getting the product data (to reach get the seller id)
                var productData = await _productRepository.GetById(productSale.ProductId);
                if (productData is null) // If true: Deletes the sale and returns an error in the request
                {
                    await Delete(saleId);
                    return $"An error ocurred getting a product with id {productSale.ProductId}.";
                }
               
                // Updating the unit amount of the product
                productData.Quantity -= productSaleOfList.ProductQuantity;

                // checking if units remain
                if (productData.Quantity == 0) productData.IsActive = false;

                // Updating the product publication
                _productRepository.Update(productData);
                await _productRepository.Save();

                // Getting the seller bank account
                var sellerBankAccount = _bankAccountRepository.Search(s => s.UserId == productData.SellerId && s.IsMainAccount).FirstOrDefault();
                if (sellerBankAccount is null)
                { // If true: Deletes the sale, reverse product information and returns an error in the request
                    await Delete(saleId);

                    // Returning the product data to the original state
                    productData.Quantity += productSaleOfList.ProductQuantity;
                    if (productData.Quantity > 0) productData.IsActive = true;
                    _productRepository.Update(productData);
                    await _productRepository.Save();
                    
                    return $"An error ocurred getting the main bank account of the seller with id {productData.SellerId}.";
                }
                 

                var paymentInsertDTO = new PaymentInsertDTO
                {
                    ProductSaleId = productSale.Id,
                    // With tax applied
                    TransactionValue = productSale.TotalPrice - (productSale.TotalPrice * ((decimal)tax/100)),
                    PaymentMethodId = paymentMethodId,
                    TransactionNumber = _transactionNumberService.GenerateTransactionNumber(),
                    Tax = (decimal)tax,
                    SellerBankAccount = sellerBankAccount.AccountNumber
                };

                await _paymentService.Add(paymentMethodId, paymentInsertDTO);
            }

            return true;
        }

        public async Task<object?> Add(SaleInsertDTO insertDTO, IEnumerable<Models.ProductInfo> productList, int paymentMethodId)
        {
            // Generating a OrderNumber
            insertDTO.OrderNumber = _orderNumberService.GenerateOrderNumber();

            // Mapping the entity
            var sale = _mapper.Map<Models.Sale>(insertDTO);

            // Adding the sale to the DB
            await _saleRepository.Add(sale);
            await _saleRepository.Save();

            var saleDTO = _mapper.Map<SaleDTO>(sale);

            // Returns true if the sale sale was created correctly
            // Returns string (with the error) if an error ocurred during the sale creation
            var saleResult = await MakeSale(saleDTO.Id, productList, paymentMethodId);

            if (saleResult is not bool) return saleResult;

            var productSale = await _productSaleService.GetAllBySaleId(saleDTO.Id);

            decimal totalValue = 0m;

            foreach (var product in productSale)
            {
                totalValue += product.TotalPrice;
            }

            sale.SaleValue = totalValue;

            _saleRepository.Update(sale);
            await _saleRepository.Save();

            return saleDTO;
        }
    }
}
