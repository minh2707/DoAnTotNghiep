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

                        TrangChuService.taodonhang(hoadon)
                            .then(function (kq) {
                                var mangCacViecCanHoanThanh = [];
                                for (var i = 0; i < sp.length; i++) {
                                    var ctdh = {
                                        IddonHang: kq.id,
                                        GiamGia: sp[i].giamGia,
                                        IdsanPham: sp[i].idSanPham,
                                        SoLuong: sp[i].soLuong,
                                        DonGia: sp[i].donGia,
                                        Id: null
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

                                            $location.path('/');
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
