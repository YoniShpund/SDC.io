<%@ Page Title="Register" Language="C#" MasterPageFile="~/SDC.io.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SDC.io.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMaster" runat="server">
    <br />
    <div class="container center_alignment" style="width: 400px;">
        <h1>Register</h1>
        <div class="form-group has-danger">
            <label for="RegisterEmail">Email Address</label>
            <asp:TextBox runat="server" TextMode="email" CssClass="form-control" ID="RegisterEmail" placeholder="Email"></asp:TextBox>
            <asp:Panel runat="server" CssClass="invalid-feedback">Sorry, that email's invalid or taken.</asp:Panel>
        </div>
        <div class="form-group has-danger">
            <label for="RegisterPassword">Password</label>
            <asp:TextBox runat="server" TextMode="password" CssClass="form-control" ID="RegisterPassword" placeholder="Password"></asp:TextBox>
            <asp:Panel runat="server" CssClass="invalid-feedback">Sorry, the password was empty.</asp:Panel>
        </div>
        <div class="form-group has-danger">
            <label for="RegisterPasswordCheck">Re-enter Password</label>
            <asp:TextBox runat="server" TextMode="password" CssClass="form-control" ID="RegisterPasswordCheck" placeholder="Password"></asp:TextBox>
            <asp:Panel runat="server" CssClass="invalid-feedback">Sorry, the passwords did not match.</asp:Panel>
        </div>
        <asp:Button runat="server" OnClick="RegisterSubmit" CssClass="btn btn-primary" Text="Submit"></asp:Button>
    </div>
</asp:Content>
