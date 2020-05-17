using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StarWarsForum.Models.ForumViewModels
{
    public class ForumIndexModel
    {
        [TempData]
        public string ForumDeletedMessage { get; set; }
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
