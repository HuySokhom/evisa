app.controller('singup_ctrl', [
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {
        $scope.sex = 'male';
        $scope.photo = '';
        $scope.save = function () {

            var dateofbirth = $scope.day + '/' + $scope.month + '/' + $scope.year;
            var passportIssue = $scope.day_passport + '/' + $scope.month_passport + '/' + $scope.year_passport;
            var passportExpire = $scope.day_passport_expire + '/' + $scope.month_passport_expire + '/' + $scope.year_passport_expire;
            var Json = {
                surname: $scope.surname,
                middle_name: $scope.middleName,
                given_name: $scope.givenName,
                sex: $scope.sex,
                dob: dateofbirth,
                country: $scope.country.name,
                nationality: $scope.nationality.name,
                photo: $scope.photo,
                passport_no: $scope.passportNo,
                country_issue: $scope.countryIssue.name,
                passport_issue: passportIssue,
                passport_expire: passportExpire,
                contact_name: $scope.contactName,
                primary_email: $scope.primaryEmail,
                secondary_email: $scope.secondaryEmail,
                phone_number: $scope.phoneNumber,
                residential_address: $scope.residentialAddress,
                heard_from: $scope.heardFrom.name,
                user_id: $scope.userId,
                password: $scope.password
            };
            //console.log(Json); return;
            $http.post("/User/SignUp", Json).success(function (data) {
                console.log(data.success);
                if (data.success) {
                    //$window.location.href = '/User/Success';
                }
                
            });
        };

        function readURL(input) {
            if (input.files && input.files[0]) {
                var data = new FormData();
                var files = $("#upload_image").get(0).files;
                if (files.length > 0) {
                    data.append("MyImages", files[0]);
                }
                
                $.ajax({
                    url: "/User/UploadImage",
                    type: "POST",
                    processData: false,
                    contentType: false,
                    data: data,
                    success: function (response) {
                        console.log(response);
                        //code after success
                        $scope.photo = '/Uploads/Users/' + response;
                        console.log($scope.photo);
                        //$("#blah").attr('src', '/Uploads/Users/' + response);

                        //console.log(data);
                        //var reader = new FileReader();

                        //reader.onload = function (e) {
                        //    $scope.photo = e.target.result;
                        //}

                        //reader.readAsDataURL(input.files[0]);

                        //console.log(files);

                    },
                    error: function (er) {
                        console.log(er);
                    }
                });
            }
        }

        $("#upload_image").change(function () {
            readURL(this);
        });

    }
]);