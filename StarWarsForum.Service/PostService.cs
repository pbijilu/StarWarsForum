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
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int postId)
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id) =>
            _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .First();

        public IEnumerable<Post> GetPostsByTopic(int topicId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContent(int postId, string newContent)
        {
            throw new NotImplementedException();
        }
    }
}
