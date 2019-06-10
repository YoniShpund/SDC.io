<%@ Page Title="Analyze" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="Analyze.aspx.cs" Inherits="SDC.io.Analyze" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <table class="container">
                <tr>
                    <td class="td_for_select_field">
                        <select class="form-control input_field " id="ModelDetails" ng-options="ModelInList for ModelInList in ModelNames" ng-model="ModelName" style="width: 144px; float: right; margin-right: 10px;">
                        </select>
                    </td>
                    <td>
                        <button ng-click="move('AnalyzeParamsArticle', 'ModelDetailsArticle')" style="float: left;">More Details</button>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 15px;">
                        <div class="form-group center_alignment" hidden="">
                            <div class="input-group mb-3">
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="TextFileUpload1">
                                    <label class="custom-file-label" for="TextFileUpload1">Choose File</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" CssClass="form-control" ID="Textarea1" Rows="30" TextMode="MultiLine" Style="resize: none;" placeholder="Please enter the text here..."></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding: 15px;">
                        <div class="form-group center_alignment" hidden="">
                            <div class="input-group mb-3">
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="TextFileUpload2">
                                    <label class="custom-file-label" for="TextFileUpload1">Choose File</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" Rows="30" TextMode="MultiLine" Style="resize: none;" placeholder="Please enter the text here..."></asp:TextBox>
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
        <article class="container" id="ResultsArticle" style="display: none; width: 350px;">
            <div class="form-group">
                <label>DZV Result:</label>
                <img src="<%=ResultDzv %>" class="center_alignment" /><br />
            </div>
            <div class="form-group">
                <label>ZV Result:</label>
                <img src="<%=ResultZv %>" class="center_alignment" />
            </div>
        </article>

        <!--
        Modal popup
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
                            <asp:Panel runat="server" CssClass="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuemin="0" aria-valuemax="100" Style="width: 75%" ID="ProgressPercentage"></asp:Panel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button ng-click="move('AnalyzeParamsArticle', 'ResultsArticle')" runat="server" id="MoveToResultsButton" data-dismiss="modal">See Results</button>
                        <asp:Button runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="StopProcess"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
