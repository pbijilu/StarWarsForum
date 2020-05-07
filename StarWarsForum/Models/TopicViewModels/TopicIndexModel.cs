using System.Collections.Generic;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.PostViewModels;

namespace StarWarsForum.Models.TopicViewModels
{
    public class TopicIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<PostModel> Posts { get; set; } 
        public ForumListingModel Forum { get; set; }
    }
}
