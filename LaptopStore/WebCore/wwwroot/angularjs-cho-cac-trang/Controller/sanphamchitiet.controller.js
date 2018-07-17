(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('SanPhamChiTietController', SanPhamChiTietController);

    SanPhamChiTietController.$inject = ['$location', '$scope', '$routeParams', 'TrangChuService', 'NgStorageService', '$rootScope', '$filter'];

    function SanPhamChiTietController($location, $scope, $routeParams, TrangChuService, ngStorageService, $rootScope, $filter) {
        /* jshint validthis:true */
        var vm = $scope;
        var rootScope = $rootScope;

        var idSanPham = $routeParams.id;
        vm.soluongspthemvaogiohang = 1;
        vm.themvaogiohang = themvaogiohang;

        khoitao();

        function khoitao() {
            
            laySanPhamChiTiet();
        }

        function laySanPhamChiTiet() {
            if (idSanPham != null) {
                TrangChuService.laysanphamchitiet(idSanPham)
                    .then(function (sp) {
                        vm.chitietsanpham = sp;
                        vm.chitietsanpham.soluong = 1;
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            } else {
                vm.chitietsanpham = vm.sanpham;
            }
            
        }

        function themvaogiohang(sanpham) {
            var sanphamtronggiohang = ngStorageService.laySession('giohang');

            if (angular.isUndefined(sanphamtronggiohang)) {
                sanpham.soluong = 1;
                sanphamtronggiohang = [sanpham]
                rootScope.giohang.sanpham.push(sanpham);
                rootScope.giohang.tongtien = sanpham.gia * sanpham.soluong;
            } else {
                if (sanphamtronggiohang.sanpham.length === 0) {
                    sanpham.soluong = 1;
                    sanphamtronggiohang.sanpham.push(sanpham);
                    rootScope.giohang.sanpham.push(sanpham);
                    rootScope.giohang.tongtien = sanpham.gia * sanpham.soluong;
                } else {
                    var truyvan = $filter('filter')(sanphamtronggiohang.sanpham, { id: sanpham.id });
                    if (truyvan && truyvan.length !== 0) {
                        sanphamtronggiohang.sanpham.forEach(function (p) {
                            if (p.id === sanpham.id) {
                                p.soluong++;
                                rootScope.giohang.tongtien += p.gia;
                            }
                        });
                    } else {
                        sanpham.soluong = 1;
                        sanphamtronggiohang.sanpham.push(sanpham);
                        rootScope.giohang.sanpham.push(sanpham);
                        rootScope.giohang.tongtien = sanpham.gia * sanpham.soluong;
                    }
                }

            }
            ngStorageService.ganSession('giohang', sanphamtronggiohang);
        }

    }
})();
