var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngCookies', 'toaster'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: 'app/templates/home.html', controller: 'homeController' });
        $routeProvider.when('/signin', { templateUrl: 'app/templates/signin.html', controller: 'signinController' });
        $routeProvider.when('/alterarsenha', { templateUrl: 'app/templates/empresa/empresa-alterarSenha.html', controller: 'alterarSenhaController' });
        $routeProvider.when('/esquecisenha', { templateUrl: 'app/templates/empresa/empresa-esqueciSenha.html', controller: 'esqueciSenhaController' });
        $routeProvider.when('/paginanaoencontrada', { templateUrl: 'app/templates/paginaNaoEncontrada.html', controller: 'paginaNaoEncontradaController' });
        $routeProvider.when('/area', { templateUrl: 'app/templates/area/areas.html', controller: 'areaController' });
        $routeProvider.when('/area/add', { templateUrl: 'app/templates/area/area-add.html', controller: 'areaController' });
        $routeProvider.when('/area/:id/edit', { templateUrl: 'app/templates/area/area-edit.html', controller: 'areaController' });
        $routeProvider.when('/area/:id', { templateUrl: 'app/templates/area/area-view.html', controller: 'areaController' });
        $routeProvider.otherwise({ redirectTo: '/paginanaoencontrada' });
        $locationProvider.html5Mode(true);
    });

app.run(function ($rootScope) {
    /*
        Receive emitted message and broadcast it.
        Event names must be distinct or browser will blow up!
    */
    $rootScope.$on('atualizaHeaderEmit', function (event, args) {
        $rootScope.$broadcast('atualizaHeaderBroadcast', args);
    });
});

app.factory('UserService', function ($http, $window, $cookies, $location, toasterAlert) {
    return {
        getUser: function () {
            var user = $cookies.get('user');
            if (user) {
                return JSON.parse(user);
            }
            else {
                return null;
            }
        },
        setUser: function (newUser) {
            if (newUser) {
                $cookies.put('user', JSON.stringify(newUser));
                $location.path('');
            }
            else {
                $cookies.put('user', null);
            }
        },
        verificaLogin: function () {
            var user = this.getUser();
            if (!user) {
               $location.path('signin');
            }
        }
    };
});