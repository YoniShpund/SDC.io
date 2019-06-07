
var OverallApp = angular.module('OverallApp', []);
OverallApp.controller('OverallController', ['$scope', function ($scope) {

    $scope.move = function (Current, Next) {
        $("#" + Current).fadeOut();
        /*Wait 1 second(s) until the current article disappears and then fade in the next one.*/
        setTimeout(function () {
            $("#" + Next).fadeIn();
        }, 1000);

        /*The next line is for preventing the default refresh of the page after the onClick event.*/
        event.preventDefault();
    };

    $scope.ModelParams = [
        { ngModel: "ModelName", Name: "Model Name" },
        { ngModel: "EncoderType", Name: "Encoder Type" },
        { ngModel: "DecoderType", Name: "Decoder Type" },
        { ngModel: "EncoderLayers", Name: "Encoder Layers" },
        { ngModel: "DecoderLayers", Name: "Decoder Layers" },
        { ngModel: "EncoderSize", Name: "Encoder Size" },
        { ngModel: "DecoderSize", Name: "Decoder Size" },
        { ngModel: "OptimizerType", Name: "Optimizer Type" },
        { ngModel: "LearningRate", Name: "Learning Rate" },
        { ngModel: "Dropout", Name: "Dropout" }
    ];

    $scope.ModelNames = ["en-de", "de-en"];

    $scope.DisplayModelData = function () {
        /*TODO: Add HTTP request to get the data of the model*/
        $scope.OptimizerType = "ADAM";
    };
}]);