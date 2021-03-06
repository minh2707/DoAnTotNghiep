﻿(function () {
    'use strict';

    var app = angular.module('laptopStoreApp', [
        // Angular modules 
        'ngRoute',
        'ngSanitize',
        'angularUtils.directives.dirPagination',
        'fancyboxplus',
        'rzModule',
        'ngStorage',
        'ngMaterial',
        'ui.bootstrap',
        'ngMap'

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
            .when('/thanhtoanthanhcong', {
                templateUrl: 'Html/thanhtoanthanhcong.html',
                controller: ''
            })
            .otherwise({
                redirectTo: '/'
            });
    }]);

    app.run(['NgStorageService', '$rootScope', '$http', '$window', function (ngStorageService, $rootScope, $http, $window) {
        $rootScope.taikhoan = ngStorageService.layLocal('');
        console.log($rootScope.taikhoan);
        $rootScope.giohang = {};
        $rootScope.giohang.sanpham = [];
        $rootScope.giohang.tongtien = 0;

        $rootScope.xoasanphamtronggiohang = xoasanphamtronggiohang;
        $rootScope.dangxuat = dangxuat;

        if ($rootScope.taikhoan) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + $rootScope.taikhoan.token;
        }

        $rootScope.$watchCollection('giohang.sanpham', function (giatrimoi, giatricu) {
            var tongtien = 0;

            if (!angular.isUndefined(giatrimoi) || giatrimoi.length !== 0 || giatrimoi !== null) {

                if (giatrimoi.soluong != giatricu.soluong) {
                    giatricu.soluong = giatrimoi.soluong
                }

                giatrimoi.forEach(function (sanpham) {
                    tongtien += (sanpham.gia * sanpham.soluong);
                });

                $rootScope.giohang.tongtien = tongtien;
            } else {
                $rootScope.giohang.tongtien = tongtien;
            }

            ngStorageService.ganSession('giohang', $rootScope.giohang);

        }, true);

        function xoasanphamtronggiohang(sanpham) {
            
        }

        function dangxuat() {
            $http.get('http://localhost:49595/api/DangXuat', { headers: { Authorization: 'Bearer ' + $rootScope.taikhoan.token } })
                .then(function (res) {
                    $rootScope.taikhoan = null;

                    ngStorageService.xoaHetSession();
                    ngStorageService.xoaHetLocal();
                    $window.location.href = '/TaiKhoan/#!/dangnhap';
                    $window.location.replace();
                })
                .catch(function (err) {
                    console.log(err);
                });
        };

        var timgiohang = ngStorageService.laySession('giohang');
        if (angular.isObject(timgiohang) && timgiohang.hasOwnProperty('sanpham') && timgiohang.hasOwnProperty('tongtien')) {
            $rootScope.giohang = timgiohang
        } else {
            $rootScope.giohang = $rootScope.giohang;
        }

        ngStorageService.ganSession('giohang', $rootScope.giohang);

    }]);

})();