(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('TrangChuController', TrangChuController);

    TrangChuController.$inject = [
        '$location',
        'TrangChuService',
        '$scope',
        '$uibModal',
        '$q',
        '$rootScope',
        'NgStorageService',
        '$filter'
    ];

    function TrangChuController($location, TrangChuService, $scope, $uibModal, $q, $rootScope, ngStorageService, $filter) {
        /* jshint validthis:true */
        var vm = $scope;
        var rootScope = $rootScope;

        
        vm.sanphammoinhat = null;
        vm.sanpham = null;
        vm.cautruyvandelocsanpham = {};
        vm.tongsosanpham = 0;
        vm.thanhlocgia = {
            tuychon: {
                buoc: 1000000
            }
        };
        vm.sapxep = {}
        vm.tukhoa = '';
        
        vm.mosanphamchitietcuaso = mosanphamchitietcuaso;
        vm.themgiohang = themgiohang;
        vm.laysanphamtheoloai = laysanphamtheoloai;
        vm.timkiemsanpham = timkiemsanpham;

        khoitao();

        function khoitao() {
           
            laytatcasanpham();
            laynamsanphammoinhat();
            layhetloaisanpham();
            demtongsosanpham();
            laygialonnhatvanhonhat();

        }

        function laytatcasanpham() {
            
            TrangChuService.laytatcasanpham()
                .then(function (res) {
                    vm.sanpham = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function laynamsanphammoinhat() {
            TrangChuService.laynamsanphammoinhat()
                .then(function (res) {
                    vm.sanphammoinhat = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function layhetloaisanpham() {
            TrangChuService.laytatcaloaisanpham()
                .then(function (res) {
                    vm.loaisanpham = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }
        
        function demtongsosanpham() {
            TrangChuService.demsanpham()
                .then(function (res) {
                    vm.tongsosanpham = res;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function mosanphamchitietcuaso(sp) {

            vm.sanphamchitiet = sp;

            var modalInstance = $uibModal.open({
                templateUrl: '../Html/sanphamchitiet-cuaso.html',
                controller: 'SanPhamChiTietController',
                size: 'lg',
                scope: $scope
               
            });
        }

        function laysanphamtheoloai(idloai) {
            vm.cautruyvandelocsanpham.idLoai = idloai;
            TrangChuService.laytatcasanpham(vm.cautruyvandelocsanpham)
                .then(function (sanpham) {
                    vm.sanpham = sanpham;
                })
                .catch(function (err) {
                    console.log(err);
                });
        }

        function laygialonnhatvanhonhat() {
            var laygianhonhat = function () {
                TrangChuService.laygiatiennhonhat()
                    .then(function (gianhonhat) {
                        vm.thanhlocgia.gianhonhat = gianhonhat;
                        vm.thanhlocgia.tuychon.canduoi = gianhonhat;
                        laygiatienlonnhat();
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            };

            var laygiatienlonnhat = function () {
                TrangChuService.laygiatienlonnhat()
                    .then(function (gialonnhat) {
                        vm.thanhlocgia.giatrilonnhat = gialonnhat;
                        vm.thanhlocgia.tuychon.cantren = gialonnhat;
                        vm.$broadcast('rzSliderForceRender');
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            };

            laygianhonhat();
        }

        function themgiohang(sanpham) {
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
            rootScope.giohang = angular.copy(sanphamtronggiohang);
            ngStorageService.ganSession('giohang', sanphamtronggiohang);
        }

        function timkiemsanpham(tukhoa) {
            if (tukhoa) {
                TrangChuService.timkiemsanpham(tukhoa)
                    .then(function (res) {
                        vm.sanpham = res;
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            } else {
                laytatcasanpham();
            }
        }

        
    }
})();
