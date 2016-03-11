app.controller('atendimentoController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService, socket) {
    UserService.verificaLogin();

    var url = 'api/usuarioAtendimento';
    var urlUsuario = 'api/usuario';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };
    $scope.usuario = null;
    $scope.usuarioOld = null;
    $scope.salas = [];

    //APIs
    $scope.getUsuario = function () {

        $http.get(urlUsuario + '/GetForAtendimento', headerAuth).success(function (data) {
            $scope.usuario = data;

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
            }
            else {
                $scope.excluiSalas();
            }

            var user = UserService.getUser();
            user.atendendo = $scope.usuario.disponivel;
            UserService.setUser(user);
            $scope.$emit('atualizaHeaderEmit', user);

        }).error(function (jqxhr, textStatus) {
            angular.copy($scope.usuarioOld, $scope.usuario);
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.criaSalas = function () {
        $scope.salas = [];
        for (var i = 0; i < $scope.usuario.usuarioAtendimentos.length; i++) {
            var sala = { id: $scope.usuario.usuarioAtendimentos, usuarioId: $scope.usuario.id, chaveEmpresa: $scope.usuario.empresa.chave };
            $scope.salas.push(sala);
        }
        socket.emit('iniciaAtendimentos', $scope.salas);
    };

    $scope.excluiSalas = function () {
        socket.emit('excluiAtendimentos', $scope.salas);
        $scope.salas = [];
    };
});
