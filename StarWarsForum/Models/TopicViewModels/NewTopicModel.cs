using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.TopicViewModels
{
    public class NewTopicModel
    {
        public string ForumTitle { get; set; }
        public int ForumId { get; set; }
        public string TopicStarterName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
