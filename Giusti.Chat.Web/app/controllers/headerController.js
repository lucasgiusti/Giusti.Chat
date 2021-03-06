app.controller('headerController', function ($scope, $http, toasterAlert, $uibModal, UserService, $location, socket) {

    $scope.usuario = null;
    $scope.navbarCollapsed = true;

    $scope.$on('atualizaHeaderBroadcast', function (event, args) {
        $scope.atualizaHeader(args);
    });
    
    $scope.atualizaHeader = function (args) {
        if (args) {
            $scope.usuario = args;
        }
        else {
            $scope.usuario = UserService.getUser();
        }
    };

    $scope.openModalSair = function () {
        $scope.dadosModalConfirm = { 'titulo': 'Sair', 'mensagem': 'Deseja realmente sair ?' };

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'app/templates/modalConfirm.html',
            controller: 'modalConfirmInstanceController',
            resolve: {
                dadosModalConfirm: function () {
                    return $scope.dadosModalConfirm;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.cookieDestroy();            
        });
    };

    $scope.cookieDestroy = function () {
        UserService.setUser(null);
        $scope.$emit('atualizaHeaderEmit', null);
        $location.path('signin');
    };

    $scope.gotoHome = function () {
        $location.path('');
    }
});