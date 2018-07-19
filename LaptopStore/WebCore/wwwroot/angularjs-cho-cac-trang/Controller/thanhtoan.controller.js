(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .controller('ThanhToanController', ThanhToanController);

    ThanhToanController.$inject = ['$location', 'TrangChuService', '$scope', '$rootScope', '$q', 'NgStorageService', '$sessionStorage'];

    function ThanhToanController($location, TrangChuService, $scope, $rootScope, $q, NgStorageService, $sessionStorage) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.kh = $rootScope.taikhoan;

        vm.thanhtoan = thanhtoan;

        khoitao();

        function khoitao() {
            const nguoidungdadangnhap = NgStorageService.layLocal('taikhoan');
            if (angular.isUndefined(nguoidungdadangnhap) || !nguoidungdadangnhap.id || nguoidungdadangnhap.id == null) {
                window.location.href = "/";
                window.location.replace();
            } else {
                vm.kh = nguoidungdadangnhap;
            }
        }

        function thanhtoan(sp, khachhang) {
            var taoKhachHang = function () {
                var khachhangdetao = {
                    Id: Math.floor((Math.random() * 9999999999) + 1),
                    HoTen: khachhang.tenHienThi,
                    Email: khachhang.email,
                    DiaChi: khachhang.diachi
                };

                TrangChuService.taokhachhang(khachhangdetao)
                    .then(function (kq) {
                        var hoadon = {
                            IdkhachHang: kq.id,
                            DiaChi: kq.diaChi,
                            SoLuong: $rootScope.giohang.sanpham.length,
                            NgayGiao: new Date(),
                            NgayDat: new Date(),
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
                    })
                    .catch(function (err) {
                        console.log(err);
                    });
            };

            taoKhachHang();
        }
    }
})();
