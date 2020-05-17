using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Data
{
    public interface IApplicationUserService
    {
        public ApplicationUser GetByUserName(string userName);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> GetFilteredUsers(string searchTerm);

        Task SetProfileImage(string userName, Uri uri);
        Task BanFor10Mins(string userName);
        Task BanForDays(string userName);
        Task BanForMonth(string userName);
        Task PermanentBan(string userName);
        Task Unban(string userName);
    }
}
