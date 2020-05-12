using StarWarsForum.Models.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.HomeViewModels
{
    public class HomeIndexModel
    {
        public string SearchQuery { get; set; }
        public IEnumerable<TopicListingModel> LatestReplies { get; set; }
    }
}
