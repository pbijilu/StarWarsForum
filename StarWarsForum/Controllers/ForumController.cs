using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.TopicViewModels;

namespace StarWarsForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => BuildForumListing(forum));

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

            var topicListings = topics.Select(topic => new TopicListingModel
            {
                Id = topic.Id,
                Title = topic.Title,
                TopicStarterName = topic.Posts.OrderBy(post => post.Created).First().User.UserName,
                TopicStarterId = topic.Posts.OrderBy(post => post.Created).First().User.Id,
                DateStarted = topic.Posts.OrderBy(post => post.Created).First().Created,
                PostsCount = topic.Posts.Count(),
                LastPostAuthorName = topic.Posts.OrderByDescending(post => post.Created).First().User.UserName,
                LastPostAuthorId = topic.Posts.OrderByDescending(post => post.Created).First().User.Id,
                LastPostCreated = topic.Posts.OrderByDescending(post => post.Created).First().Created
            }).OrderByDescending(topic => topic.LastPostCreated) ;

            var model = new ForumTopicModel
            {
                Forum = BuildForumListing(forum),
                Topics = topicListings
            };
            return View(model);
        }

        private ForumListingModel BuildForumListing(Forum forum) =>
            new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
    }
}