using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.ProfileViewModels
{
    public class ProfileUsersModel
    {
        public IEnumerable<UserModel> UserList { get; set; }
    }
}
