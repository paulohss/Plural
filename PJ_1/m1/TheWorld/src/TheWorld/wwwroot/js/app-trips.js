﻿// app-trips.js
(function () {

    "use strict";

    // Creating the Module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
      .config(function ($routeProvider) {

          $routeProvider.when("/", {
              controller: "tripsController",
              controllerAs: "viewModel",
              templateUrl: "/views/tripsView.html"
          });

          $routeProvider.when("/editor/:tripName", {  //        /*used at tripview <td><a ng-href="#/editor/{{ trip.name }}" bla bal*/
              controller: "tripEditorController",           
              controllerAs: "viewModel",
              templateUrl: "/views/tripEditorView.html"
          });

          $routeProvider.otherwise({ redirectTo: "/" });

      });

})();