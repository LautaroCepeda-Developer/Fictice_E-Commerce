using DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IInfractionService
    {
        public Task<int?> GetUserInfractions(int id);
        public Task<UserGetPublicDTO?> AddOrRemoveInfractions(int id, int quantity);
    }
}
