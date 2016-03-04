app.controller('esqueciSenhaController', function ($scope, $http, toasterAlert, UserService, $location) {

    var mensagemSalvo = 'Nova senha gerada com sucesso e enviada para [EMAIL]';
    var url = 'api/esquecisenha';

    $scope.heading = 'Esqueci Minha Senha';
    $scope.empresa = {};

    //APIs
    $scope.postGeraNovaSenha = function () {

        $http.post(url, $scope.empresa).success(function (data) {
            toasterAlert.showAlert(JSON.stringify({ Success: "info", Messages: [{ Message: mensagemSalvo.replace('[EMAIL]', $scope.empresa.emailUsuarioAdm) }] }));
            $scope.empresa = {};
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.cookieDestroy = function () {
        UserService.setUser(null);
        $scope.$emit('atualizaHeaderEmit', null);
    };
});