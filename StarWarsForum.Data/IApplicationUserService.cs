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

        Task SetProfileImage(string userName, Uri uri);
    }
}
