using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Data
{
    public interface ITopicService
    {
        Topic GetById(int id);
        IEnumerable<Topic> GetAll();
        IEnumerable<Topic> GetFilteredTopics(string searchQuery);
        IEnumerable<Topic> GetTopicsByForum(int forumId);

        Task Add(Topic topic);
        Task Delete(int topicId);
        Task UpdateTitle(int topicId, string newTitle);

    }
}
