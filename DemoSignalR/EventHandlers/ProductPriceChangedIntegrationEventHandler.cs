using DemoSignalR.Hubs;
using DemoSignalR.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DemoSignalR.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private IHubContext<ChatHub> _connectionManager;

        public ProductPriceChangedIntegrationEventHandler(ILogger<ProductPriceChangedIntegrationEventHandler> logger,
            IHubContext<ChatHub> connectionManager)
        {
            logger.LogInformation("EventHandler.Constructor");
            _connectionManager = connectionManager;
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            Console.WriteLine("Received Event");
            await _connectionManager.Clients.User(Constants.UserEmail).SendAsync("PriceUpdated", @event.ProductId, @event.NewPrice);
        }
    }
}
