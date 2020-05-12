using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.PostViewModels
{
    public class PostEditModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string TopicTitle { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
    }
}
