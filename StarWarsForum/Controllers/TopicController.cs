using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.PostViewModels;
using StarWarsForum.Models.TopicViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IForumService _forumService;
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TopicController(ITopicService topicService, IForumService forumService, IPostService postService, UserManager<ApplicationUser> userManager)
        {
            _topicService = topicService;
            _forumService = forumService;
            _postService = postService;
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            var topic = _topicService.GetById(id);
            var forum = topic.Forum;

            var model = new TopicIndexModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Posts = topic.Posts.Select(post => new PostModel
                {
                    Id = post.Id,
                    Content = post.Content,
                    AuthorName = post.User.UserName,
                    AuthorId = post.User.Id,
                    AuthorImageUrl = post.User.ProfileImageUrl,
                    Created = post.Created
                }).OrderBy(post => post.Created),
                Forum = new ForumListingModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                }
            };

            return View(model);
        }

        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);

            var model = new NewTopicModel
            {
                ForumTitle = forum.Title,
                ForumId = forum.Id,
                TopicStarterName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(NewTopicModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var topic = new Topic
            {
                Title = model.Title,
                Forum = _forumService.GetById(model.ForumId)
            };
            var post = new Post
            {
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Topic = topic
            };

            await _topicService.Add(topic);
            await _postService.Add(post);

            return RedirectToAction("Topics", "Forum", new { id = model.ForumId });
        }
    }
}