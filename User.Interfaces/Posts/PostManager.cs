using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace User.Posts
{
    public class PostManager
    {
        public async Task<Post> createPost(Post post)
        {
            // Create a new post
            Post newPost = new Post
            {
                body = post.body,
                created = DateTime.Now,
                images = post.images,
                isActive = true,
                postId = new Guid(),
                postLikes = new List<PostLike>(),
                title = post.title,
                userId = post.userId,
                videos = post.videos
            };

            return await Task.FromResult(newPost);
        }
    }
}
