using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using social_blog_API.DTOs.Comments;
using social_blog_API.Entities;

namespace social_blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly SocialBlogDbContext _context;

        public CommentsController(SocialBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var commentList = await _context.Comments.ToListAsync();
            var commentDTOs = commentList.Select(e => new CommentDTO
            {
                Id = e.Id,
                Content = e.Content,
                Likes = e.Likes,
                UserId = e.UserId,
                PostId = e.PostId,
            }).ToList();
            return Ok(commentDTOs);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            //var commentDTO
            return comment;
        }

        //TODO: ADD A GET BY POST ID
        // GET: api/Comments/post/0
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<Comment>> GetCommentByPost(int postId)
        {
            var commentList = await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
            var commentsDTO = commentList.Select(x => new CommentDTO
            {
                Id = x.Id,
                Content = x.Content,
                Likes = x.Likes,
                UserId = x.UserId,
                PostId = x.PostId
            });
            return Ok(commentsDTO);
        }
        // GET: api/Comments/user/userId
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Comment>> GetCommentsByUser(int userId)
        {
            var commentList = await _context.Comments.Where(x => x.UserId == userId).ToListAsync();
            var commentsDTO = commentList.Select(x => new CommentDTO
            {
                Id = x.Id,
                Content = x.Content,
                Likes = x.Likes,
                UserId = x.UserId,
                PostId = x.PostId
            });
            return Ok(commentsDTO);
        }




        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateCommentsDTO>> PostComment(CreateCommentsDTO comment)
        {
            var newComment = new Comment
            {
                Content = comment.Content.Trim(),
                PostId = comment.PostId,
                UserId = comment.UserId
            };
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();
            var createComment = new CreateCommentsDTO
            {
                Id = newComment.Id,
                Content = newComment.Content,
                PostId = newComment.PostId,
                UserId = newComment.UserId
            };

            return CreatedAtAction("GetComment", new { id = newComment.Id }, createComment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
