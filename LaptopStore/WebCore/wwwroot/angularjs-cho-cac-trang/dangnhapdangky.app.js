(function () {
    'use strict';

    var app = angular.module('DangNhapDangKyApp', [
        // Angular modules 
        'ngRoute',
        'toastr',
        'ngStorage',
        'ngMaterial'

        // Custom modules 

        // 3rd Party Modules

    ]);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/dangky', {
                templateUrl: '../Html/dangky.html',
                controller: 'DangKyController'
            })
            .when('/dangnhap', {
                templateUrl: '../Html/dangnhap.html',
                controller: 'DangNhapController'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    
})();