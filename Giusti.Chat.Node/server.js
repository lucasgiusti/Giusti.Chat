var io = require('socket.io').listen(3000);

io.sockets.on('connection', function (socket) {
    console.log('new connection');

    socket.on('disconnect', function () {
        console.log('disconnected');
    });
});