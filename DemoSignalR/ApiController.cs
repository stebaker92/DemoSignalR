using DemoSignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalR
{
    public class ApiController : Controller
    {
        private readonly IHubContext<ChatHub> connectionManager;
        private readonly string _jwtVerificationKey;

        public ApiController(IHubContext<ChatHub> connectionManager)
        {
            this.connectionManager = connectionManager;
            _jwtVerificationKey = Constants.JwtVerificationKey;
        }

        [HttpGet("status")]
        public async Task<ActionResult> ChangeStatus()
        {
            var userId = Constants.UserEmail;

            await connectionManager.Clients.User(userId).SendAsync("StatusUpdated", "Proceeding");

            return Ok("Status updated for " + userId);
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Steve"),
                new Claim(ClaimTypes.Email, Constants.UserEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtVerificationKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
