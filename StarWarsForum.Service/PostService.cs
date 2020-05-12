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
                .Include(post => post.Topic)
                .First();

        public IEnumerable<Post> GetPostsByTopic(int topicId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateContent(int postId, string newContent)
        {
            _context.Posts.Where(post => post.Id == postId).First().Content = newContent;
            _context.Posts.Where(post => post.Id == postId).First().IsEdited = true;

            await _context.SaveChangesAsync();
        }
    }
}
