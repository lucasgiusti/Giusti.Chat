app.controller('atendimentoController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService, socket) {
    UserService.verificaLogin();

    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };
    $scope.usuario = null;

    $scope.getUsuario = function () {
        $scope.usuario = UserService.getUser();

        $http.get(url + '/' + $scope.usuario, headerAuth).success(function (data) {
            $scope.usuario = data;
            
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };
});
