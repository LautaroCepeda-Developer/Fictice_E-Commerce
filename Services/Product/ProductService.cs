using AutoMapper;
using DTOs.Product;
using Models;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Product
{
    public class ProductService(IRepository<Models.Product> repository, IMapper mapper) : ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO>
    {
        private readonly IRepository<Models.Product> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public List<string> Errors { get; } = [];
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var products = await _repository.Get();

            return products.Select(_mapper.Map<ProductDTO>);
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            var product = await _repository.GetById(id);

            if (product is null) return null;

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<ProductDTO> Add(ProductInsertDTO insertDTO)
        {
            var product = _mapper.Map<Models.Product>(insertDTO);

            await _repository.Add(product);
            await _repository.Save();

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<ProductDTO?> Update(int id, ProductUpdateDTO updateDTO)
        {
            var product = await _repository.GetById(id);

            if (product is null) return null;

            product = _mapper.Map(updateDTO, product);

            _repository.Update(product);
            await _repository.Save();

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            var product = await _repository.GetById(id);

            if (product is null) return null;

            var productDTO = _mapper.Map<ProductDTO>(product);

            _repository.Delete(product);

            await _repository.Save();

            return productDTO;
        }

        public bool Validate(ProductInsertDTO DTO)
        {
            if (!_repository.Search(p => p.Name == DTO.Name && p.SellerId == DTO.SellerId).Any()) return true;

            Errors.Add("This seller already have a product with this name.");
            return false;
        }

        public bool Validate(ProductUpdateDTO DTO)
        {
            if (_repository.Search(c => c.Name == DTO.Name && c.Id != DTO.Id).Any()) return true;

            Errors.Add("This seller already have a product with this name.");
            return false;
        }
    }
}
