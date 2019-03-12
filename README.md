# unity-sockets-test
Quick demo of sockets and unity. This is a quick prototype to test creating a custom socket server and connecting to it from unity.

## Requirements
- Unity Version: 2018.2.18f
- Docker ( if you want to run local server )

## Running Unity
- Open the project in Unity
- Open the `SampleScene`
- The default server is `138.197.162.2` this is always running
- If running the socket server locally
	- Adjust the `SocketIO` game object `IP` field to `localhost:3000`

## Running sockets
- cd into `server`
- run `docker-compose up`
- Will start a socket server at `localhost:3000`
- You can also run Ngrok if you'd like to share your local server

