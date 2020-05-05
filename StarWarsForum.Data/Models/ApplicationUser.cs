using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace StarWarsForum.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
    }
}
