using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using social_blog_API.DTOs.Posts;
using social_blog_API.Entities;

namespace social_blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialBlogDbContext _context;

        public PostsController(SocialBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var postLists = await _context.Posts.ToListAsync();
            var postDTO = postLists.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorId = p.AuthorId
            });
            return Ok(postDTO);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
        // GET: api/Posts/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<PostDTO>> GetPostByUserId(int userId)
        {
            var postsList = await _context.Posts.Where(p => p.AuthorId == userId).ToListAsync();
            if (postsList == null || postsList.Count == 0)
            {
                return NotFound();
            }
            var postsDTO = postsList.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorId = p.AuthorId
            });
            return Ok(postsDTO);
        }

        // GET: api/Posts/comment/5
        [HttpGet("comment/{commentId}")]
        public async Task<ActionResult<PostDTO>> GetPostByCommentId(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(comment.PostId);
            if (post == null )
            {
                return NotFound();
            }
            var postsDTO = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId
            };
            return postsDTO;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreatePostsDTO>> PostPost(CreatePostsDTO postDTO)
        {
            var newPost = new Post
            {
                Title = postDTO.Title,
                Content = postDTO.Content,
                AuthorId = postDTO.AuthorId
            };
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            var createdPost = new CreatePostsDTO
            {
                Id = newPost.Id,
                Title = newPost.Title,
                Content = newPost.Content,
                AuthorId = newPost.AuthorId
            };
            return CreatedAtAction("GetPost", new { id = newPost.Id }, createdPost);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
