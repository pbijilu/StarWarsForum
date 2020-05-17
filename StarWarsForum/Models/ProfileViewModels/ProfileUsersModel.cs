using System.Collections.Generic;

namespace StarWarsForum.Models.ProfileViewModels
{
    public class ProfileUsersModel
    {
        public IEnumerable<UserModel> UserList { get; set; }
    }
}
