(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('DonHangAdminController', DonHangAdminController);

    DonHangAdminController.$inject = ['$location', 'AdminService', 'toastr', '$scope', '$uibModal', '$routeParams'];

    function DonHangAdminController($location, AdminService, toastr, $scope, $uibModal, $routeParams) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.xoamotdonhang = xoamotdonhang;

        vm.moCuaSo = moCuaSo;
        vm.dongcuaso = dongcuaso;

        vm.xoachitietdonhang = xoachitietdonhang;
        vm.idDonHang = $routeParams.id;

        khoitao();

        function khoitao() {
            if (vm.idDonHang && vm.idDonHang != null) {
                laychitietdonhang();
            } else {
                laytatcadonhang();
            }

        }

        function laychitietdonhang() {
            AdminService.laychitietdonhang(vm.idDonHang)
                .then(function (donhang) {
                    vm.chitietdonhang = donhang;
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function xoachitietdonhang(id) {
            AdminService.xoachitietdonhang(id)
                .then(function (res) {
                    toastr.success("Chi tiết đơn hàng " + id + " được xóa!");
                    if (vm.chitietdonhang.length === 1) {
                        AdminService.xoadonhang(vm.chitietdonhang[0].id)
                            .then(function (res) {
                                $location.path('/laytatcadonhang');
                                $location.replace();
                            })
                            .catch(function (err) {
                                toastr.error('Lỗi:' + JSON.stringify(err));
                            });
                    } else {
                        khoitao();
                    }
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function laytatcadonhang() {
            AdminService.layhetdonhang()
                .then(function (kq) {
                    vm.tatcahoadon = kq;
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function xoamotdonhang(id) {
            AdminService.xoadonhang(id)
                .then(function (kq) {
                    toastr.success("Hóa Đơn " + id + " Đã Xóa!");
                    vm.modalInstance.close('xoa');
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function moCuaSo(hd) {
            vm.hoadontrongcuaso = hd;

            vm.modalInstance = $uibModal.open({
                templateUrl: 'cuaso.html',
                controller: 'DonHangAdminController',
                scope: vm
            });

            vm.modalInstance.result.then(function (result) {
                khoitao();
            });
        }

        function dongcuaso() {
            vm.modalInstance.close('dong');
        }
    }
})();
