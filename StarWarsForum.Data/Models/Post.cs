using System;

namespace StarWarsForum.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
