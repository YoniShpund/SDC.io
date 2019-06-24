
var OverallApp = angular.module('OverallApp', []);
OverallApp.controller('OverallController', ['$scope', function ($scope) {

    $scope.move = function (Current, Next) {
        var isJsonNeeded = false;
        var xmlhttp = new XMLHttpRequest();
        switch (Next) {
            case "ModelDetailsArticle":
                xmlhttp.onreadystatechange = function () {
                    if (this.readyState == 4 && this.status == 200) {
                        var output = JSON.parse(this.responseText);

                        $scope.EncoderType = output["EncoderType"];
                        $scope.DecoderType = output["DecoderType"];
                        $scope.EncoderLayers = output["EncoderLayers"];
                        $scope.DecoderLayers = output["DecoderLayers"];
                        $scope.EncoderSize = output["EncoderSize"];
                        $scope.DecoderSize = output["DecoderSize"];
                        $scope.OptimizerType = output["OptimizerType"];
                        $scope.LearningRate = output["LearningRate"];
                        $scope.Dropout = output["Dropout"];

                        var AccData = [];
                        var acc = output["acc"];
                        var crossEntropyData = [];
                        var crossEntropy = output["cross_entropy"];
                        var learningRateData = [];
                        var learningRate = output["learning_rate"];
                        var trainLossData = [];
                        var trainLoss = output["ppl"];
                        for (var index = 0; index < acc.length; index++) {
                            AccData.push({ x: index, y: acc[index] });
                            crossEntropyData.push({ x: index, y: crossEntropy[index] });
                            learningRateData.push({ x: index, y: learningRate[index] });
                            trainLossData.push({ x: index, y: trainLoss[index] });
                        }

                        DrawChart("chartAccuracy", "Accuracy", AccData)
                        DrawChart("chartCrossEntropy", "Cross Entropy", crossEntropyData)
                        DrawChart("chartLearningRate", "Learning Rate", learningRateData)
                        DrawChart("chartTrainLoss", "Train Loss", trainLossData)

                    }
                };

                isJsonNeeded = true;
                break;

            default:
                break;
        }

        if (isJsonNeeded) {
            xmlhttp.open("GET", "Models\\" + $scope.ModelName + ".json", false);
            xmlhttp.send();
        }/*End of --> if (isJsonNeeded)*/

        $("#" + Current).fadeOut();
        /*Wait 1 second(s) until the current article disappears and then fade in the next one.*/
        setTimeout(function () {
            $("#" + Next).fadeIn();
        }, 1000);

        /*The next line is for preventing the default refresh of the page after the onClick event.*/
        event.preventDefault();
    };

    $scope.toggleModal = function () {
        $('#myModal').modal('toggle');

        $("#ContentMaster_StartButton").click();

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

    //$scope.ModelNames = <%=ModelNames %>;

}]);

function DrawChart(ChartId, ChartTitle, ChartData) {
    var chart = new CanvasJS.Chart(ChartId, {
        animationEnabled: true,
        theme: "light1",//light1
        title: {
            text: ChartTitle
        },
        data: [
            {
                // Change type to "bar", "splineArea", "area", "spline", "pie",etc.
                type: "line",
                dataPoints: ChartData
            }
        ]
    });

    chart.render();
}

/* On change of browse button, the name of the file appears */
$(document).ready(function () {
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        TextArea = this.id[this.id.length - 1];

        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var output = e.target.result;

                $("#Textarea" + TextArea).val(output);
            };
            reader.readAsText(this.files[0]);
        }
    });
});