var app = angular.module('app', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngCookies', 'toaster', 'socket-io'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: 'app/templates/home.html', controller: 'homeController' });
        $routeProvider.when('/signin', { templateUrl: 'app/templates/signin.html', controller: 'signinController' });
        $routeProvider.when('/perfil', { templateUrl: 'app/templates/perfil/perfis.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/add', { templateUrl: 'app/templates/perfil/perfil-add.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/:id/edit', { templateUrl: 'app/templates/perfil/perfil-edit.html', controller: 'perfilController' });
        $routeProvider.when('/perfil/:id', { templateUrl: 'app/templates/perfil/perfil-view.html', controller: 'perfilController' });
        $routeProvider.when('/usuario', { templateUrl: 'app/templates/usuario/usuarios.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/add', { templateUrl: 'app/templates/usuario/usuario-add.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/:id/edit', { templateUrl: 'app/templates/usuario/usuario-edit.html', controller: 'usuarioController' });
        $routeProvider.when('/usuario/:id', { templateUrl: 'app/templates/usuario/usuario-view.html', controller: 'usuarioController' });
        $routeProvider.when('/empresa', { templateUrl: 'app/templates/empresa/empresas.html', controller: 'empresaController' });
        $routeProvider.when('/empresa/add', { templateUrl: 'app/templates/empresa/empresa-add.html', controller: 'empresaController' });
        $routeProvider.when('/empresa/:id/edit', { templateUrl: 'app/templates/empresa/empresa-edit.html', controller: 'empresaController' });
        $routeProvider.when('/empresa/:id', { templateUrl: 'app/templates/empresa/empresa-view.html', controller: 'empresaController' });
        $routeProvider.when('/area', { templateUrl: 'app/templates/area/areas.html', controller: 'areaController' });
        $routeProvider.when('/area/add', { templateUrl: 'app/templates/area/area-add.html', controller: 'areaController' });
        $routeProvider.when('/area/:id/edit', { templateUrl: 'app/templates/area/area-edit.html', controller: 'areaController' });
        $routeProvider.when('/area/:id', { templateUrl: 'app/templates/area/area-view.html', controller: 'areaController' });
        $routeProvider.when('/alterarsenha', { templateUrl: 'app/templates/usuario/usuario-alterarSenha.html', controller: 'alterarSenhaController' });
        $routeProvider.when('/esquecisenha', { templateUrl: 'app/templates/usuario/usuario-esqueciSenha.html', controller: 'esqueciSenhaController' });
        $routeProvider.when('/log', { templateUrl: 'app/templates/logs/logs.html', controller: 'logController' });
        $routeProvider.when('/paginanaoencontrada', { templateUrl: 'app/templates/paginaNaoEncontrada.html', controller: 'paginaNaoEncontradaController' });
        $routeProvider.when('/atendimento', { templateUrl: 'app/templates/atendimento/atendimentos.html', controller: 'atendimentoController' });
        $routeProvider.when('/atendimento/sala/:chaveEmpresa', { templateUrl: 'app/templates/atendimento/sala.html', controller: 'atendimentoSalaController' });
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

app.factory('socket', function ($rootScope) {
    var socket = io.connect('http://localhost:3000');
    return {
        on: function (eventName, callback) {
            socket.on(eventName, function () {
                var args = arguments;
                $rootScope.$apply(function () {
                    callback.apply(socket, args);
                });
            });
        },
        emit: function (eventName, data, callback) {
            socket.emit(eventName, data, function () {
                var args = arguments;
                $rootScope.$apply(function () {
                    if (callback) {
                        callback.apply(socket, args);
                    }
                });
            })
        }
    };
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
                var urlFuncionalidade = 'api/funcionalidade/GetForMenu';

                var headerAuth = { headers: { 'Authorization': 'Basic ' + newUser.token } };

                $http.get(urlFuncionalidade, headerAuth).success(function (data) {
                    newUser.menus = data;
                    $cookies.put('user', JSON.stringify(newUser));
                }).error(function (jqxhr, textStatus) {
                    toasterAlert.showAlert(jqxhr.message);
                });
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
            else {
                if (user.atendendo) {
                    $location.path('atendimento');
                }
            };
        }
    };
});