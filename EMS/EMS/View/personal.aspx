<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="personal.aspx.cs" Inherits="EMS.View.personal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 1000px; height: 700px; margin-top:50px;margin-left:350px; border:1px solid black; border-radius: 20px;" class="center-bg2;">
        <h1 style="font-family:华文行楷;text-align:center;padding-top: 40px;">个人中心</h1>
        <table style="font-family: 华文行楷; font-size: 22px; margin:40px auto">
            <tr>
                <td style="font-size:26px;">编号：</td>            
                <td><asp:TextBox ID="TextBox1" runat="server" Height="40px" Width="400px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="font-size:26px;">姓名：</td>            
                <td><asp:TextBox ID="TextBox2" runat="server" Height="40px" Width="400px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="font-size:26px;">密码：</td>            
                <td><asp:TextBox ID="TextBox3" runat="server" Height="40px" Width="400px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="font-size:26px;">电话：</td>            
                <td><asp:TextBox ID="TextBox4" runat="server" Height="40px" Width="400px" CssClass="form-control"></asp:TextBox></td>
            </tr> 
            <tr>
                <td style="font-size:26px;">部门：</td>            
                <td><asp:TextBox ID="TextBox5" runat="server" Height="40px" Width="400px" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <asp:TextBox ID="TextBox6" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="TextBox7" runat="server" Visible="False"></asp:TextBox>
        </table>  

        <div class="card mb-3" style="max-width: 800px;margin-left:100px;">
            <div class="row no-gutters">
                <div class="col-md-4">
                    <img src="../img/personal-1.png" class="card-img"/>
                </div>
                <div class="col-md-8">
                    <div class="card-body">
                        <h5 class="card-title">温馨提示</h5>
                        <p class="card-text">只能修改姓名、密码和电话哦~</p>
                        <p class="card-text">编号和部门只有系统管理员才可以修改~</p>
                        <p class="card-text" style="margin-left:160px;">
                            <asp:Button ID="Button1" runat="server" Text="确认修改" CssClass="btn btn-info" OnClick="Button1_Click" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>
