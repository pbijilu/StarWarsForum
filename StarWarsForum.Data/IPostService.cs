using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Data
{
    public interface IPostService
    {
        Post GetById(int id);
        IEnumerable<Post> GetPostsByTopic(int topicId);

        Task Add(Post post);
        Task Delete(int postId);
        Task UpdateContent(int postId, string newContent);
    }
}
