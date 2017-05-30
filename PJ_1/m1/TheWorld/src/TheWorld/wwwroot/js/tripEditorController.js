(function () {
    "use strict";

    angular.module("app-trips")
      .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams) {

        var viewModel = this;
        viewModel.tripName = $routeParams.tripName;//from app-trips:  $routeProvider.when("/editor/:tripName", (tripName)  
        viewModel.stops = [];
        viewModel.errorMessage = "";
        viewModel.isBusy = true;

    }

})();