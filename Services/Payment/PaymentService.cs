using AutoMapper;
using DTOs.Payment;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Payment
{
    public class PaymentService(IPaymentRepository repository, IMapper mapper) : IPaymentService
    {
        private readonly IPaymentRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PaymentDTO>> Get()
        {
            var result = await _repository.Get();

            return result.Select(_mapper.Map<PaymentDTO>);
        }

        public async Task<PaymentDTO?> GetById(int id)
        {
            var result = await _repository.GetById(id);

            if (result is null) return null;

            var paymentDTO = _mapper.Map<PaymentDTO>(result);

            return paymentDTO;
        }

        public async Task<PaymentDTO?> GetByTransactionNumber(string transactionNumber)
        {
            var result = await _repository.GetByTransactionNumber(transactionNumber);

            if (result is null) return null;

            var paymentDTO = _mapper.Map<PaymentDTO>(result);

            return paymentDTO;
        }

        public async Task<PaymentDTO> Add(int id, PaymentInsertDTO insertDTO)
        {
            var payment = _mapper.Map<Models.Payment>(insertDTO);

            await _repository.Add(payment);
            await _repository.Save();

            var paymentDTO = _mapper.Map<PaymentDTO>(payment);

            return paymentDTO;
        }
    }
}
