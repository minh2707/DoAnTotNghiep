(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('ThanhToanController', ThanhToanController);

    ThanhToanController.$inject = ['$location', 'TrangChuService', '$scope', '$rootScope', '$q', 'NgStorageService', '$sessionStorage', 'NgMap', 'GeoCoder'];

    function ThanhToanController($location, TrangChuService, $scope, $rootScope, $q, NgStorageService, $sessionStorage, NgMap, GeoCoder) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.kh = $rootScope.taikhoan;

        vm.thanhtoan = thanhtoan;

        vm.chonDiaChi = chonDiaChi;

        vm.bando = null;

        khoitao();

        function khoitao() {
            const nguoidungdadangnhap = NgStorageService.layLocal('taikhoan');
            if (angular.isUndefined(nguoidungdadangnhap) || !nguoidungdadangnhap.id || nguoidungdadangnhap.id == null) {
                window.location.href = "/";
                window.location.replace();
            } else {
                vm.kh = nguoidungdadangnhap;
            }

            NgMap.getMap().then(function (bando) {
                vm.bando = bando;
            });
        }

        function chonDiaChi() {
            vm.diadiem = this.getPlace();
            vm.map.setCenter(vm.diadiem.geometry.location);
        }

        function thanhtoan(sp, khachhang) {

                        var hoadon = {
                            IdkhachHang: khachhang.id,
                            DiaChi: khachhang.diachi,
                            SoLuong: $rootScope.giohang.sanpham.length,
                            NgayGiao: new Date(),
                            NgayDat: new Date(),
                            HinhThucThanhToan: khachhang.hinhThucThanhToan,
                            TrangThai: false
                        };

                        TrangChuService.taohoadon(hoadon)
                            .then(function (kq) {
                                var mangCacViecCanHoanThanh = [];
                                for (var i = 0; i < sp.length; i++) {
                                    var ctdh = {
                                        IdDonHang: kq.id,
                                        GiamGia: sp[i].giamGia,
                                        IdSanPham: sp[i].id,
                                        SoLuong: sp[i].soluong,
                                        DonGia: sp[i].gia,
                                        Id: Math.floor((Math.random() * 9999999999) + 1)
                                    }

                                    mangCacViecCanHoanThanh.push(TrangChuService.taochitietdonhang(ctdh));
                                }

                                if (mangCacViecCanHoanThanh.length > 0) {
                                    $q.all(mangCacViecCanHoanThanh)
                                        .then(function (data) {
                                            delete $sessionStorage['giohang']
                                            $rootScope.giohang = {
                                                sanpham: []
                                            };

                                            $location.path('/thanhtoanthanhcong');
                                            $location.replace();
                                        })
                                        .catch(function (err) {
                                            console.log(err);
                                       })
                                }

                            })
                            .catch(function (err) {
                                console.log(err);
                            })

        }
    }
})();
