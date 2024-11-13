using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User?> GetById(int id);
        Task<User?> GetByNickname(string nickname);
        Task<User?> GetByEmail(string email);
        Task Add(User entity);
        void Update(User entity);
        void Delete(User entity);
        Task Save();
        IEnumerable<User> Search(Func<User, bool> filter);
    }
}
