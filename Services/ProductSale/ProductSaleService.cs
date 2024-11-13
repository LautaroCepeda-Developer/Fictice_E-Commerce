using AutoMapper;
using DTOs.ProductSale;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductSale
{
    public class ProductSaleService(IProductSaleRepository repository, IMapper mapper) : IProductSaleService
    {
        private readonly IProductSaleRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProductSaleDTO>> Get()
        {
            var result = await _repository.Get();

            return result.Select(_mapper.Map<ProductSaleDTO>);
        }

        public async Task<IEnumerable<ProductSaleDTO>> GetAllByProductId(int id)
        {
            var result = await _repository.GetAllByProductId(id);

            return result.Select(_mapper.Map<ProductSaleDTO>);
        }

        public async Task<ProductSaleDTO?> GetById(int id)
        {
            var result = await _repository.GetById(id);

            if (result is null) return null;

            var productSaleDTO = _mapper.Map<ProductSaleDTO>(result);

            return productSaleDTO;
        }

        public async Task<IEnumerable<ProductSaleDTO>> GetAllBySaleId(int id)
        {
            var result = await _repository.GetAllBySaleId(id);

            return result.Select(_mapper.Map<ProductSaleDTO>);
        }

        public async Task<ProductSaleDTO> Add(ProductSaleInsertDTO insertDTO)
        {
            var productSale = _mapper.Map<Models.ProductSale>(insertDTO);

            await _repository.Add(productSale);
            await _repository.Save();

            var productSaleDTO = _mapper.Map<ProductSaleDTO>(productSale);

            return productSaleDTO;
        }
    }
}
