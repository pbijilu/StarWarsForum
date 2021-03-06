﻿using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.TopicViewModels
{
    public class NewTopicModel
    {
        public string ForumTitle { get; set; }
        public int ForumId { get; set; }
        public string TopicStarterName { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
