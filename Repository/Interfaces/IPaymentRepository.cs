using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> Get();
        Task<Payment?> GetById(int id);
        Task<Payment?> GetByTransactionNumber(string transactionNumber);
        Task Add(Payment entity);
        Task Save();
        IEnumerable<Payment> Search(Func<Payment, bool> filter);
    }
}
