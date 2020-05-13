using System;

namespace StarWarsForum.Models.PostViewModels
{
    public class PostDeleteModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
    }
}
