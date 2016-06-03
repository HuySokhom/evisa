var app = angular.module(
	'main',
	[
		, 'ngSanitize'
	]
);


app.controller('app_ctrl',[
    '$scope',
    '$window',
    '$http',
    function ($scope, $window, $http) {
    
    // logoff functionality
    $scope.logoff = function () {
        $http.post("/User/LogOff/").success(function (data, status) {
            console.log(data);
            console.log(status);
            location.reload();
        });
    };

    // declear variable for apply form in dropdown box
    $scope.apply_items = [
        {
            id: 1,
            name: 'Airport',
        },
        {
            id: 2,
            name: 'Embassy',
        },
        {
            id: 3,
            name: 'Forum',
        },
        {
            id: 4,
            name: 'Friend',
        },
        {
            id: 5,
            name: 'Google',
        },
        {
            id: 6,
            name: 'Government Organization',
        },
        {
            id: 7,
            name: 'Guide Book',
        },
        {
            id: 8,
            name: 'MSN',
        },
        {
            id: 9,
            name: 'Newsletter',
        },
        {
            id: 10,
            name: 'Others',
        },
        {
            id: 11,
            name: 'Travel Agent',
        },
        {
            id: 12,
            name: 'Travel Magazine',
        },
        {
            id: 13,
            name: 'Travel Web Page',
        },
        {
            id: 14,
            name: 'Yahoo',
        }

    ];
    // function for application form 
    $scope.backApplication = function () {
        $window.location.href = '/Home/Apply';
    };
    $scope.nextApplication = function () {
        
        //if ($scope.Surname != '' && $scope.MiddleName != '' && $scope.GivenName != '') {
        //    return Materialize.toast('Please Fill Require Information In Application Form.', 4000);
        //}
        
        $window.location.href = '/Home/Review';
    };

    // sing up show prompt
    $scope.signUp = function () {
        $window.location.href = '/User/FQSignup';
    };
    
    // functionality for get countries
    $scope.getCountry = function () {
        $http.get("/Country/").success(function (data) {
            $scope.countries = data;
        });
    };

    //print page in payment page
    $scope.printPayment = function () {
        var printContents = document.body.innerHTML;
        var popupWin = window.open('', '', '');
        popupWin.document.open();
        popupWin.document.write(
            '<html><head>'
            + '<link href="/Content/bootstrap.css" rel="stylesheet">'
            + '<link href="/Content/materialize.css" rel="stylesheet">'
            + '<link href="/Content/angular-material.min.css" rel="stylesheet">'
            + '<link href="/Content/master_main.css" rel="stylesheet">'
            + '<link href="/Content/style.css" rel="stylesheet">'
            + '<link href="/Content/Site.css" rel="stylesheet" type="text/css" />'
            + '<link href="/Content/style.css" rel="stylesheet">'
            + '<link href="/Content/font-awesome.min.css" rel="stylesheet">'
            + '<link href="/Content/material_icon.css" rel="stylesheet">'
            + '</head><body onload="window.print()">'
            + printContents
            + '</body></html>'
        );
        popupWin.document.close();
    };

    $scope.changeLanguage = function (name, url, code) {
        var data = {
            name: name,
            url: url,
            language: code
        };
        $http.post("/Base/LanguageEven/", JSON.stringify(data)).success(function (data, status) {
            console.log(data);
            console.log(status);
            location.reload();
        });
    };

    $scope.tempData = [];
    /*
    / functionality for apply form
    / 
    */
    $scope.apply = function () {
        if ($scope.PrimaryEmail != $scope.RetypePrimaryEmail) {
            return Materialize.toast('Emails have to match!', 4000);
        }
        $scope.tempData.push({
            form_apply: {
                surname: $scope.Surname,
                given_name: $scope.GivenName,
                country: $scope.Country,
                phone_number: $scope.PhoneNumber,
                primary_email: $scope.PrimaryEmail,
                secondary_email: $scope.SecondaryEmail,
                retype_primary_email: $scope.RetypePrimaryEmail,
                heard_from: $scope.heardFrom.name
            }
        });
        $window.location.href = '/Home/Application';
    };

    $scope.editReview = function (params) {
        $scope.Surname = params.Surname;
        $scope.MiddleName = params.MiddleName;
        $scope.GivenName = params.GivenName;
        $scope.sex = params.Sex;
        $scope.day = params.Day;
        $scope.month = params.Month;
        $scope.year = params.Year;
        $scope.Region = params.Region;
        $scope.Nationality = params.Nationality;
        $scope.ResidentialAddress = params.ResidentialAddress;
        $scope.Photo = '/Images/avatar.png';
        $scope.IssuingDate = $scope.day_issue + '/' + $scope.month_issue + '/' + $scope.year_issue;
        $scope.ExpriryDate = $scope.day_expiry + '/' + $scope.month_expiry + '/' + $scope.year_expiry;
        $scope.CountryOfIssue = params.CountryOfIssue;
        $scope.EntryDate = $scope.day_entry + '/' + $scope.month_entry + '/' + $scope.year_entry;
        $scope.PortOfEntry = params.PortOfEntry;
        $scope.VisaType = params.VisaType;
        $scope.ModOfTravel = params.ModOfTravel,
        $scope.ArrivalVehicleNo = params.ArrivalVehicleNo,
        $scope.PassportNumber = params.PassportNumber;
        $scope.ArrivalTime = params.ArrivalTime,
        $scope.AddressDuringVisit = params.AddressDuringVisit,
        $scope.OrganizationPersonsTo = params.OrganizationPersonsTo,
        $(".apply_popup").modal('show');
    }; 

    // review app form    
    $scope.delete = true;
    $scope.applicationViews = [{1:'test'}];
    $scope.addMoreApplication = function () {
        $scope.applicationViews.push({
            Surname: $scope.Surname,
            MiddleName: $scope.MiddleName,
            GivenName: $scope.GivenName,
            Sex: $scope.sex,
            Day: $scope.day,
            Month: $scope.month,
            Year: $scope.year,
            Region: $scope.Region,
            Nationality: $scope.Nationality,
            ResidentialAddress: $scope.ResidentialAddress,
            PassportNumber: $scope.PassportNumber,
            Photo: '/Images/avatar.png',
            IssuingDate: $scope.day_issue + '/' + $scope.month_issue + '/' + $scope.year_issue,
            ExpriryDate: $scope.day_expiry + '/' + $scope.month_expiry + '/' + $scope.year_expiry,
            CountryOfIssue: $scope.CountryOfIssue,
            EntryDate: $scope.day_entry + '/' + $scope.month_entry + '/' + $scope.year_entry,
            PortOfEntry: $scope.PortOfEntry,
            VisaType: $scope.VisaType,
            ModOfTravel: $scope.ModOfTravel,
            ArrivalVehicleNo: $scope.ArrivalVehicleNo,
            ArrivalTime: $scope.ArrivalTime,
            AddressDuringVisit: $scope.AddressDuringVisit,
            OrganizationPersonsTo: $scope.OrganizationPersonsTo,
        });

        $('.apply_popup').modal('hide');
    };

    // remove app from remove form
    $scope.removeApp = function ($index) {
        $scope.delete = false;
        $('.apply_popup').modal('show');
        $scope.index = $index;        
    };
    $scope.closeAppForm = function () {
        $scope.delete = true;
    };
    $scope.deleteApplicationForm = function () {
        $('.apply_popup').modal('hide');
        $scope.applicationViews.splice($scope.index, 1);
        $scope.delete = true;
    };


}]);
// filter with range of year in the future
app.filter('rangeYearFuture', function () {
    return function (input, max) {
        max = parseInt(max); //Make string input int
        var currentYear = (new Date()).getFullYear();
        for (var i = 0; i <= max; i++) {
            var y = parseInt(currentYear + i);
            input.push(y);            
        }
        return input;
    };
});
// filter with range of year
app.filter('rangeYear', function () {
    return function (input, min) {
        min = parseInt(min); //Make string input int
        var currentYear = (new Date()).getFullYear();
        for (var i = min; i <= currentYear; i++)
            input.push(i);
        return input;
    };
});
// range with number 
app.filter('rangeNumber', function () {
    return function (input, total) {
        total = parseInt(total);
        for (var i = 1; i <= total; i++) {
            //if(i <= 9 ){
                
            //}
            input.push(i);
        }
        return input;
    };
});