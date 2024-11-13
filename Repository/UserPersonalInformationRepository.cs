using Microsoft.EntityFrameworkCore;
using Models;
using Models.Context;
using Repository.Interfaces;

namespace Repository
{
    public class UserPersonalInformationRepository(DatabaseContext context) : IUserPersonalInformationRepository
    {
        // DbContext
        private readonly DatabaseContext _context = context;

        // Get all records (async)
        public async Task<IEnumerable<UserPersonalInformation>> Get() => await _context.UserPersonalInformation.ToListAsync();

        // Get one record by ID (async)
        public async Task<UserPersonalInformation?> GetById(int id)
        {
            var user = await _context.Users.Include(u=>u.PersonalInformation).FirstOrDefaultAsync(u=> u.Id == id);

            if (user is null) return null;

            var userPersonalInfo = user.PersonalInformation;

            return userPersonalInfo;
        }

        // Update one record
        public void Update(UserPersonalInformation userPersonalInformation)
        {
            _context.UserPersonalInformation.Attach(userPersonalInformation);
            _context.UserPersonalInformation.Entry(userPersonalInformation).State = EntityState.Modified;
        }

        // Save the changes (async)
        public async Task Save() => await _context.SaveChangesAsync();

        // Get records by search condition
        public IEnumerable<UserPersonalInformation> Search(Func<UserPersonalInformation, bool> filter) => _context.UserPersonalInformation.Where(filter);
    }
}
