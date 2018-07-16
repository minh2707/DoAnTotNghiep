(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('LoaiSanPhamAdminController', LoaiSanPhamAdminController);

    LoaiSanPhamAdminController.$inject = ['$location', 'AdminService', 'toastr', '$scope', '$uibModal'];

    function LoaiSanPhamAdminController($location, AdminService, toastr, $scope, $uibModal) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.xoaLoaiSP = xoaLoaiSP;
        vm.moCuaSoLoaiSP = moCuaSoLoaiSP;
        vm.dongCuaSo = dongCuaSo;
        vm.loaiSP = {};
        vm.taoLoaiSP = taoLoaiSP;

        vm.laCapNhatSP = false;
        var idLoaiSP = $routeParams.id;
        vm.spPhaiCapNhat = {};
        vm.moChucNangSuaLoaiSP = moChucNangSuaLoaiSP;
        vm.capnhatSP = capnhatSP;
        vm.loaiPhaiCapNhat = {}

        khoitao();

        function khoitao() {
            if (idLoaiSP && idLoaiSP != null) {
                laymotloaisp();
            } else {
                laytatcaloaisp();
            }
        }

        function laytatcaloaisp() {
            AdminService.layhetloaisanpham()
                .then(function (kq) {
                    vm.cacloaisanpham = kq;
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function xoaLoaiSP(id) {
            AdminService.xoaloaisanpham(id)
                .then(function (kq) {
                    toastr.success("Loại " + id + " được xóa!");
                    vm.modalInstance.close('xoa');
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function moCuaSoLoaiSP(loai) {
            vm.loaiSPDaChon = loai;

            vm.modalInstance = $uibModal.open({
                templateUrl: 'cuasoloaisp.html',
                controller: 'LoaiSanPhamAdminController',
                scope: vm
            });

            vm.modalInstance.result.then(function (result) {
                khoitao();
            });
        }

        function dongCuaSo() {
            vm.modalInstance.close('xoa');
        }

        function taoLoaiSP(loai) {

            AdminService.taoloaisanpham(loai)
                .then(function (res) {
                    toastr.success("Tạo thành công!");
                    $location.path('/laytatcaloaisp');
                    $location.replace();
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                });
        }

        function laymotloaisp() {
            AdminService.getCategoryDetail(idLoaiSP)
                .then(function (res) {
                    vm.loaiSP = res;
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                })
        }

        function moChucNangSuaLoaiSP() {
            vm.laCapNhatSP = !vm.laCapNhatSP;
            vm.loaiPhaiCapNhat = angular.copy(vm.loaiSP);
        }

        function capnhatSP(loai) {
            var obj = {
                Id: loai.id,
                Ten: loai.ten,
                MoTa: loai.mota
            };

            AdminService.capnhatloaisanpham(obj)
                .then(function (res) {
                    toastr.success("Cập Nhật Thành Công");
                    vm.loaiSP = angular.copy(loai);
                    vm.laCapNhatSP = false;
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                })
        }
    }
})();
