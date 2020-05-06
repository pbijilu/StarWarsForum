using StarWarsForum.Models.ForumViewModels;
using System;

namespace StarWarsForum.Models.TopicsViewModel
{
    public class TopicListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TopicStarterName { get; set; }
        public int TopicStarterId { get; set; }
        public DateTime DatePosted { get; set; }

        public ForumListingModel Forum { get; set; }

        public int PostsCount { get; set; }

    }
}
