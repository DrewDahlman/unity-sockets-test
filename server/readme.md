# Socket Server
A simple socket server for a basic multiplayer unity game.

## Running
- have Docker installed
- run `docker-compose up`

## Under the hood
When the server is started it creates a socket listener on port `3000` it also creates an in memory object that represents the "room" which is where players are added as they join and removed as they quit the game.