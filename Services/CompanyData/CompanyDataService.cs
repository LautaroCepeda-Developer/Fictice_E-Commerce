using AutoMapper;
using DTOs.CompanyData;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CompanyData
{
    public class CompanyDataService(ICompanyDataRepository repository, IMapper mapper) : ICompanyDataService
    {
        private readonly ICompanyDataRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public List<string> Errors { get; } = [];

        public async Task<Models.CompanyData> Get()
        {
            var companyData = await _repository.GetById(1);

            if (companyData is null) return null;

            return companyData;
        }

        public async Task<decimal?> GetTax()
        {
            var tax = await _repository.GetTax();

            // A validator reacts to this value, and cancels the operation
            if (tax is null) return 0m;

            return tax;
        }

        public async Task<CompanyDataDTO> Add(CompanyDataInsertDTO insertDTO)
        {
            var companyData = _mapper.Map<Models.CompanyData>(insertDTO);

            await _repository.Add(companyData);
            await _repository.Save();

            var companyDataDTO = _mapper.Map<CompanyDataDTO>(companyData);

            return companyDataDTO;
        }

        public async Task<CompanyDataDTO?> Update(CompanyDataUpdateDTO updateDTO)
        {
            var companyData = await _repository.GetById(1);

            if (companyData is null) return null;

            companyData = _mapper.Map(updateDTO, companyData);

            _repository.Update(companyData);
            await _repository.Save();

            var companyDataDTO = _mapper.Map<CompanyDataDTO>(companyData);

            return companyDataDTO;
        }

        public bool Validate(CompanyDataInsertDTO DTO)
        {
            // Checking if exist at least one record in the CompanyData table. If not, then allow the insertion
            if (!_repository.Get().Result.Any()) return true;

            Errors.Add("Already exists a record with the company data.");
            return false;
        }
    }
}
