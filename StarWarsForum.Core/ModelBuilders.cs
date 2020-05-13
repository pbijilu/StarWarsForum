using StarWarsForum.Data.Models;
using StarWarsForum.Models.ForumViewModels;
using StarWarsForum.Models.TopicViewModels;
using System.Linq;

namespace StarWarsForum.Lib
{
    public class ModelBuilders
    {
        public static TopicListingModel BuildTopicListing(Topic topic)
        {
            var head = topic.Posts.OrderBy(post => post.Created).First();
            var last = topic.Posts.OrderByDescending(post => post.Created).First();

            var model = new TopicListingModel
            {
                Id = topic.Id,
                Title = topic.Title,
                TopicStarterName = head.User.UserName,
                TopicStarterId = head.User.Id,
                DateStarted = head.Created,
                PostsCount = topic.Posts.Count(),
                LastPostAuthorName = last.User.UserName,
                LastPostAuthorId = last.User.Id,
                LastPostCreated = last.Created
            };

            return model;
        }

        public static ForumListingModel BuildForumListing(Forum forum) =>
            new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
    }
}
