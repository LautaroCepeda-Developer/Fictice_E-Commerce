using DTOs.Category;
using DTOs.CompanyData;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICompanyDataService
    {
        public List<string> Errors { get; }
        public Task<Models.CompanyData> Get();
        public Task<decimal?> GetTax();
        public Task<CompanyDataDTO> Add(CompanyDataInsertDTO insertDTO);
        public Task<CompanyDataDTO?> Update(CompanyDataUpdateDTO updateDTO);
        public bool Validate(CompanyDataInsertDTO DTO);
    }
}
