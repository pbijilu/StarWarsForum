using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Models.TopicViewModels;
using System.Collections.Generic;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumTopicModel
    {
        [TempData]
        public string TopicDeletedMessage { get; set; }
        public ForumListingModel Forum { get; set; }
        public IEnumerable<TopicListingModel> Topics { get; set; }
    }
}
