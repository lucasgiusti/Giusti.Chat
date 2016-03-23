app.controller('atendimentoSalaController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService, socket, guid) {

    $scope.sala = null;
    $scope.guidCliente = guid;
    
    $scope.iniciaAtendimento = function (iniciar) {

            if (iniciar) {
                $scope.solicitaAtendimento();
            }
            else {
                $scope.finalizaAtendimento();
            }
    };

    $scope.solicitaAtendimento = function () {
        socket.emit('solicitaAtendimento', $scope.guidCliente);
    };

    $scope.finalizaAtendimento = function () {
        socket.emit('finalizaAtendimento', $scope.guidCliente);
    };

    $scope.novaMensagem = function () {
        socket.emit('novaMensagem', { guidCliente: $scope.guidCliente, enviadaPor: 'CLIENTE', texto: 'nova mensagem do cliente.' });
    };

    socket.on('atualizaSalaCliente-' + $scope.guidCliente, function (sala) {
        $scope.sala = sala;
    });
});
