using Microsoft.EntityFrameworkCore;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsForum.Services
{
    public class ForumService : IForumService
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Forum forum)
        {
            _context.Add(forum);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            _context.Remove(GetById(forumId));

            await _context.SaveChangesAsync();
        }

        public IEnumerable<Forum> GetAll() =>
            _context.Forums;

        public Forum GetById(int id) =>
            _context.Forums.Where(forum => forum.Id == id)
                           .Include(forum => forum.Topics)
                               .ThenInclude(topic => topic.Posts)
                                   .ThenInclude(post => post.User)
                           .First();

        public async Task UpdateForumDescription(int forumId, string newDescription)
        {
            GetById(forumId).Description = newDescription;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumImageUrl(int forumId, string newImageUrl)
        {
            GetById(forumId).ImageUrl = newImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateForumTitle(int forumId, string newTitle)
        {
            GetById(forumId).Title = newTitle;

            await _context.SaveChangesAsync();
        }
    }
}
