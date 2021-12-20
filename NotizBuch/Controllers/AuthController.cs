using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotizBuch.Models;
using NotizBuch.SQL.Connections;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotizBuch.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Prüft die Nutzerdaten und erstellt anschließend einen JWT Token und gibt diesen zurück
        [HttpPost]
        public string Auth([FromBody] UserCredentials user)
        {

            if (UserConnection.CheckAuth(user))
            {

                var claims = new[]
                {
                    new Claim("username", user.Username)
                };

                var secretBytes = Encoding.UTF8.GetBytes(AuthConstants.Secret);

                var key = new SymmetricSecurityKey(secretBytes);
                var algorithm = SecurityAlgorithms.HmacSha256;

                var signingCredentials = new SigningCredentials(key, algorithm);

                var token = new JwtSecurityToken(
                    AuthConstants.Issuer,
                    AuthConstants.Audiance,
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenJson;
            }
            else return "";
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] UserCredentials credentials)
        {
            UserConnection.Insert(credentials);
            return Ok();
        }
    }
}
