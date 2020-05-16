using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.TopicViewModels
{
    public class TopicDeleteModel
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public DateTime Created { get; set; }
        public string TopicStarterName { get; set; }
    }
}
