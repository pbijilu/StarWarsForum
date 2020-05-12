using System;
using System.Collections.Generic;
using System.Linq;
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
            var model = new PostModel
            {
                TopicId = id,
                TopicTitle = topic.Title,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostModel model)
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
        public async Task<IActionResult> EditPost(PostEditModel model)
        {
            await _postService.UpdateContent(model.Id, model.Content);

            return RedirectToAction("Index", "Topic", new { id = model.TopicId });
        }
    }
}