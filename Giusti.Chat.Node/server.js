var application_root = __dirname,
    express = require("express"),
    bodyParser = require('body-parser'),
    path = require("path"),
    app = express(),
    http = require('http').Server(app),
    io = require('socket.io')(http),
    port = process.env.PORT || 3000;

// Config
app.set('port', process.env.PORT || 3000);
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(express.static(path.join(__dirname, 'public')));
//************************************************************

io.on('connection', function () {
    console.log('connection');
    io.emit('msg', 'mensagem do nodejs');
});

// Launch server
http.listen(port, function () {
    console.log('Giusti.Chat.Node Application listening on *:3000');
});