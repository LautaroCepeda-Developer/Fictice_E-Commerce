using AutoMapper;
using DTOs.BannedUser;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Ban
{
    public class BannedBankAccountService(IRepository<BannedBankAccount> bannedRepository, IRepository<UserBankAccount> bankAccountRepository, IMapper mapper) : IBannedBankAccountService
    {
        private readonly IRepository<BannedBankAccount> _bannedRepository = bannedRepository;
        private readonly IRepository<UserBankAccount> _bankAccountRepository = bankAccountRepository;
        private readonly IMapper _mapper = mapper;


        public async Task<IEnumerable<BannedBankAccountDTO>> Get()
        {
            var bannedBankAccount = await _bannedRepository.Get();

            return bannedBankAccount.Select(_mapper.Map<BannedBankAccountDTO>);
        }

        public async Task<BannedBankAccountDTO?> GetById(int id)
        {
            var bannedBankAccount = await _bannedRepository.GetById(id);

            if (bannedBankAccount is null) return null;

            var bannedBankAccountDTO = _mapper.Map<BannedBankAccountDTO>(bannedBankAccount);

            return bannedBankAccountDTO;
        }

        public async Task<BannedBankAccountInsertDTO> BanInsertPreview(int id, BanData banData)
        {
            var bankAccount = await _bankAccountRepository.GetById(id);

            if (bankAccount is null) return null;

            var bannedBankAccount = _mapper.Map<BannedBankAccount>(bankAccount);
            bannedBankAccount.BanDateTimeEnd = banData.BanDateTimeEnd;
            bannedBankAccount.BanMotive = banData.BanMotive;

            return _mapper.Map<BannedBankAccountInsertDTO>(bannedBankAccount);
        }

        public async Task<BannedBankAccountDTO> Add(int id, BanData banData)
        {
            var bankAccount = await _bankAccountRepository.GetById(id);

            if (bankAccount is null) return null;

            var bannedBankAccount = _mapper.Map<BannedBankAccount>(bankAccount);
            bannedBankAccount.BanDateTimeEnd = banData.BanDateTimeEnd;
            bannedBankAccount.BanMotive = banData.BanMotive;

            await _bannedRepository.Add(bannedBankAccount);

            _bankAccountRepository.Delete(bankAccount);

            await _bannedRepository.Save();

            var bannedBankAccountDTO = _mapper.Map<BannedBankAccountDTO>(bannedBankAccount);

            return bannedBankAccountDTO;
        }

        public async Task<BannedBankAccountDTO?> Update(int id, BannedBankAccountUpdateDTO updateDTO)
        {
            var bannedBankAccount = await _bannedRepository.GetById(id);

            if (bannedBankAccount is null) return null;

            bannedBankAccount = _mapper.Map(updateDTO, bannedBankAccount);

            _bannedRepository.Update(bannedBankAccount);
            await _bannedRepository.Save();

            var bannedBankAccountDTO = _mapper.Map<BannedBankAccountDTO>(bannedBankAccount);

            return bannedBankAccountDTO;
        }

        public async Task<BannedBankAccountDTO?> Delete(int id)
        {
            var bannedBankAccount = await _bannedRepository.GetById(id);

            if (bannedBankAccount is null) return null;

            var bannedBankAccountDTO = _mapper.Map<BannedBankAccountDTO>(bannedBankAccount);

            _bannedRepository.Delete(bannedBankAccount);

            await _bannedRepository.Save();

            return bannedBankAccountDTO;
        }
    }
}
