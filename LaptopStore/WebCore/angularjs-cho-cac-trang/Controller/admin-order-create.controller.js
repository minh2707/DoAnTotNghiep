﻿(function () {
    'use strict';

    angular
        .module('AdminApp')
        .controller('AdminOrderCreate', AdminOrderCreate);

    AdminOrderCreate.$inject = ['$location', 'AdminService', 'toastr', '$scope'];

    function AdminOrderCreate($location, AdminService, toastr, $scope) {
        /* jshint validthis:true */
        var vm = $scope;

        vm.category = {};
        vm.doCreateCategory = doCreateCategory;


        activate();

        function activate() {
        }

        function doCreateCategory(category) {

            AdminService.createCategory(category)
                .then(function (res) {
                    toastr.success("Create Category Successfully!");
                    $location.path('/categories');
                    $location.replace();
                })
                .catch(function (err) {
                    toastr.error("Lỗi:" + JSON.stringify(err));
                });
        }
    }
})();