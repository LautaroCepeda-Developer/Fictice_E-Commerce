using AutoMapper;
using DTOs.PaymentMethod;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentMethod
{
    public class PaymentMethodService(IRepository<Models.PaymentMethod> repository, IMapper mapper) : ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO>
    {
        private readonly IRepository<Models.PaymentMethod> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public List<string> Errors { get; } = [];

        public async Task<IEnumerable<PaymentMethodDTO>> Get()
        {
            var paymentMethods = await _repository.Get();

            return paymentMethods.Select(_mapper.Map<PaymentMethodDTO>);
        }

        public async Task<PaymentMethodDTO?> GetById(int id)
        {
            var paymentMethod = await _repository.GetById(id);

            if (paymentMethod is null) return null;

            var paymentMethodDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);

            return paymentMethodDTO;
        }

        public async Task<PaymentMethodDTO> Add(PaymentMethodInsertDTO insertDTO)
        {
            var paymentMethod = _mapper.Map<Models.PaymentMethod>(insertDTO);

            await _repository.Add(paymentMethod);
            await _repository.Save();

            var paymentMethodDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);

            return paymentMethodDTO;
        }

        public async Task<PaymentMethodDTO?> Update(int id, PaymentMethodUpdateDTO updateDTO)
        {
            var paymentMethod = await _repository.GetById(id);

            if (paymentMethod is null) return null;

            paymentMethod = _mapper.Map(updateDTO, paymentMethod);

            _repository.Update(paymentMethod);
            await _repository.Save();

            var paymentMethodDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);

            return paymentMethodDTO;
        }

        public async Task<PaymentMethodDTO?> Delete(int id)
        {
            var paymentMethod = await _repository.GetById(id);

            if (paymentMethod is null) return null;

            _repository.Delete(paymentMethod);
            await _repository.Save();

            var paymentMethodDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);

            return paymentMethodDTO;
        }

        public bool Validate(PaymentMethodInsertDTO DTO)
        {
            if (!_repository.Search(pm => pm.Name == DTO.Name).Any()) return true;

            Errors.Add("This payment method already exists.");
            return false;
        }

        public bool Validate(PaymentMethodUpdateDTO DTO)
        {
            if (!_repository.Search(pm => pm.Name == DTO.Name && pm.Id != DTO.Id).Any()) return true;

            Errors.Add("This payment method already exists.");
            return false;
        }
    }
}
