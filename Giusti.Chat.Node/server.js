var io = require('socket.io').listen(3000);

var salas = [];

io.sockets.on('connection', function (socket) {
    
    socket.on('disconnect', function () {
        
    });
    
    socket.on('iniciaAtendimentos', function (novasSalas) {
        
        for (var i = 0; i < novasSalas.length; i++) {
            salas.push(novasSalas[i]);
        };
    });
    
    socket.on('excluiAtendimentos', function (novasSalas) {
        
        for (var i = 0; i < novasSalas.length; i++) {
            salas.splice(salas.indexOf(novasSalas[i]), 1);
        };
    });
    
    socket.on('solicitaAtendimento', function (solicitacao) {
        
        var salasDisponiveis = [];
        for (var i = 0; i < salas.length; i++) {
            if (salas[i].chaveEmpresa == solicitacao.chaveEmpresa && salas[i].situacao == 0)
                salasDisponiveis.push(salas[i]);
        }
        if (salasDisponiveis.length > 0) {
            var salaDisponivel = salasDisponiveis[Math.floor(Math.random() * salasDisponiveis.length)];
            
            salaDisponivel.situacao = 1;
            salaDisponivel.guidCliente = solicitacao.guidCliente;
            salas[salas.indexOf(salaDisponivel)] = salaDisponivel;
            io.sockets.emit('atualizaSalaAtendente-' + salaDisponivel.guidAtendente, sala = salaDisponivel);
            socket.emit('atualizaSalaCliente-' + solicitacao.guidCliente, sala = salaDisponivel);
        }
        else {
            socket.emit('atualizaSalaCliente-' + solicitacao.guidCliente, sala = null);
        }
    });
    
    socket.on('finalizaAtendimento', function (guidCliente) {
        
        var salaCliente = null;
        for (var i = 0; i < salas.length; i++) {
            if (salas[i].guidCliente == guidCliente)
                salaCliente = salas[i];
        }
        if (salaCliente != null) {
            salaCliente.situacao = 0;
            salaCliente.guidCliente = null;
            salas[salas.indexOf(salaCliente)] = salaCliente;
            io.sockets.emit('atualizaSalaAtendente-' + salaCliente.guidAtendente, sala = salaCliente);
        }
        io.sockets.emit('atualizaSalaCliente-' + guidCliente, sala = null);
    });

    socket.on('novaMensagem', function (mensagem) {
        
        var salaCliente = null;
        for (var i = 0; i < salas.length; i++) {
            if (salas[i].guidCliente == mensagem.guidCliente)
                salaCliente = salas[i];
        }
        if (salaCliente != null) {
            salaCliente.mensagens.push({ texto: mensagem.texto });
            salas[salas.indexOf(salaCliente)] = salaCliente;
            
            io.sockets.emit('atualizaSalaAtendente-' + salaCliente.guidAtendente, sala = salaCliente);
            io.sockets.emit('atualizaSalaCliente-' + mensagem.guidCliente, sala = salaCliente);
        }
    });
});