
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

}]);

/* On change of browse button, the name of the file appears */
$(document).ready(function () {
    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);

        /*var client = new XMLHttpRequest();
        client.open('GET', $(this).val());
        client.onreadystatechange = function () {
            $("#Textarea" + $(this).attr('id').charAt($(this).attr('id').length - 1)).text(client.responseText);
        }
        client.send();

        $(this).load($(this).val(), function (responseTxt, statusTxt, xhr) {
            $("#Textarea" + $(this).attr('id').charAt($(this).attr('id').length - 1)).text(responseTxt);
            //event.target.id[event.target.id])
        });

        fr = new FileReader();
        fr.onload = function () {
            $("#Textarea" + $(this).attr('id').charAt($(this).attr('id').length - 1)).text(fr.result);
            //event.target.id[event.target.id])
        };
        fr.readAsDataURL($(this).val());
        $.data({
            type: "GET",
            url: $(this).val(),
            dataType: "text",
            success: function (html) {
                $("#Textarea" + $(this).attr('id').charAt($(this).attr('id').length - 1)).text(html);
                //event.target.id[event.target.id])
            }
        });*/
    });
});