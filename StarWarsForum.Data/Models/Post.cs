using System;

namespace StarWarsForum.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool IsHead { get; set; } = false;
        public bool IsEdited { get; set; } = false;

        public Topic Topic { get; set; }
        public ApplicationUser User { get; set; }
    }
}
