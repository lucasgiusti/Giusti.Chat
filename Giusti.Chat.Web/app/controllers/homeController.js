app.controller('homeController', function ($scope, $window, $http, $location, UserService, toasterAlert) {
    UserService.verificaLogin();

    $scope.heading = "Giusti.Chat";
    $scope.message = "Sistema template desenvolvido em ASP.Net WebAPI, Angularjs e Bootstrap";
    var url = 'api/usuario';
    var perfilAtendenteId = 3;

    $scope.verificaAtendente = function () {

        var headerAuth = { headers: { 'Authorization': 'Basic ' + user.token } };
        $http.get(url + '/GetForAtendimento', headerAuth).success(function (data) {
            
            if ($scope.usuarioAtendente(data)) {
                $location.path('atendimento');
            }

        }).error(function (jqxhr, textStatus) {
            if (textStatus) {
                $location.path('atendimento');
            }
        });
    };

    var user = UserService.getUser();
    if(user)
        $scope.verificaAtendente();

    $scope.usuarioAtendente = function (data)
    {
        var perfilAtendente = false;
        angular.forEach(data.perfis, function (perfil, key) {
            if (perfil.perfilId == perfilAtendenteId) {
                perfilAtendente = true;
            }
        });
        return perfilAtendente;
    }
});