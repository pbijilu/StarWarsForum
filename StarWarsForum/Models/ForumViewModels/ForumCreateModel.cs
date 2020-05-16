using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumCreateModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ForumImageUrl { get; set; }
        [Required]
        [Display(Name = "Image")]
        public IFormFile ImageUpload { get; set; }
    }
}
