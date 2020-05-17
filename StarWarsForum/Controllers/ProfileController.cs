using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Lib;
using StarWarsForum.Models.ProfileViewModels;

namespace StarWarsForum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUserService applicationUserService, IUploadService uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _uploadService = uploadService;
            _configuration = configuration;
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
                BirthDate = user.BirthDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage (IFormFile file)
        {
            
            var userName = _userManager.GetUserName(User);

            await _applicationUserService.SetProfileImage(userName, await Uploader.UploadImage("profile-images", file, _configuration, _uploadService));

            return RedirectToAction("Detail", "Profile", new { id = userName });
        }

        public IActionResult Users(string searchTerm = null)
        {
            if (!User.IsInRole("Admin"))
                RedirectToAction("Index", "Home");

            var users = searchTerm == null ? _applicationUserService.GetAll() : _applicationUserService.GetFilteredUsers(searchTerm); 

            var model = new ProfileUsersModel
            {
                UserList = users.Select(user => new UserModel
                                 {
                                     UserName = user.UserName,
                                     BirthDate = user.BirthDate,
                                     BannedTill = user.LockoutEnd
                                 }) 
            };

            return View(model);
        }

        public IActionResult Ban(string id, int banFor)
        {
            if (!User.IsInRole("Admin"))
                RedirectToAction("Index", "Home");

            var user = _applicationUserService.GetByUserName(id);

            var model = new UserModel
            {
                UserName = id,
                BannedTill = user.LockoutEnd,
                BanFor = banFor
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Ban(UserModel model)
        {
            switch (model.BanFor)
            {
                case 1:
                    await _applicationUserService.BanFor10Mins(model.UserName);
                    break;
                case 2:
                    await _applicationUserService.BanForDays(model.UserName);
                    break;
                case 3:
                    await _applicationUserService.BanForMonth(model.UserName);
                    break;
                case 4:
                    await _applicationUserService.PermanentBan(model.UserName);
                    break;
            }

            return RedirectToAction("Detail", "Profile", new { id = model.UserName });
        }
    }
}