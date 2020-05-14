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
        public IEnumerable<ApplicationUser> GetAll() =>
            _context.ApplicationUsers;

        public ApplicationUser GetByUserName(string userName) =>
            GetAll().First(user => user.UserName == userName);

        public async Task SetProfileImage(string userName, Uri uri)
        {
            GetByUserName(userName).ProfileImageUrl = uri.AbsoluteUri;

            await _context.SaveChangesAsync();
        }
    }
}
