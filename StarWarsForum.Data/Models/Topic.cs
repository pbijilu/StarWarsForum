using System.Collections.Generic;

namespace StarWarsForum.Data.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Forum Forum { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
