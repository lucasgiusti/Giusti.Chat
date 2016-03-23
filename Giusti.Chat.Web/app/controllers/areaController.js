app.controller('areaController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService) {
    UserService.verificaLogin();

    var mensagemExcluir = 'Deseja realmente excluir a area [NOMEAREA] ?';
    var mensagemSalvo = JSON.stringify({ Success: true, Messages: [{ Message: 'Área salva com sucesso' }] });
    var url = 'api/area';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };

    $scope.heading = 'Áreas';
    $scope.areas = [];
    $scope.area = null;

    //APIs
    $scope.getAreas = function () {

        $http.get(url, headerAuth).success(function (data) {
            $scope.areas = data;
            $scope.total = $scope.areas.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    $scope.getArea = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id, headerAuth).success(function (data) {
            $scope.area = data;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.postArea = function () {

        $http.post(url, JSON.stringify($scope.area), headerAuth).success(function (id) {
            $scope.id = id;
            $scope.getArea();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.putArea = function () {

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.area), headerAuth).success(function (data) {
            $scope.area = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.deleteArea = function () {

        $http.delete(url + '/' + $scope.area.id, headerAuth).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.areas.splice($scope.areas.indexOf($scope.area), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    //Utils
    $scope.addArea = function () {
        $scope.area = { ativo: 1 };
    };

    $scope.openModalDelete = function (area) {
        $scope.area = area;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEAREA]', $scope.area.nome) };

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
            $scope.deleteArea();
        });
    };

    //PAGINATION
    $scope.total = 0;
    $scope.currentPage = 1;
    $scope.itemPerPage = 5;
    $scope.start = 0;
    $scope.maxSize = 5;
    $scope.pageChanged = function () {
        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
    };
});
