using DTOs.BannedUser;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBannedBankAccountService
    {
        public Task<IEnumerable<BannedBankAccountDTO>> Get();

        public Task<BannedBankAccountDTO?> GetById(int id);

        public Task<BannedBankAccountInsertDTO> BanInsertPreview(int id, BanData banData);
        public Task<BannedBankAccountDTO> Add(int id, BanData banData);

        public Task<BannedBankAccountDTO?> Update(int id, BannedBankAccountUpdateDTO updateDTO);

        public Task<BannedBankAccountDTO?> Delete(int id);
    }
}
