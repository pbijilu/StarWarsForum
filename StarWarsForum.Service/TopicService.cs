﻿using Microsoft.EntityFrameworkCore;
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
            _context.Topics.Where(topic => topic.Id == id)
                    .Include(topic => topic.Forum)
                    .Include(topic => topic.Posts)
                        .ThenInclude(post => post.User)
                    .First();

        public IEnumerable<Topic> GetFilteredTopics(string searchQuery) =>
            _context.Topics.Where(topic => topic.Title.Contains(searchQuery));

        public async Task UpdateTitle(int topicId, string newTitle)
        {
            _context.Topics.Where(topic => topic.Id == topicId).First().Title = newTitle;

            await _context.SaveChangesAsync();
        }
    }
}
