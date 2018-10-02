# SignalR with Auth demo

A demo SignalR application with secure Hubs using JWT authentication & using rabbitMQ

## Running through Docker

- `docker-compose.exe up --build`

## Usage

- Run RabbitMQ locally (or point to an existing instance & update `appsettings.json`)
  - `docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management`
- Run the application and open your browser to: `http://localhost:60996/`
- Click login to generate a valid authentication token
- Ping the following endpoint to publish a PriceUpdatedEvent to the Exchange: `http://localhost:60996/publish/`
  - The Event will then be picked up & processed by the event handler (defined in the Api Startup.cs)
  - This will broadcast an event to the specified user (by email address) that a products price has been updated

## Notes

RabbitMQ boilerplate taken from [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Basket/Basket.API/Startup.cs)
