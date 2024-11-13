using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserPersonalInformationRepository
    {
        public Task<IEnumerable<UserPersonalInformation>> Get();
        public Task<UserPersonalInformation?> GetById(int id);
        void Update(UserPersonalInformation userPersonalInformation);
        public Task Save();
        public IEnumerable<UserPersonalInformation> Search(Func<UserPersonalInformation, bool> filter);

    }
}
