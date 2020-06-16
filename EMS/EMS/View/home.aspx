<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="EMS.View.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; height: 800px;" class="center-bg">
        <div style="width: 100%; height: 100%; background: rgba(0,0,0,0.4);position: relative;">
            <h1 style="position: absolute; top:200px; left: 100px;color: white">Welcome to the equipment storage and inquiry system!</h1>
            <h3 style="position: absolute; top:400px; left: 300px;color: white">欢迎</h3>
            <asp:Label ID="Label1" runat="server" 
                style="position: absolute; top:390px; left: 420px;color: red;font-size:40px;font-weight:bold;font-family:华文行楷;"></asp:Label>
            <h3 style="position: absolute; top:400px; left: 600px;color: white">使用设备保管及查询系统！</h3>
            <p style="position: absolute; bottom:100px; right: 100px;color:white;">--by Z·S·J</p>
        </div>
    </div>
</asp:Content>
