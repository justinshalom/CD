
var m=angular.module('form', []);
m.config(function($httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
});
         m.controller('FormController', ['$scope', '$http','$element', function ($scope, $http,$element) {
             $scope.Server = "192.168.1.100";
             $scope.Database = "table";
             $scope.UID = "sa";
             $scope.Password = "admin";
             $scope.formData = {};
             $scope.submit = function (data) {
                 $scope.submitted = true;
                 console.log($scope.formData);
                 $http.post(apirooturl + 'SettingsApi/SaveDBConnection',$element.serialize()).then(successCallback, errorCallback);
             }

         }]);