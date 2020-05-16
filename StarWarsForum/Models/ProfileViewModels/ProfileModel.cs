using Microsoft.AspNetCore.Http;
using System;

namespace StarWarsForum.Models.ProfileViewModels
{
    public class ProfileModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl{ get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile ImageUpload { get; set; }
    }
}
