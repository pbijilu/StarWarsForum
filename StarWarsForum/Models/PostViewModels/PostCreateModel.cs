using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.PostViewModels
{
    public class PostCreateModel
    {
        public int TopicId { get; set; }
        public string TopicTitle { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
