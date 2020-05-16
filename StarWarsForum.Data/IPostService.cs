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

        Task Add(Post post);
        Task Delete(int postId);
        Task DeletePostsinTopic(int topicId);
        Task UpdateContent(int postId, string newContent);
    }
}
