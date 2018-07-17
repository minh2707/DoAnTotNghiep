(function () {
    'use strict';

    angular
        .module('laptopStoreApp')
        .factory('NgStorageService', NgStorageService);

    NgStorageService.$inject = ['$http', '$localStorage', '$sessionStorage'];

    function NgStorageService($http, $localStorage,
        $sessionStorage) {
        var service = {
            ganSession: ganSession,
            ganLocal: ganLocal,
            laySession: laySession,
            layLocal: layLocal,
            xoaHetSession: xoaHetSession
        };

        return service;

        function ganSession(tukhoa, giatri) {
            if (angular.isObject(giatri)) {
                $sessionStorage[tukhoa] = JSON.stringify(giatri);
            } else {
                $sessionStorage[tukhoa] = giatri
            }
        }

        function ganLocal(tukhoa, giatri) {
            if (angular.isObject(giatri)) {
                $localStorage[tukhoa] = JSON.stringify(giatri);
            } else {
                $localStorage[tukhoa] = giatri;
            }
        }

        function laySession(tukhoa) {
            try {
                return JSON.parse($sessionStorage[tukhoa])
            } catch (err) {
                return $sessionStorage[tukhoa];
            }
        }

        function layLocal(tukhoa) {
            try {
                return JSON.parse($localStorage[tukhoa]);
            } catch(err) {
                return $localStorage[tukhoa];
            }
        }

        function xoaHetSession() {
            $localStorage.$reset();
        }
    }
})();