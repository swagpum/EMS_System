<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMS.View.Default" CodePage="936" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/signin.css" rel="stylesheet" />
    <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }

      .text-center{
          position: relative;
          background: url("../img/default_bg.jpg") no-repeat center center fixed;
          background-size: cover;
          z-index: 1;
          overflow: hidden;
      }
      .text-center:after{
          content:"";
          width: 100%;
          height: 100%;
          position: absolute;
          left: 0;
          top: 0;
          background: inherit;
          filter: blur(4px);
          z-index: 2;
      }
      .form-signin{
          z-index: 11;
      }
      .form-signin .ctol .form-control{
          padding-left: 36px;
      }

      .ctol{
          position: relative;
      }
      .icon{
          position: absolute;
          left: 6px;
          top: 11px;
          z-index: 100;
      }
    </style>
</head>
<body class="text-center">
    <form class="form-signin" runat="server">
      <h1 class="mb-3 font-weight-normal" style="font-family: 华文彩云;color:white;">登录</h1>
      <div class="ctol">
          <asp:TextBox ID="username" runat="server" class="form-control" placeholder="用户名"></asp:TextBox>
          <img src="../img/person-fill.svg" alt="" width="24" height="24" class="icon"/>
      </div>      
      <div class="ctol">
          <asp:TextBox ID="password" runat="server" class="form-control" placeholder="密码"></asp:TextBox>
          <img src="../img/lock-fill.svg" alt="" width="22" height="22" class="icon"/>
      </div>
      <div class="checkbox mb-3">
        <label class="text-white">
          <asp:CheckBox ID="CheckBox1" runat="server" Text="Remember me" Visible="False" />
        </label>
      </div>
      <asp:Button ID="Button1" runat="server" class="btn btn-lg btn-info btn-block" Text="Sign in" OnClick="Button1_Click" />
      <p class="mt-5 mb-3 text-light">&copy; ZSJ-2020</p>
    </form>
</body>
</html>
