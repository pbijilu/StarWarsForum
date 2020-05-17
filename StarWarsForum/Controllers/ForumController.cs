using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Lib;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StarWarsForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;
        private readonly IPostService _postService;
        private readonly ITopicService _topicService;

        public ForumController(IForumService forumService, IConfiguration configuration, IUploadService uploadService, IPostService postService, ITopicService topicService)
        {
            _forumService = forumService;
            _configuration = configuration;
            _uploadService = uploadService;
            _postService = postService;
            _topicService = topicService;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => ModelBuilders.BuildForumListing(forum));

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            if (TempData["ForumDeletedMessage"] != null)
            {
                model.ForumDeletedMessage = TempData["ForumDeletedMessage"] as string;
            }

            return View(model);
        }

        public IActionResult Topics(int id)
        {
            var forum = _forumService.GetById(id);

            if (forum != null)
            {
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

            return RedirectToAction("Index", "Forum");
        }

        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index","Forum");

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

        public IActionResult Edit(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var forum = _forumService.GetById(id);

                if (forum != null)
                {
                    var model = new ForumCreateModel
                    {
                        Id = id,
                        Title = forum.Title,
                        Description = forum.Description,
                        ForumImageUrl = forum.ImageUrl
                    };

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ForumCreateModel model)
        {
            if (model.ImageUpload != null)
            {
                Uri uri = await Uploader.UploadImage("forum-images", model.ImageUpload, _configuration, _uploadService);
                model.ForumImageUrl = uri.AbsoluteUri;
            }

            if (ModelState.IsValid)
            {
                await _forumService.UpdateForumDescription(model.Id, model.Description);
                await _forumService.UpdateForumImageUrl(model.Id, model.ForumImageUrl);
                await _forumService.UpdateForumTitle(model.Id, model.Title);

                return RedirectToAction("Index", "Forum");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var forum = _forumService.GetById(id);

                if (forum != null)
                {
                    var model = new ForumDeleteModel
                    {
                        Id = forum.Id
                    };
    
                return View(model);
                }
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ForumDeleteModel model)
        {
            await _postService.DeletePostsByForum(model.Id);
            await _topicService.DeleteTopicsByForum(model.Id);
            await _forumService.Delete(model.Id);

            TempData["ForumDeletedMessage"] = "Forum deleted!";
            TempData.Keep("ForumDeletedMessage");

            return RedirectToAction("Index", "Forum");
        }
    }
}