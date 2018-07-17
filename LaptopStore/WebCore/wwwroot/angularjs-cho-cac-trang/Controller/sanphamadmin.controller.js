(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('SanPhamAdminController', SanPhamAdminController);

    SanPhamAdminController.$inject = ['$location', 'AdminService', 'toastr', '$scope', '$uibModal', '$routeParams', '$q', 'Upload', '$timeout'];

    function SanPhamAdminController($location, AdminService, toastr, $scope, $uibModal, $routeParams, $q, Upload, $timeout) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.xoasanpham = xoasanpham;
        vm.moCuaSo = moCuaSo;
        vm.dongCuaSo = dongCuaSo;

        vm.laCheDoCapNhat = false;
        var idCuaSanPham = $routeParams.id;
        vm.sanPhamCanPhaiSua = {};
        vm.sanpham = {};
        vm.tatCaLoaiSP = [];
        vm.moChucNangCapNhat = moChucNangCapNhat;
        vm.capNhatSP = capNhatSP;

        vm.sanphamcantao = {
            hinh: {
                animate: true,
                tiendo: 0
            }
        };
        vm.taoSP = taoSP;

        khoitao();

        function khoitao() {
            if (idCuaSanPham && idCuaSanPham != null) {
                layMotSanPham();
            } else {
                layTatCaSanPham();
            }

            laytatcaloaisp();
        }

        function layTatCaSanPham() {
            AdminService.layhetsanpham()
                .then(function (sp) {
                    vm.tatcasanpham = sp;
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function xoasanpham(id) {
            AdminService.xoasanpham(id)
                .then(function (sanpham) {
                    toastr.success("Sản Phẩm " + id + " được xóa!");
                    vm.modalInstance.close('deleted');
                })
                .catch(function (err) {
                    toastr.error('Lỗi:' + JSON.stringify(err));
                });
        }

        function moCuaSo(sanpham) {
            vm.sanphamtrongcuaso = sanpham;

            vm.modalInstance = $uibModal.open({
                templateUrl: 'cuaso.html',
                controller: 'SanPhamAdminController',
                scope: vm
            });

            vm.modalInstance.result.then(function (result) {
                khoitao();
            });
        }

        function dongCuaSo() {
            vm.modalInstance.close('close');
        }

        function layMotSanPham() {
            AdminService.laymotsanpham(idCuaSanPham)
                .then(function (kq) {
                    vm.sanpham = kq;
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                })
        }

        function moChucNangCapNhat() {
            vm.laCheDoCapNhat = !vm.laCheDoCapNhat;
            vm.sanPhamCanPhaiSua = angular.copy(vm.sanpham);
        }

        function laytatcaloaisp() {
            AdminService.layhetloaisanpham()
                .then(function (kq) {
                    vm.tatCaLoaiSP = kq;
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                });

        }

        function capNhatSP(sp) {
            if (sp.idLoai && angular.isObject(sp.idLoai)) {
                sp.idLoai = sp.idLoai.id;
            }

            AdminService.capnhatsanpham(sp)
                .then(function (res) {
                    toastr.success("Cập Nhật Thành Công");
                    vm.sanpham = angular.copy(sp);
                    vm.laCheDoCapNhat = false;
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                })
        }

        function taoSP(sp) {
            taiHinhLenServer(sp.hinh)
                .then(function (res) {

                    var obj = {
                        Idloai: sp.idloai.id,
                        Ten: sp.ten,
                        Gia: sp.gia,
                        Hinh: res.taptin.fileName,
                        MoTa: sp.mota,
                        SoLuong: sp.soluong,
                        GiamGia: sp.giamgia,
                    };

                    if (sp.trangthai === 'true') {
                        obj.TrangThai = true;
                    } else {
                        obj.TrangThai = false;
                    }

                    AdminService.taosanpham(obj)
                        .then(function (res) {
                            toastr.success("Tạo Thành Công!");
                            $location.path('/tatcasanpham');
                            $location.replace();
                        })
                        .catch(function (err) {
                            toastr.error("Lỗi:" + JSON.stringify(err));
                            console.log(err);
                        });
                })
                .catch(function (err) {
                    if (err.status > 0) {
                        toastr.error(err.status + ': ' + err.data);
                        console.log(err);
                    }
                });

        }

        function taiHinhLenServer(taptin) {
            var deferred = $q.defer();

            Upload.upload({
                url: 'http://localhost:49595/api/CapNhatAnh',
                data: { taptin: taptin },
                method: 'POST'
            }).then(function (res) {
                $timeout(function () {
                    vm.sanphamcantao.hinh.animate = false;
                    deferred.resolve(res.data);
                });
                }, function (err) {
                    deferred.reject(err);
                }, function (evt) {
                    taptin.tiendo = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                });

            return deferred.promise;

        }
    }
})();
