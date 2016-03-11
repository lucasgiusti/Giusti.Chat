app.controller('signinController', function ($scope, $http, toasterAlert, UserService, $location, $timeout) {

    var url = 'api/signin';

    $scope.heading = 'Login';

    //APIs
    $scope.postLogin = function () {
        
        $http.post(url, $scope.usuario).success(function (data) {
            UserService.setUser(data);
            
            $timeout(function () {
                $scope.$emit('atualizaHeaderEmit', data);
                $location.path('');
            }, 1000);

        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.cookieDestroy = function () {
        UserService.setUser(null);
        $scope.$emit('atualizaHeaderEmit', null);
    };
});