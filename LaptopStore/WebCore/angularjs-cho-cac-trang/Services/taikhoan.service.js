(function () {
    'use strict';

    angular
        .module('DangNhapDangKyApp')
        .factory('TaiKhoanService', TaiKhoanService);

    TaiKhoanService.$inject = ['$http', '$q'];

    function TaiKhoanService($http, $q) {
        var service = {
            dangkytaikhoan: dangkytaikhoan,
            dangnhaptaikhoan: dangnhaptaikhoan
        };

        return service;

        function dangkytaikhoan(taikhoan) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/DangKy', taikhoan)
                .then(function (res) {
                    deferred.resolve(res.data);
                })
                .catch(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;

        }

        function dangnhaptaikhoan(taikhoan) {
            var deferred = $q.defer();

            $http.post('http://localhost:49595/api/DangNhap', taikhoan)
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