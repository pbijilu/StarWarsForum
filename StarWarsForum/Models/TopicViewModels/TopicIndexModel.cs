using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.PostViewModels;

namespace StarWarsForum.Models.TopicViewModels
{
    public class TopicIndexModel
    {
        [TempData]
        public string PostDeletedMessage { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<PostModel> Posts { get; set; } 
        public int ForumId { get; set; }
        public string ForumTitle { get; set; }
    }
}
