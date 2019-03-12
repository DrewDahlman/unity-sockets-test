var server = require('http').Server();
var io = require('socket.io')(server);
server.listen(3000);


// Base room
let _room = {
	name: "lobby",
	players: []
};

// Connection
io.on('connection', function (socket) {

	// User join
	socket.on("join", () => {
	  io.emit("welcome", {room: _room});
	});

	// Register user
	socket.on("register", (data) => {
		_room.players.push(data);
		socket.user = data.name;
	  io.emit("welcome", {room: _room});
	});

	// Update from user
  socket.on('update', function (data) {
    let _player = _room.players.find( (p) => {
    	return p.name == data.name;
    });
    if( _player ){
	    _player.position = data.position;
	    _player.rotation = data.rotation;
	  }
  });

  // Remove user
  socket.on('disconnect', function () {
    _room.players = _room.players.filter(function(p) { return p.name != socket.user; });
	  io.emit('user left', {user: {name: socket.user}});
	});
});

// Update
setInterval( () => {
	io.emit("room update", {data: _room});
},1);
