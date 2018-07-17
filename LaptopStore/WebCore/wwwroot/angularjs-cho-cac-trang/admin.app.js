(function () {
    'use strict';

    var app = angular.module('AdminApp', [
        // Angular modules 
        'ngRoute',
        'toastr',
        'ui.bootstrap',
        'ngStorage',
        'ngFileUpload'

        // Custom modules 

        // 3rd Party Modules

    ]);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/tatcasanpham', {
                templateUrl: '/Html/Admin/Product/products.html',
                controller: 'SanPhamAdminController'
            })
            .when('/laymotsanpham/:id', {
                templateUrl: '/Html/Admin/Product/product-detail.html',
                controller: 'SanPhamAdminController'
            })
            .when('/taosanpham', {
                templateUrl: '/Html/Admin/Product/product-create.html',
                controller: 'SanPhamAdminController'
            })
            .when('/laytatcaloaisp', {
                templateUrl: '/Html/Admin/LoaiSanPham/tatcaloaisp.html',
                controller: 'LoaiSanPhamAdminController'
            })
            .when('/laymotloaisanpham/:id', {
                templateUrl: '/Html/Admin/LoaiSanPham/chitietloaisp.html',
                controller: 'LoaiSanPhamAdminController'
            })
            .when('/taoloaisp', {
                templateUrl: '/Html/Admin/LoaiSanPham/taoloaisp.html',
                controller: 'LoaiSanPhamAdminController'
            })
            .when('/laytatcadonhang', {
                templateUrl: '/Html/Admin/Order/order.html',
                controller: 'AdminOrderController'
            })
            .when('/laymotdonhang/:id', {
                templateUrl: '/Html/Admin/Order/order-detail.html',
                controller: 'AdminOrderController'
            })
            .when('/taodonhang', {
                templateUrl: '/Html/Admin/Order/order-create.html',
                controller: 'AdminOrderController'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    app.run(['$localStorage', '$rootScope', '$http', '$window', function ($localStorage, $rootScope, $http, $window) {
        var rvm = $rootScope;
        rvm.dangxuat = dangxuat;

        try {
            rvm.taikhoan = JSON.parse($localStorage['taikhoan']);
        } catch (err) {
            rvm.taikhoan = $localStorage['taikhoan'];
        }
        console.log(rvm.taikhoan);

        if (rvm.taikhoan) {

            if (rvm.taikhoan.laAdmin === false) {
                $window.location.href = '/';
            } else {
                $http.defaults.headers.common.Authorization = 'Bearer ' + rvm.taikhoan.token;
            }
        } else {
            $window.location.href = '/TaiKhoan/DangNhap';
        }

        function dangxuat() {
            $http.get('http://localhost:49595/api/DangXuat', { headers: { Authorization: 'Bearer ' + rvm.taikhoan.token } })
                .then(function (res) {
                    rvm.taikhoan = null;

                    $localStorage.$reset();

                    $window.location.href = '/TaiKhoan/DangNhap';
                })
                .catch(function (err) {
                    console.log(err);
                });
        };

    }]);


})();