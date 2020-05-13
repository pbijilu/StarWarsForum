using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.TopicViewModels;
using StarWarsForum.Lib;

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
            return View(model);
        }
    }
}