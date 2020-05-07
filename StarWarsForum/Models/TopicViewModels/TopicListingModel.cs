using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using System;

namespace StarWarsForum.Models.TopicViewModels
{
    public class TopicListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TopicStarterName { get; set; }
        public string TopicStarterId { get; set; }
        public DateTime DateStarted { get; set; }
        public string LastPostAuthorName { get; set; }
        public string LastPostAuthorId { get; set; }
        public DateTime LastPostCreated { get; set; }

        //public ForumListingModel Forum { get; set; }

        public int PostsCount { get; set; }

    }
}
