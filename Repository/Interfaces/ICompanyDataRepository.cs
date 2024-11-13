using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICompanyDataRepository
    {
        Task<IEnumerable<CompanyData>> Get();
        Task<CompanyData?> GetById(int id);
        Task<decimal?> GetTax();
        Task Add(CompanyData entity);
        void Update(CompanyData entity);
        Task Save();
    }
}
