using System;
using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.PostViewModels
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime AuthorMemberSince { get; set; }
        public DateTime Created { get; set; }
        public int TopicId { get; set; }
        public string TopicTitle { get; set;}
        public bool IsHead { get; set; }
        public bool IsEdited { get; set; }

    }
}
