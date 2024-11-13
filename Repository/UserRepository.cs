using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class UserRepository(DatabaseContext context) : IUserRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<User>> Get() => await _context.Users.Include(u => u.PersonalInformation).ToListAsync();

        // Get one record by ID with his related data (async)
        public async Task<User?> GetById(int id) => await _context.Users
            .Include(u => u.PersonalInformation)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByNickname(string nickname) => await _context.Users
            .Include(u => u.PersonalInformation)
            .FirstOrDefaultAsync(u => u.Nickname == nickname);

        public async Task<User?> GetByEmail(string email) => await _context.Users
            .Include(u => u.PersonalInformation)
            .FirstOrDefaultAsync(u => u.Email == email);

        // Add one record (async)
        public async Task Add(User user) => await _context.Users.AddAsync(user);

        // Update one record
        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Users.Entry(user).State = EntityState.Modified;
        }

        // Delete one record
        // Because the cascade delete directión, this receives an user, but removes the personal information of the user to cascade delete de user.
        public void Delete(User user) => _context.UserPersonalInformation.Remove(user.PersonalInformation);
        
        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<User> Search(Func<User, bool> filter) => _context.Users.Include(u=> u.PersonalInformation).Where(filter);
    }
}
