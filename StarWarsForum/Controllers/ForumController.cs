using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Models.ForumViewModels;

namespace StarWarsForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly ITopicService _topicService;

        public ForumController(IForumService forumService, ITopicService topicService)
        {
            _forumService = forumService;
            _topicService = topicService;
        }
        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description
                });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };
            return View(model);
        }

        public IActionResult Topics(int id)
        {
            var forum = _forumService.GetById(id);
            var topics = _topicService.GetFilteredTopics(id);

            var topicListings = 
        }
    }
}