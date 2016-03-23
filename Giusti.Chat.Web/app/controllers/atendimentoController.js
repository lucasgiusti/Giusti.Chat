app.controller('atendimentoController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService, socket, guid) {
    UserService.verificaLogin();

    var mensagemNaoPodeFinalizar = JSON.stringify({ Success: false, Messages: [{ Message: 'É Necessário finalizar todos os atendimentos para tornar-se indisponível.' }] });
    var url = 'api/usuarioAtendimento';
    var urlUsuario = 'api/usuario';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };
    $scope.usuario = null;
    $scope.usuarioOld = null;
    $scope.salas = [];
    $scope.guidAtendente = guid;

    //APIs
    $scope.getUsuario = function () {

        $http.get(urlUsuario + '/GetForAtendimento', headerAuth).success(function (data) {
            $scope.usuario = data;
            $scope.usuarioId = data.id;

        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.iniciaAtendimentos = function (iniciar) {
        $scope.usuarioOld = angular.copy($scope.usuario);
        $scope.usuario.disponivel = iniciar;

        $http.put(url + '/' + $scope.usuario.id, JSON.stringify($scope.usuario), headerAuth).success(function (data) {
            $scope.usuario = data;

            if ($scope.usuario.disponivel) {
                $scope.criaSalas();

                var user = UserService.getUser();
                user.atendendo = $scope.usuario.disponivel;
                UserService.setUser(user);
                $scope.$emit('atualizaHeaderEmit', user);
            }
            else {
                var podeExcluir = true;
                for (var i = 0; i < $scope.salas.length; i++) {
                    console.log($scope.salas[i]);
                    if ($scope.salas[i].situacao == 1) {
                        podeExcluir = false;
                    }
                }

                if (podeExcluir) {
                    $scope.excluiSalas();

                    var user = UserService.getUser();
                    user.atendendo = $scope.usuario.disponivel;
                    UserService.setUser(user);
                    $scope.$emit('atualizaHeaderEmit', user);
                }
                else {
                    angular.copy($scope.usuarioOld, $scope.usuario);
                    toasterAlert.showAlert(mensagemNaoPodeFinalizar);
                }
            }
        }).error(function (jqxhr, textStatus) {
            angular.copy($scope.usuarioOld, $scope.usuario);
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.criaSalas = function () {
        $scope.salas = [];
        for (var i = 0; i < $scope.usuario.usuarioAtendimentos.length; i++) {
            var sala = { id: $scope.usuario.usuarioAtendimentos[i].id, guidAtendente: $scope.guidAtendente, chaveEmpresa: $scope.usuario.empresa.chave, situacao: 0, guidCliente: null, mensagens: [] };
            $scope.salas.push(sala);
        }
        socket.emit('iniciaAtendimentos', $scope.salas);
    };

    $scope.excluiSalas = function () {
        socket.emit('excluiAtendimentos', $scope.salas);
        $scope.salas = [];
    };

    $scope.novaMensagem = function (guidCliente) {
        socket.emit('novaMensagem', { 'guidCliente': guidCliente, enviadaPor: 'ATENDENTE', texto: 'nova mensagem do atendente.' });
    };

    $scope.finalizaAtendimento = function (guidCliente) {
        socket.emit('finalizaAtendimento', guidCliente);
    };

    socket.on('atualizaSalaAtendente-' + $scope.guidAtendente, function (sala) {
        for (var i = 0; i < $scope.salas.length; i++) {
            if ($scope.salas[i].id == sala.id) {
                $scope.salas[i] = sala;
            }
        }
    });
});
