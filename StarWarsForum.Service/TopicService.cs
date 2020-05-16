using Microsoft.EntityFrameworkCore;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Delete(int topicId)
        {
            _context.Remove(GetById(topicId));

            await _context.SaveChangesAsync();
        }

        public IEnumerable<Topic> GetAll() =>
            _context.Topics
                    .Include(topic => topic.Forum)
                    .Include(topic => topic.Posts)
                        .ThenInclude(post => post.User);

        public Topic GetById(int id) =>
            GetAll().First(topic => topic.Id == id);

        public IEnumerable<Topic> GetFilteredTopics(string searchQuery) =>
            GetAll().Where(topic => topic.Title.ToUpper().Contains(searchQuery.ToUpper()) 
                                || topic.Posts.Where(post => post.IsHead).First().Content.ToUpper().Contains(searchQuery.ToUpper()));

        public async Task UpdateTitle(int topicId, string newTitle)
        {
            GetById(topicId).Title = newTitle;

            await _context.SaveChangesAsync();
        }
    }
}
