(function () {
    'use strict';

    var app = angular.module('laptopStoreApp', [
        // Angular modules 
        'ngRoute',
        'ngSanitize',
        'ui.bootstrap',
        'angularUtils.directives.dirPagination',
        'fancyboxplus',
        'rzModule',
        'ngStorage'

        // Custom modules 

        // 3rd Party Modules

    ]);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'Html/trangchu.html',
                controller: 'TrangChuController'
            })
            .when('/sanpham/:id', {
                templateUrl: 'Html/sanphamchitiet.html',
                controller: 'SanPhamChiTietController'
            })
            .when('/giohang', {
                templateUrl: 'Html/giohang.html',
                controller: 'GioHangController'

            })
            .when('/thanhtoan', {
                templateUrl: 'Html/thanhtoan.html',
                controller: 'ThanhToanController'
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    app.run(['NgStorageService', '$rootScope', '$http', function (ngStorageService, $rootScope, $http) {
        $rootScope.taikhoan = ngStorageService.getLocalStorage('taikhoan');
        console.log($rootScope.taikhoan);
        $rootScope.giohang = {};

        $rootScope.xoasanphamtronggiohang = xoasanphamtronggiohang;
        $rootScope.dangxuat = dangxuat;

        if ($rootScope.taikhoan) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + $rootScope.taikhoan.token;
        }

        $rootScope.$watchCollection('giohang.sanpham', function (giatrimoi, giatricu) {
            var tongtien = 0;

            if (!angular.isUndefined(giatrimoi) || giatrimoi.length !== 0 || giatrimoi !== null) {

                giatrimoi.forEach(function (sanpham) {
                    tongtien += (sanpham.gia * sanpham.soluong);
                });

                $rootScope.giohang.tongtien = tongtien;
            } else {
                $rootScope.giohang.tongtien = tongtien;
            }
        }, true);

        function xoasanphamtronggiohang(sanpham) {
            var sanphamtronggiohang = ngStorageService.getSessionStorage('giohang');
            var stt = null;

            sanphamtronggiohang.forEach(function (p, i) {
                if (p.id === sanpham.id) {
                    stt = i;
                }
            });

            sanphamtronggiohang.splice(stt, 1);
            $rootScope.giohang.sanpham.splice(stt, 1);
            ngStorageService.setSessionStorage('giohang', sanphamtronggiohang);
        }

        function dangxuat() {
            $http.get('http://localhost:49595/api/DangXuat', { headers: { Authorization: 'Bearer ' + $rootScope.taikhoan.token } })
                .then(function (res) {
                    $rootScope.taikhoan = null;

                    ngStorageService.resetLocalStorage();
                })
                .catch(function (err) {
                    console.log(err);
                });
        };

        var timgiohang = ngStorageService.getSessionStorage('giohang');
        if (!angular.isUndefined(timgiohang)) {
            $rootScope.giohang.sanpham = timgiohang
        } else {
            $rootScope.giohang.sanpham = [];
        }

    }]);

})();