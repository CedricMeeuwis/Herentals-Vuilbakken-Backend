using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HerentalsVuilbakken.Data;
using HerentalsVuilbakken.Models;
using HerentalsVuilbakken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HerentalsVuilbakken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly VuilbakContext _context;
        public UserController(IUserService userService, VuilbakContext context)
        {
            _userService = userService;
            _context = context;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            //getuser
            User user = _context.Users.Where(x => x.Username == userParam.Username).FirstOrDefault();
            /* Fetch the stored value */
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            string savedPasswordHash = user.Wachtwoord;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(userParam.Wachtwoord, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return Unauthorized();

            var userAuthenticate = _userService.Authenticate(userParam.Username, savedPasswordHash);
            if (userAuthenticate == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(user.Wachtwoord, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            user.Wachtwoord = savedPasswordHash;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (AuthorizationCheck()) { return Unauthorized(); }

            if (user.Wachtwoord != "" && user.Wachtwoord != null)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(user.Wachtwoord, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                user.Wachtwoord = savedPasswordHash;

                _context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.Entry(user).Property(u => u.Wachtwoord).IsModified = false;
            }

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
            user.Wachtwoord = null;
            return user;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (AuthorizationCheck()) { return Unauthorized(); }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        private bool AuthorizationCheck()
        {
            //TO DO
            return false;
            bool isAdmin = bool.Parse(User.Claims.FirstOrDefault(c => c.Type == "Admin").Value);
            if (!isAdmin)
            {
                return true;
            }
            return false;
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
