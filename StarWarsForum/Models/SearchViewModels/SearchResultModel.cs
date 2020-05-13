using StarWarsForum.Models.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Models.SearchViewModels
{
    public class SearchResultModel
    {
        public IEnumerable<TopicListingModel> Topics { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}
