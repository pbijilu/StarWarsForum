using Microsoft.EntityFrameworkCore;
using StarWarsForum.Data;
using StarWarsForum.Data.Models;
using System.Linq;
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

        public async Task Delete(int postId)
        {
            _context.Remove(GetById(postId));

            await _context.SaveChangesAsync();
        }

        public Post GetById(int id) =>
            _context.Posts.Where(post => post.Id == id)
                          .Include(post => post.User)
                          .Include(post => post.Topic)
                          .First();

        public async Task UpdateContent(int postId, string newContent)
        {
            _context.Posts.First(post => post.Id == postId).Content = newContent;
            _context.Posts.First(post => post.Id == postId).IsEdited = true;

            await _context.SaveChangesAsync();
        }
    }
}
