using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Post
    {
        [Key]
        public Guid postId { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public List<string> images { get; set; }

        public List<string> videos { get; set; }

        public List<PostLike> postLikes { get; set; }
        
        public bool isActive { get; set; }

        public DateTime created { get; set; }
    }

    public class PostLike
    {
        [Key]
        public Guid likeId { get; set; }

        public Guid postId { get; set; }
        public Post Post { get; set; }

        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public bool isActive { get; set; }

        public DateTime created { get; set; }
    }
}
