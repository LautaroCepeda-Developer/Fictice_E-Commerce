using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> Get();
        Task<Sale?> GetById(int id);
        Task<Sale?> GetByOrderNumber (string orderNumber);
        Task Add(Sale entity);
        void Update(Sale entity);
        void Delete(Sale entity);

        Task Save();
        IEnumerable<Sale> Search(Func<Sale, bool> filter);
    }
}
