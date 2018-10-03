# SignalR with Auth demo

A demo SignalR application with secure Hubs using JWT authentication & using rabbitMQ

RabbitMQ Exchanges and Queues will be automatically generated

## Usage (Running with Docker)

- Run `docker-compose.exe up --build` to run both API's & rabbitMQ locally
- Run RabbitMQ locally (or point to an existing instance & update `appsettings.json`)
  - `docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management`
- Run the application and open your browser to: `http://localhost:9092/`
- Click login to generate a valid authentication token
- Ping the following endpoint to publish a PriceUpdatedEvent to the RabbitMQ Exchange: `http://localhost:9091/price/update`
  - The Event will then be picked up & processed by the event handler (defined in the CustomersArea Startup.cs)
  - This will broadcast an event to the specified customer (by email address) that a displays 'price has been updated'

## Notes

RabbitMQ boilerplate taken from [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Basket/Basket.API/Startup.cs)
