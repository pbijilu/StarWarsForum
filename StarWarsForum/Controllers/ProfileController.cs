using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ProfileViewModels;

namespace StarWarsForum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IUploadService _uploadService;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService, IUploadService uploadService)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _uploadService = uploadService;
        }
        public IActionResult Detail(string id)
        {
            var user = _applicationUserService.GetByUserName(id);

            var model = new ProfileModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                BirthDate = user.BirthDate,
                IsDarkSided = user.IsDarkSided
            };

            return View(model);
        }
    }
}