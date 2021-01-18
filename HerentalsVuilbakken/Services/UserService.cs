using HerentalsVuilbakken.Data;
using HerentalsVuilbakken.Helpers;
using HerentalsVuilbakken.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly VuilbakContext _vuilbakContext;

        public UserService(IOptions<AppSettings> appSettings, VuilbakContext vuilbakContext)
        {
            _appSettings = appSettings.Value;
            _vuilbakContext = vuilbakContext;
        }

        public User Authenticate(string username, string wachtwoord)
        {
            var user = _vuilbakContext.Users.Include(r => r.Role).SingleOrDefault(x => x.Username == username && x.Wachtwoord == wachtwoord);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Adres", user.Adres),
                    new Claim("Username", user.Username),
                    new Claim("RoleID", user.RoleID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Wachtwoord = null;

            return user;
        }
    }
}
