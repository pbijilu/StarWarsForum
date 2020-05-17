using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumCreateModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ForumImageUrl { get; set; }
        [Display(Name = "Image")]
        public IFormFile ImageUpload { get; set; }
    }
}
