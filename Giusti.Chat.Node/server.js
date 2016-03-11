var io = require('socket.io').listen(3000);

var salas = [];

io.sockets.on('connection', function (socket) {
    console.log('new connection');

    socket.on('disconnect', function () {
        console.log('disconnected');
    });

    socket.on('iniciaAtendimentos', function (novasSalas) {
        
        for (var i = 0; i < novasSalas.length; i++) {
            salas.push(novasSalas[i]);
        };

        console.log('salas: ' + JSON.stringify(salas));
    });

    socket.on('excluiAtendimentos', function (novasSalas) {
        
        for (var i = 0; i < novasSalas.length; i++) {
            salas.splice(salas.indexOf(novasSalas[i]), 1);
        };
        
        console.log('salas: ' + salas.length);
    });
});