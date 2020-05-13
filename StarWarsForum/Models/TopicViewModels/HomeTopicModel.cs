using System;

namespace StarWarsForum.Models.TopicViewModels
{
    public class HomeTopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ForumId { get; set; }
        public string ForumImageUrl { get; set; }
        public int PostsCount { get; set; }
        public string LastPostAuthorName { get; set; }
        public DateTime LastPostCreated { get; set; }


    }
}
