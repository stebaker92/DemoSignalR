using PricePublisher.IntegrationEvents.Events;
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

namespace PricePublisher.Controllers
{
    public class ApiController : Controller
    {
        private readonly IEventBus _eventBus;

        public ApiController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok($"Price API. {Environment.NewLine}Use the price/update route to send a PriceUpdatedEvent");
        }

        [HttpGet("price/update")]
        public ActionResult PublishPriceChanged()
        {
            _eventBus.Publish(new ProductPriceChangedIntegrationEvent(123, 9.99m, 14.99m));

            return Ok("Event published to RabbitMQ");
        }
    }
}
