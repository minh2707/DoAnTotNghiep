(function () {
    'use strict';

    angular
        .module('AdminApp')
        .factory('AdminService', AdminService);

    AdminService.$inject = ['$http', '$q'];

    function AdminService($http, $q) {
        var service = {
            layhetsanpham: layhetsanpham,
            xoasanpham: xoasanpham,
            layhetloaisanpham: layhetloaisanpham,
            capnhatsanpham: capnhatsanpham,
            laymotsanpham: laymotsanpham,
            taosanpham: taosanpham,
            xoaloaisanpham: xoaloaisanpham,
            taokhachhang: taokhachhang,
            taodonhang: taodonhang,
            taochitietdonhang: taochitietdonhang,
            laymotloaisanpham: laymotloaisanpham,
            capnhatloaisanpham: capnhatloaisanpham,
            taoloaisanpham: taoloaisanpham,
            layhetdonhang: layhetdonhang,
            xoadonhang: xoadonhang,
            laychitietdonhang: laychitietdonhang,
            xoachitietdonhang: xoachitietdonhang
        };

        return service;

        function layhetsanpham() {
            var deferred = $q.defer();
            var api = 'http://localhost:49595/api/LayVaTimSanPham'

            $http.get(api)
                .then(function (sptimdc) {
                    deferred.resolve(sptimdc.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function xoasanpham(id) {
            var deferred = $q.defer();

            $http.delete('http://localhost:49595/api/XoaSanPham/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function layhetloaisanpham() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayHetLoaiSanPham')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function capnhatsanpham(sanpham) {
            var deferred = $q.defer();

            $http.put('http://localhost:49595/api/CapNhatSanPham/' + sanpham.id, sanpham)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laymotsanpham(id) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayMotSanPham/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;

        }

        function taosanpham(sp) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoSanPham', sp, { header: { 'Content-Type': 'application/json' } })
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function xoaloaisanpham(id) {
            var deferred = $q.defer();

            $http.delete('http://localhost:49595/api/XoaLoaiSanPham/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function taokhachhang(kh) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoKhachHang', kh)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function taodonhang(hd) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoDonHang', hd)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function taochitietdonhang(cthd) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoChiTietDonHang', cthd)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laymotloaisanpham(id) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayMotLoaiSanPham/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function capnhatloaisanpham(category) {
            var deferred = $q.defer();

            $http.put('http://localhost:49595/api/CapNhatLoaiSanPham', category)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function taoloaisanpham(loai) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoLoaiSanPham', loai)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function layhetdonhang() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayHetDonHang')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function xoadonhang(id) {
            var deferred = $q.defer();

            $http.delete('http://localhost:49595/api/XoaDonHang/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laychitietdonhang(id) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayChiTietDonHangCuaDonHang/' + id )
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function xoachitietdonhang(id) {
            var deferred = $q.defer();

            $http.delete('http://localhost:49595/api/XoaChiTietDonHang/' + id)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }
    }
})();