using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.PostViewModels;

namespace StarWarsForum.Controllers
{
    public class PostController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(ITopicService topicService, IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _topicService = topicService;
            _postService = postService;
            _userManager = userManager;
        }
        
        public IActionResult Create(int id)
        {
            var topic = _topicService.GetById(id);

            var model = new PostCreateModel
            {
                TopicId = id,
                TopicTitle = topic.Title,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create (PostCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);
                var topic = _topicService.GetById(model.TopicId);

                var post = new Post
                {
                    Content = model.Content,
                    Created = DateTime.Now,
                    User = user,
                    Topic = topic
                };

                await _postService.Add(post);

                return RedirectToAction("Index", "Topic", new { id = model.TopicId });
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var post = _postService.GetById(id);

            var model = new PostEditModel
            {
                Content = post.Content,
                AuthorName = post.User.UserName,
                Created = post.Created,
                TopicTitle = post.Topic.Title,
                TopicId = post.Topic.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostEditModel model)
        {
            if (ModelState.IsValid)
            {
                await _postService.UpdateContent(model.Id, model.Content);

                return RedirectToAction("Index", "Topic", new { id = model.TopicId });
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var post = _postService.GetById(id);

            var model = new PostDeleteModel
            {
                TopicId = post.Topic.Id,
                AuthorName = post.User.UserName,
                Created = post.Created
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PostDeleteModel model)
        {
            await _postService.Delete(model.Id);
            TempData["PostDeletedMessage"] = "Post deleted!";
            TempData.Keep("PostDeletedMessage");
            return RedirectToAction("Index", "Topic", new { id = model.TopicId});
        }
    }
}