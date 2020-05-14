using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace StarWarsForum.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDarkSided { get; set; }
    }
}
