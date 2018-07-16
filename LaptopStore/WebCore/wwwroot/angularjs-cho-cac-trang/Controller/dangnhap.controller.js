(function () {
    'use strict';

    angular
        .module('DangNhapDangKyApp')
        .controller('DangNhapController', DangNhapController);

    DangNhapController.$inject = ['$location', '$scope', 'TaiKhoanService', 'toastr', '$localStorage', '$window','$rootScope'];

    function DangNhapController($location, $scope, TaiKhoanService, toastr, $localStorage, $window, $rootScope) {
        /* jshint validthis:true */
        var vm = $scope;
        var rvm = $rootScope;

        vm.dangnhap = dangnhap;

        khoitao();

        function khoitao() {
            if ($localStorage['taikhoan'] || rvm.taikhoan) {
                $window.location.href = '/';
            }
        }

        function dangnhap(taikhoan) {
            if (taikhoan) {
                var taikhoandungdedangnhap = {
                    Email: taikhoan.email,
                    MatKhau: taikhoan.matkhau
                }

                TaiKhoanService.dangnhaptaikhoan(taikhoandungdedangnhap)
                    .then(function (res) {
                        toastr.success('Đăng Nhập Thành Công!');
                        var tk = res;
                        $localStorage['taikhoan'] = JSON.stringify(res);
                        if (tk.laAdmin) {
                            $window.location.href = 'TrangChu/Admin';

                        } else {
                            $window.location.href = '/';
                        }
                    })
                    .catch(function (err) {
                        toastr.error(err.message);
                    });
            }
        }
    }
})();
