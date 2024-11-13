using DTOs.BannedUser;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBannedUserService
    {

        public Task<IEnumerable<BannedUserDTO>> Get();

        public Task<BannedUserDTO?> GetById(int id);

        public Task<BannedUserInsertDTO> BanInsertPreview(int id, BanData banData);
        public Task<BannedUserDTO> Add(int id, BanData banData);

        public Task<BannedUserDTO?> Update(int id, BannedUserUpdateDTO updateDTO);

        public Task<BannedUserDTO?> Delete(int id);
    }
}
