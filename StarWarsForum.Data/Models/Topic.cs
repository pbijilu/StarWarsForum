﻿using System.Collections.Generic;

namespace StarWarsForum.Data.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual Post OpeningPost { get; set; }

        public virtual IEnumerable<Post> Replies { get; set; }
    }
}