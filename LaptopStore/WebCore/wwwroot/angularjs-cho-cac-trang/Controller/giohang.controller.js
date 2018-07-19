(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('GioHangController', GioHangController);

        GioHangController.$inject = ['$location', 'NgStorageService', '$rootScope', '$filter','$scope'];

    function GioHangController($location, ngStorageService, $rootScope, $filter, $scope) {
        /* jshint validthis:true */
        var vm = $scope;
        var rootScope = $rootScope;
        vm.chuyenDenTrangchu = chuyenDenTrangchu;
        vm.capnhatSoLuongSanPham = capnhatSoLuongSanPham;


        khoitao();

        function khoitao() { }

        function chuyenDenTrangchu() {
            $location.path('/');
            $location.replace();
        }

        function capnhatSoLuongSanPham(sanphamso, sl) {
            var giohanglayduoc = ngStorageService.laySession('giohang');
            giohanglayduoc.sanpham[sanphamso].soluong = sl;
            $rootScope.giohang = giohanglayduoc;
            ngStorageService.ganSession('giohang', giohanglayduoc);
        }
    }
})();
