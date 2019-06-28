<%@ Page Title="Analyze" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="Analyze.aspx.cs" Inherits="SDC.io.Analyze" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="inc/canvasjs.min.js"></script>
    <script src="inc/angular.min.js"></script>
    <script src="inc/sdc.io.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMaster" runat="server">
    <div class="container body-content" data-ng-app="OverallApp" ng-controller="OverallController">
        <!--
        Model Details page
        -->
        <article class="container" id="ModelDetailsArticle" style="display: none;">
            <table>
                <tr ng-repeat="param in ModelParams">
                    <th class="input_field" style="text-align: right;">{{param.Name}}: </th>
                    <td ng-bind="{{param.ngModel}}" class="input_field"></td>
                </tr>
                <tr>
                    <td>
                        <div id="chartAccuracy" style="height: 400px; width: 100%;"></div>
                    </td>
                    <td>
                        <div id="chartTrainLoss" style="height: 400px; width: 100%;"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="chartCrossEntropy" style="height: 400px; width: 100%;"></div>
                    </td>
                    <td>
                        <div id="chartLearningRate" style="height: 400px; width: 100%;"></div>
                    </td>
                </tr>
                <tr>
                    <th style="font-weight: normal; text-align: right;">
                        <button ng-click="move('ModelDetailsArticle', 'AnalyzeParamsArticle')">Back</button>
                    </th>
                    <td></td>
                </tr>
            </table>
        </article>
        <!--
        Analyze Process Select Parameters
        -->
        <article class="container" id="AnalyzeParamsArticle">
            <button type="button" class="btn btn-info" data-toggle="modal" data-target="#infoModal" style="width: 5px; text-align: center; float: right; border-radius: 50px;"><strong>?</strong></button>
            <table class="container">
                <tr>
                    <td class="td_for_select_field">
                        <asp:DropDownList runat="server" CssClass="form-control input_field custom-select" ID="ModelDetails" ng-model="ModelName" Style="width: 144px; float: right; margin-right: 10px;">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <button ng-click="move('AnalyzeParamsArticle', 'ModelDetailsArticle')" style="float: left;">More Details</button>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 15px;">
                        <div class="form-group center_alignment">
                            <div class="input-group mb-3">
                                <div class="custom-file">
                                    <input runat="server" type="file" class="custom-file-input" id="TextFileUpload1">
                                    <label class="custom-file-label" for="TextFileUpload1">Choose File</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <textarea class="form-control" id="Textarea1" rows="30" style="resize: none;" readonly placeholder="Please enter the text here..."></textarea>
                        </div>
                    </td>
                    <td style="padding: 15px;">
                        <div class="form-group center_alignment">
                            <div class="input-group mb-3">
                                <div class="custom-file">
                                    <input runat="server" type="file" class="custom-file-input" id="TextFileUpload2">
                                    <label class="custom-file-label" for="TextFileUpload1">Choose File</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <textarea class="form-control" id="Textarea2" rows="30" style="resize: none;" readonly placeholder="Please enter the text here..."></textarea>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" OnClick="StartAnalyze" Style="float: left;" Text="Start" CssClass="sdc_button"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />
        </article>
        <!--
        Analyze Process Results Parameters
        -->
        <article class="container" id="ResultsArticle" style="display: none; text-align: center; width: 100%">
            <button type="button" class="btn btn-info" data-toggle="modal" data-target="#legendModal" style="width: 5px; text-align: center; float: right; border-radius: 50px;"><strong>?</strong></button>
            <div class="form-group">
                <h2><strong>Before Conversion</strong>:</h2>
                <table>
                    <tr>
                        <td style="height: auto;">
                            <img src="<%=ResultBefore %>" style="width: 90%;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
                <h2><strong>After Conversion</strong>:</h2>
                <table>
                    <tr>
                        <td style="height: auto;">
                            <img src="<%=ResultAfter %>" style="width: 90%;" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Rows="20" ID="AfterFirstText" Style="resize: none;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Rows="20" ID="AfterSecondText" Style="resize: none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </article>

        <!--
        Modal popups
        -->
        <div class="modal" id="myModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">In Progress...</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="height: 100%;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="progress">
                            <asp:Panel runat="server" CssClass="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuemin="0" aria-valuemax="100" Style="width: 90%" ID="ProgressPercentage"></asp:Panel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button ng-click="move('AnalyzeParamsArticle', 'ResultsArticle')" runat="server" id="MoveToResultsButton" data-dismiss="modal">See Results</button>
                        <asp:Button runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="StopProcess"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="infoModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">HOW TO</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="height: 100%;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>
                            In order to analyze documents, a model must be chosen.
                        </p>
                        <p>
                            Please follow the next steps:
                        </p>
                        <p>
                            1. Choose a model from the drop down list.
                        </p>
                        <p>
                            2. Upload two documents that you want to analyze.
                        </p>
                        <p>
                            3. Press the "<strong>Start</strong>" button, and wait for the results display.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="legendModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Results</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="height: 100%;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>
                            On this screen you can observe the results of the analysis of the texts that you inserted.
                        </p>
                        <p>
                            The "<strong>DZV Distance</strong>" show the distances matrix between the texts.
                        </p>
                        <p>
                            If the matrix is symetric, that means that the texts are identical.
                        </p>
                        <br />
                        <p><strong>NOTE!</strong> The translation of the inputed texts is displayed in the bottom of the page.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
