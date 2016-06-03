app.controller('login_ctrl', [
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {
        
        $scope.login = function () {
            var data = {
                user_id: $scope.user_id,
                password: $scope.password
            };
            if(data.user_id == '' || data.password == '' ){
                return;
            }
            $http.post("/User/LoginAction", data).success(function (data) {
                if (data.success) {
                    $window.location.href = '/User/Index';
                } else {
                    $scope.message = "<b>Waring: </b> Invalid User and Password.";
                }                
            });
        };

    }
]);