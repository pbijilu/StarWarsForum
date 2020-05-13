using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.TopicViewModels
{
    public class TopicEditModel
    {
        public int Id { get; set; }
        public string ForumTitle { get; set; }
        public string TopicStarterName { get; set; }
        public int HeadId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }


    }
}
