
var ModelSelectApp = angular.module('ModelSelectApp', []);
ModelSelectApp.controller('ModelSelectController', ['$scope', function ($scope) {
    $scope.Params = [
        "EncoderType",
        "DecoderType",
        "EncoderLayers",
        "DecoderLayers",
        "EncoderSize",
        "DecoderSize",
        "OptimizerType",
        "LearningRate",
        "Dropout"
    ];
    $scope.DisplayModelData = function () {
        /*TODO: Add HTTP request to get the data of the model*/
        $scope.OptimizerType += "ADAM";
    };
}]);