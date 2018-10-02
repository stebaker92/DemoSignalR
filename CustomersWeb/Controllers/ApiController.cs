using CustomersWeb.Hubs;
using CustomersWeb.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomersWeb.Controllers
{
    public class ApiController : Controller
    {
        private readonly IHubContext<ChatHub> _connectionManager;
        private readonly IEventBus _eventBus;

        private readonly IConfiguration _configuration;

        public ApiController(IHubContext<ChatHub> connectionManager, IEventBus eventBus, IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionManager = connectionManager;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Mock route to simulate a status being published from another application
        /// </summary>
        /// <returns></returns>
        [HttpGet("publish")]
        public ActionResult PublishPriceChanged()
        {
            _eventBus.Publish(new ProductPriceChangedIntegrationEvent(123, 9.99m, 14.99m));

            return Ok("Event published to RabbitMQ");
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Steve"),
                new Claim(ClaimTypes.Email, Constants.UserEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSigningKey"]));

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
