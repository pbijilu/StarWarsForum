﻿using System;
using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.PostViewModels
{
    public class PostEditModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string TopicTitle { get; set; }
        [Required]
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
    }
}
