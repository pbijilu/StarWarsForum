using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Services
{
    public class TopicService : ITopicService
    {
        public Task Add(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int topicId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            throw new NotImplementedException();
        }

        public Topic GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetFilteredTopics(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTitle(int topicId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
