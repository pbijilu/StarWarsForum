using Microsoft.EntityFrameworkCore;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Services
{
    public class TopicService : ITopicService
    {
        private readonly ApplicationDbContext _context;

        public TopicService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Topic topic)
        {
            _context.Add(topic);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int topicId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            throw new NotImplementedException();
        }

        public Topic GetById(int id) =>
            _context.Topics.Where(topic => topic.Id == id)
                    .Include(topic => topic.Forum)
                    .Include(topic => topic.Posts)
                        .ThenInclude(post => post.User)
                    .First();

        public IEnumerable<Topic> GetFilteredTopics(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetTopicsByForum(int id) =>
            _context.Forums
            .Where(forum => forum.Id == id).First()
            .Topics;

        public Task UpdateTitle(int topicId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
