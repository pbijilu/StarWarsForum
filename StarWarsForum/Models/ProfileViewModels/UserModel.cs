using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.ProfileViewModels
{
    public class UserModel
    {
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTimeOffset? BannedTill { get; set; }
        public int BanFor { get; set; }
    }
}
