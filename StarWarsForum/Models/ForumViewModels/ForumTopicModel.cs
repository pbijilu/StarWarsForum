using StarWarsForum.Models.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<TopicListingModel> Topics { get; set; }
    }
}
