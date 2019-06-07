<%@ Page Title="Analyze" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="Analyze.aspx.cs" Inherits="WebApplication1.Analyze" %>

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
                    <th class="input_field">{{param.Name}}: </th>
                    <td ng-bind="{{param.ngModel}}" class="input_field"></td>
                </tr>
                <tr>
                    <th style="font-weight: normal;">
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
            <table class="center_alignment" style="width: 320px;">
                <tr>
                    <td class="td_for_select_field">
                        <select class="form-control input_field" id="ModelDetails" ng-options="ModelInList for ModelInList in ModelNames" ng-model="ModelName" style="width: 144px;">
                        </select>
                    </td>
                    <td>
                        <button ng-click="move('AnalyzeParamsArticle', 'ModelDetailsArticle')">More Details</button>
                    </td>
                </tr>
            </table>
            <div class="center_alignment" style="width: 320px;">
                <button ng-click="move('AnalyzeParamsArticle', 'IndexArticle')">Back</button>
                <button>Next</button>
            </div>
        </article>
    </div>
</asp:Content>
