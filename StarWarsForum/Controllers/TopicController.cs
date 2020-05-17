using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
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

            if (topic != null)
            {
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
                        AuthorMemberSince = post.User.MemberSince,
                        Created = post.Created,
                        IsHead = post.IsHead,
                        IsEdited = post.IsEdited
                    }).OrderBy(post => post.Created),
                    ForumId = forum.Id,
                    ForumTitle = forum.Title
                };

                if (TempData["PostDeletedMessage"] != null)
                {
                    model.PostDeletedMessage = TempData["PostDeletedMessage"] as string;
                }

                return View(model);
            }

            return RedirectToAction("Topics", "Forum");
        }

        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);

            if (forum != null)
            {
                var model = new NewTopicModel
                {
                    ForumTitle = forum.Title,
                    ForumId = forum.Id,
                    TopicStarterName = User.Identity.Name
                };

                return View(model);
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewTopicModel model)
        {
            if (ModelState.IsValid)
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
                    Topic = topic,
                    IsHead = true
                };

                await _topicService.Add(topic);
                await _postService.Add(post);

                return RedirectToAction("Topics", "Forum", new { id = model.ForumId });
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var topic = _topicService.GetById(id);

            if (topic != null)
            {
                var head = topic.Posts.Where(post => post.IsHead == true).First();

                var model = new TopicEditModel
                {
                    ForumTitle = topic.Forum.Title,
                    TopicStarterName = head.User.UserName,
                    HeadId = head.Id,
                    Title = topic.Title,
                    Content = head.Content,
                };

                return View(model);
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicEditModel model)
        {
            if (ModelState.IsValid)
            {
                await _postService.UpdateContent(model.HeadId, model.Content);
                await _topicService.UpdateTitle(model.Id, model.Title);

                return RedirectToAction("Index", "Topic", new { id = model.Id });
            }
            return View(model);
        }

        public IActionResult Delete (int id)
        {
            var topic = _topicService.GetById(id);

            if (topic != null)
            {
                var head = topic.Posts.First(post => post.IsHead);

                var model = new TopicDeleteModel
                {
                    Id = topic.Id,
                    ForumId = topic.Forum.Id,
                    Created = head.Created,
                    TopicStarterName = head.User.UserName
                };

                return View(model);
            }

            return RedirectToAction("Index", "Forum");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TopicDeleteModel model)
        {
            await _postService.DeletePostsByTopic(model.Id);
            await _topicService.Delete(model.Id);

            TempData["TopicDeletedMessage"] = "Topic deleted!";
            TempData.Keep("TopicDeletedMessage");

            return RedirectToAction("Topics", "Forum", new { id = model.ForumId });
        }
    }
}