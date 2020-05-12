using StarWarsForum.Models.ForumViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.TopicViewModels
{
    public class HomeTopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ForumListingModel Forum { get; set;}
        public int PostsCount { get; set; }
        public string LastPostAuthorName { get; set; }
        public DateTime LastPostCreated { get; set; }


    }
}
