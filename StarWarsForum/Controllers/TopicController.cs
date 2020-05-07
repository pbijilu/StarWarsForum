using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.PostViewModels;
using StarWarsForum.Models.TopicViewModels;
using System.Linq;

namespace StarWarsForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
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
    }
}