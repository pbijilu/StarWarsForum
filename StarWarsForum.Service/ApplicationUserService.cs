using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BanFor10Mins(string userName)
        {
            GetByUserName(userName).LockoutEnd = DateTimeOffset.Now.AddMinutes(10);
            await _context.SaveChangesAsync();
        }

        public async Task BanForDays(string userName)
        {
            GetByUserName(userName).LockoutEnd = DateTimeOffset.Now.AddDays(7);
            await _context.SaveChangesAsync();
        }

        public async Task BanForMonth(string userName)
        {
            GetByUserName(userName).LockoutEnd = DateTimeOffset.Now.AddMonths(1);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetAll() =>
            _context.ApplicationUsers;

        public ApplicationUser GetByUserName(string userName) =>
            GetAll().First(user => user.UserName == userName);

        public IEnumerable<ApplicationUser> GetFilteredUsers(string searchTerm) =>
            GetAll().Where(user => user.NormalizedUserName.Contains(searchTerm.ToUpper()));

        public async Task PermanentBan(string userName)
        {
            GetByUserName(userName).LockoutEnd = DateTimeOffset.Now.AddYears(999);
            await _context.SaveChangesAsync();
        }

        public async Task SetProfileImage(string userName, Uri uri)
        {
            GetByUserName(userName).ProfileImageUrl = uri.AbsoluteUri;

            await _context.SaveChangesAsync();
        }
    }
}
