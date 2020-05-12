using StarWarsForum.Models.TopicViewModels;
using System.Collections.Generic;

namespace StarWarsForum.Models.HomeViewModels
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<HomeTopicModel> LatestReplies { get; set; }
    }
}
