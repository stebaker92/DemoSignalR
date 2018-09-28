# SignalR with Auth demo

A demo SignalR application with secure Hubs using JWT authentication


## Usage

- Run the application and open your browser to: `http://localhost:60996/`
  - This will authenticate against the server with a dummy login
- Ping the following endpoint to simulate an application status being updated: `http://localhost:60996/status/`
  -  This will broadcast to a specific user (by email address) that their application has been updated