using StarWarsForum.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsForum.Data
{
    public interface IForumService
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();

        Task Add(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        Task UpdateForumImageUrl(int forumId, string newImageUrl);

    }
}