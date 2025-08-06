using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using social_blog_API.DTOs.Comments;
using social_blog_API.DTOs.Users;
using social_blog_API.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace social_blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SocialBlogDbContext _context;

        public UsersController(SocialBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userDTO = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                Description = u.Description,

            });
            return Ok(userDTO);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Description = user.Description,

            };


            return userDTO;
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<Comment>> GetCommentByPost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(post.AuthorId);
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Description = user.Description
            };
            return Ok(userDTO);
        }
        
        //I lied in commit. Doesnt work properly
        //TODO: Work out verification
        [HttpPost("verify/{username}")]
        public async Task<ActionResult<UserVerificationDTO>> VerifyUserLogin(string username, string password)
        {
            //throw new NotImplementedException();
            bool doesUserExist = UserExists(username);
            if (doesUserExist == false)
            {
                return NotFound();
            }
            var existingUser = await _context.Users.SingleAsync(x => x.Username == username);
            
            
            //AS OF RIGHT NOW DOESNT DO MUCH
            var userVerify = new UserVerificationDTO
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                Password = existingUser.Password,
                
            };
            userVerify.IsVerified = VerifyPassword(password, existingUser.Password);
            return userVerify.IsVerified ? Ok(userVerify) : Unauthorized();     
        }
        

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(user.Password);
            byte[] tmpHash = MD5.HashData(tmpSource);
            user.Password = tmpHash.ToString();


            var edittedUser = await _context.Users.FindAsync(user.Id);
            edittedUser.Username = user.Username;
            edittedUser.Password = user.Password;
            edittedUser.Description = user.Description;

            _context.Entry(edittedUser).State = EntityState.Modified;
            //Oh my fuicking god I lovce my new keyboard so muc
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //READ LATER
        [HttpPost]
        public async Task<ActionResult<CreateUsersDTO>> PostUser(CreateUsersDTO user)
        {


            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(user.Password);
            byte[] tmpHash = MD5.HashData(tmpSource);
            user.Password = tmpHash.ToString();
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Description = user.Description,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();


            var createUser = new CreateUsersDTO()
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Password = newUser.Password,
                Description = newUser.Description,
            };

            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool UserExists(string username)
        {

            return _context.Users.Any(e => e.Username == username);
        }
        private bool VerifyPassword(string password, string attemptedPassword)
        {
            byte[] userPassword = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] attemptedPasswordToBytes = ASCIIEncoding.ASCII.GetBytes(attemptedPassword);
            bool isEqualValues = true;
            if (userPassword.Length == attemptedPasswordToBytes.Length)
            {
                for (int i = 0; i < userPassword.Length; i++)
                {
                    if (userPassword[i] == attemptedPasswordToBytes[i])
                    {
                        continue;
                    }
                    else
                    {
                        isEqualValues = false;
                        return isEqualValues;
                    }
                }
            }
            return isEqualValues;
        }
    }
}

