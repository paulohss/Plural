(function () {

    "use strict";

    angular.module("app-trips").controller("tripsController", tripsCntroller); //using existing module

    function tripsCntroller($http) {

        var viewModel = this;
        //viewModel.trips = [{
        //    name: "US Trip",
        //    created: new Date()
        //}];
        viewModel.trips = [];

        viewModel.newTrip = {}; //newTrip = Form in the UI

        viewModel.errorMessage = "";
        viewModel.isBusy = true;

        //
        $http.get("/api/trips")
            .then(function (response) {
            //success
            angular.copy(response.data, viewModel.trips); //like a foreach

        }, function (error) {
            viewModel.errorMessage = "Pau!";
        })
        .finally(function () {
            viewModel.isBusy = false;
        });

        //Add new item into the collection
        viewModel.addTrip = function () {
            //viewModel.trips.push({ name: viewModel.newTrip.name, created: new Date() });
            //viewModel.newTrip = {};
            viewModel.isBusy = true;
            viewModel.errorMessage = "";

            $http.post("/api/trips", viewModel.newTrip)
               .then(function (response) {
                   viewModel.trips.push(response.data);
                   viewModel.newTrip = {};
               }, function () {
                   viewModel.errorMessage = "Pau!";
               })
               .finally(function () {
                   viewModel.isBusy = false;
               });               
        };

        //
    }
})();