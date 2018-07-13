(function () {
    'use strict';

    angular
        .module('DangNhapDangKyApp')
        .controller('DangKyController', DangKyController);

    DangKyController.$inject = ['$location', '$scope', 'TaiKhoanService', 'toastr', '$window'];

    function DangKyController($location, $scope, TaiKhoanService, toastr, $window) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.taikhoan = {};
        vm.nhaplaimatkhausai = false;
        vm.matkhausai = false;

        vm.sosanhmatkhau = sosanhmatkhau;
        vm.kiemtramatkhau = kiemtramatkhau;
        vm.dangkytaikhoan = dangkytaikhoan;

        function sosanhmatkhau() {
            if (vm.taikhoan.matkhau !== vm.taikhoan.nhaplaimatkhau) {
                vm.nhaplaimatkhausai = true;
            } else {
                vm.nhaplaimatkhausai = false;
            }
        }

        function kiemtramatkhau(pass) {
            var re = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z])(?!.*\s).{6,255}$/;  

            if (!angular.isUndefined(pass)) {
                if (pass.match(re)) {
                    vm.matkhausai = false;
                } else {
                    vm.matkhausai = true;
                }
            } else {
                vm.matkhausai = false;
            }
        }

        function dangkytaikhoan(taikhoan) {
            if (taikhoan) {
                var taikhoandungdedangky = {
                    Email: taikhoan.email,
                    MatKhau: taikhoan.matkhau,
                    NhapLaiMatKhau: taikhoan.nhaplaimatkhau,
                    Ten: taikhoan.ten,
                    Ho: taikhoan.ho,
                }
                TaiKhoanService.dangkytaikhoan(taikhoandungdedangky)
                    .then(function (res) {
                        toastr.success('Đăng Ký Thành Công');
                        $window.location.href='/TaiKhoan/DangNhap';
                        //$location.replace();
                    })
                    .catch(function (err) {
                        toastr.error(err.message);
                    })
            }
        }
    }
})();
