using DTOs.BannedUser;
using System;
using System.Collections.Generic;
using DTOs.Payment;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<IEnumerable<PaymentDTO>> Get();

        public Task<PaymentDTO?> GetById(int id);

        public Task<PaymentDTO?> GetByTransactionNumber(string transactionNumber);

        public Task<PaymentDTO> Add(int id, PaymentInsertDTO insertDTO);

    }
}
