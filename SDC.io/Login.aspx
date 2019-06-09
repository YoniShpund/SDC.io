<%@ Page Title="Login" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SDC.io.Login" %>

<%@ MasterType VirtualPath="~/SDC.io.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMaster" runat="server">
    <br />
    <div class="container center_alignment" style="width: 400px;">
        <h1>Login</h1>
        <div class="form-group">
            <label for="LoginMail">Email Address</label>
            <asp:TextBox runat="server" ID="LoginMail" TextMode="Email" CssClass="form-control" placeholder="Email"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="LoginPassword">Password</label>
            <asp:TextBox runat="server" ID="LoginPassword" TextMode="Password" CssClass="form-control" placeholder="Password"></asp:TextBox>
        </div>
        <asp:Button runat="server" OnClick="LoginSubmit" CssClass="btn btn-primary" Text="Submit"></asp:Button>
        <asp:Button runat="server" OnClick="Register" CssClass="btn btn-primary" Text="Register"></asp:Button>
    </div>
</asp:Content>
