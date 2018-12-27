using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    class Comment
    {
        [Key]
        public Guid commentId { get; set; }

        public string body { get; set; }

        public Guid postId { get; set; }
        public Post Post { get; set; }

        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public List<CommentLike> commentLikes { get; set; }

        public bool isActive { get; set; }

        public DateTime created { get; set; }
    }

    class CommentLike
    {
        [Key]
        public Guid likeId { get; set; }

        public Guid commentId { get; set; }
        public Comment Comment { get; set; }

        public long userId { get; set; }
        public UserChunk UserChunk { get; set; }

        public bool isActive { get; set; }

        public DateTime created { get; set; }
    }
}
