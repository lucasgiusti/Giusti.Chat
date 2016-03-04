app.controller('empresaController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService) {
    UserService.verificaLogin();

    var mensagemExcluir = 'Deseja realmente excluir a empresa [NOMEEMPRESA] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Empresa salva com sucesso' }] });
    var url = 'api/empresa';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };

    $scope.heading = 'Empresas';
    $scope.empresas = [];
    $scope.empresa = null;

    //APIs
    $scope.getEmpresas = function () {

        $http.get(url, headerAuth).success(function (data) {
            $scope.empresas = data;
            $scope.total = $scope.empresas.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    $scope.getEmpresa = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id, headerAuth).success(function (data) {
            $scope.empresa = data;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.postEmpresa = function () {

        $http.post(url, JSON.stringify($scope.empresa), headerAuth).success(function (id) {
            $scope.id = id;
            $scope.getEmpresa();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.putEmpresa = function () {

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.empresa), headerAuth).success(function (data) {
            $scope.empresa = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.deleteEmpresa = function () {

        $http.delete(url + '/' + $scope.empresa.id, headerAuth).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.empresas.splice($scope.empresas.indexOf($scope.empresa), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    //Utils
    $scope.addEmpresa = function () {
        $scope.empresa = { ativo: 1 };
    };

    $scope.openModalDelete = function (empresa) {
        $scope.empresa = empresa;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEEMPRESA]', $scope.empresa.nome) };

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
            $scope.deleteEmpresa();
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
