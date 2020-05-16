using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.TopicViewModels;
using StarWarsForum.Lib;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace StarWarsForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;

        public ForumController(IForumService forumService, IConfiguration configuration, IUploadService uploadService)
        {
            _forumService = forumService;
            _configuration = configuration;
            _uploadService = uploadService;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => ModelBuilders.BuildForumListing(forum));

            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        public IActionResult Topics(int id)
        {
            var forum = _forumService.GetById(id);
            var topics = forum.Topics;

            var topicListings = topics.Select(topic => ModelBuilders.BuildTopicListing(topic))
                                      .OrderByDescending(topic => topic.LastPostCreated);

            var model = new ForumTopicModel
            {
                Forum = ModelBuilders.BuildForumListing(forum),
                Topics = topicListings
            };

            if (TempData["TopicDeletedMessage"] != null)
            {
                model.TopicDeletedMessage = TempData["TopicDeletedMessage"] as string;
            }

            return View(model);
        }

        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return View("Error");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ForumCreateModel model)
        {
            if (model.ImageUpload != null)
            {
                Uri uri = await Uploader.UploadImage("forum-images", model.ImageUpload, _configuration, _uploadService);
                model.ForumImageUrl = uri.AbsoluteUri;
            }

            if (ModelState.IsValid)
            {
                var forum = new Forum
                {
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    ImageUrl = model.ForumImageUrl
                };

                await _forumService.Add(forum);

                return RedirectToAction("Index", "Forum");
            }
            return View(model);
        }
    }
}