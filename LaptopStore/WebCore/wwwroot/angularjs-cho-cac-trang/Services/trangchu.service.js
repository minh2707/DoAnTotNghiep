(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .factory('TrangChuService', TrangChuService);

    TrangChuService.$inject = ['$http', '$q'];

    function TrangChuService($http, $q) {
        var service = {
            laytatcasanpham: laytatcasanpham,
            laynamsanphammoinhat: laynamsanphammoinhat,
            laytatcaloaisanpham: laytatcaloaisanpham,
            demsanpham: demsanpham,
            laysanphamchitiet: laysanphamchitiet,
            laygiatiennhonhat: laygiatiennhonhat,
            laygiatienlonnhat: laygiatienlonnhat,
            taokhachhang: taokhachhang,
            taohoadon: taohoadon,
            taochitietdonhang: taochitietdonhang,
            timkiemsanpham: timkiemsanpham
        };

        return service;

        function laytatcasanpham(dulieu) {
            var deferred = $q.defer();
            var api = 'http://localhost:49595/api/LayVaTimSanPham/?'

            if (dulieu) {
                if (dulieu.idLoai) {
                    api = api + '&IdLoai=' + dulieu.idLoai;
                }

                if (dulieu.giatu) {
                    api = api + '&GiaTu=' + dulieu.giatu;
                }

                if (dulieu.giaden) {
                    api = api + '&GiaDen=' + dulieu.giaden;
                }

                if (dulieu.giamgia) {
                    api = api + '&GiamGia=' + dulieu.giamgia;
                }

                if (dulieu.trangso) {
                    api = api + '&TrangSo= ' + dulieu.trangso;
                }
            }

            $http.get(api)
                .then(function (sanpham) {
                    deferred.resolve(sanpham.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laynamsanphammoinhat() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/SanPhamMoiNhat')
                .then(function (sanpham) {
                    deferred.resolve(sanpham.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laytatcaloaisanpham() {
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

        function demsanpham() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/DemTatCaSanPham')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laysanphamchitiet(idsanpham) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayMotSanPham/' + idsanpham)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;

        }

        function laygiatienlonnhat() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayGiaCaoNhat')
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function laygiatiennhonhat() {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/LayGiaThapNhat')
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

        function taohoadon(order) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/Orders', order)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function taochitietdonhang(ctdh) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/TaoChiTietDonHang', ctdh)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function timkiemsanpham(ten) {
            var deferred = $q.defer();

            $http.get('http://localhost:49595/api/TimKiemSanPhamTheoTen' + '?tensanpham=' + ten)
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