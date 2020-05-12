using StarWarsForum.Models.TopicViewModels;
using System.Collections.Generic;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<TopicListingModel> Topics { get; set; }
    }
}
