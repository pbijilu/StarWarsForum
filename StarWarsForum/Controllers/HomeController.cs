using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.HomeViewModels;
using StarWarsForum.Models.TopicViewModels;

namespace StarWarsForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITopicService _topicService;

        public HomeController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public IActionResult Index()
        {
            var topics = _topicService.GetAll();
            var model = new HomeIndexModel
            {
                LatestReplies = topics.Select(topic => new TopicListingModel
                {
                    Id = topic.Id,
                    Title = topic.Title,
                    TopicStarterName = topic.Posts.OrderBy(post => post.Created).First().User.UserName,
                    TopicStarterId = topic.Posts.OrderBy(post => post.Created).First().User.Id,
                    DateStarted = topic.Posts.OrderBy(post => post.Created).First().Created,
                    PostsCount = topic.Posts.Count(),
                    LastPostAuthorName = topic.Posts.OrderByDescending(post => post.Created).First().User.UserName,
                    LastPostAuthorId = topic.Posts.OrderByDescending(post => post.Created).First().User.Id,
                    LastPostCreated = topic.Posts.OrderByDescending(post => post.Created).First().Created,
                    Forum = new ForumListingModel
                    {
                        Id = topic.Forum.Id,
                        Title = topic.Forum.Title,
                        Description = topic.Forum.Description,
                        ImageUrl = topic.Forum.ImageUrl
                    }
                }).OrderByDescending(topic => topic.LastPostCreated).Take(10)
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
