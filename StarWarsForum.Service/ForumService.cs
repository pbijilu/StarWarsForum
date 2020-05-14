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
        public Task Create(Forum forum)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Forum> GetAll() =>
            _context.Forums;

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new System.NotImplementedException();
        }

        public Forum GetById(int id) =>
            _context.Forums.Where(forum => forum.Id == id)
                           .Include(forum => forum.Topics)
                               .ThenInclude(topic => topic.Posts)
                                   .ThenInclude(post => post.User)
                           .First();

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateForumImageUrl(int forumId, string newImageUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new System.NotImplementedException();
        }
    }
}
