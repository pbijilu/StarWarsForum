using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using StarWarsForum.Lib;
using StarWarsForum.Models.SearchViewModels;
using StarWarsForum.Models.TopicViewModels;

namespace StarWarsForum.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITopicService _topicService;

        public SearchController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        public IActionResult Results(string searchQuery)
        {
            var topics = _topicService.GetFilteredTopics(searchQuery);
            var areNoResults = (!string.IsNullOrEmpty(searchQuery) && !topics.Any());

            var topicListings = topics.Select(topic => ModelBuilders.BuildTopicListing(topic));

            var model = new SearchResultModel
            {
                Topics = topicListings,
                SearchQuery = searchQuery,
                EmptySearchResults = areNoResults
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}