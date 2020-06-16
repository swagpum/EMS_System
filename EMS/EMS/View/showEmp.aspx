<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="showEmp.aspx.cs" Inherits="EMS.View.showEmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <div style="width: 1400px; margin-right: auto; margin-left: auto;">
            <table style="margin-top: 40px; font-family: 华文行楷; font-size: 22px;">
                <tr>
                    <td>员工编号：</td>            
                    <td><asp:TextBox ID="txtBox1" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工姓名：</td>            
                    <td><asp:TextBox ID="txtBox2" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门名称：</td>            
                    <td><asp:TextBox ID="txtBox3" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Back" runat="server" CssClass="btn btn-danger" Text="返回" OnClick="btnBack_Click" /></td>
                </tr>
                <tr>
                    <td>联络电话：</td>
                    <td><asp:TextBox ID="txtBox4" runat="server" Height="35px" CssClass="form-control"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>            
                    <td><asp:TextBox ID="txtBox5" runat="server" Height="35px" CssClass="form-control" Visible="False"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;是否系统管理员：</td>            
                    <td><asp:TextBox ID="txtBox6" runat="server" Height="35px" CssClass="form-control" placeholder="填0或1"></asp:TextBox></td>

                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Search" runat="server" CssClass="btn btn-info" Text="查询" OnClick="btnQury_Click" /></td>
                </tr> 
            </table>               
        </div>
    </asp:Panel>
    

    <br /><br />

    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" ShowFooter="true" DataKeyNames="EmpId"
        ShowHeaderWhenEmpty="true"
        CssClass="table"
        OnRowCommand="gv1_RowCommand"
        OnRowEditing="gv1_RowEditing"
        OnRowCancelingEdit="gv1_RowCancelingEdit"
        OnRowUpdating="gv1_RowUpdating"
        OnRowDeleting="gv1_RowDeleting"
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Height="274px" HorizontalAlign="Center" Width="100px" RowStyle-BorderWidth="1px">
        
        <%-- theme propertied --%>
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />

        <Columns>
            <asp:TemplateField HeaderText="EmpId" ItemStyle-Wrap="false" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EmpId") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtId" Text='<%# Eval("EmpId") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtIdFooter" runat="server" Width="140px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工姓名" ItemStyle-Wrap="false" HeaderStyle-Width="240px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EmpName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmpName" Text='<%# Eval("EmpName") %>' runat="server" Width="220px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEmpNameFooter" runat="server" Width="220px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="密码" HeaderStyle-Width="240px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("Password") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPwd" Text='<%# Eval("Password") %>' runat="server" Width="220px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPwdFooter" runat="server" Width="220px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="联络电话" ItemStyle-Wrap="false" HeaderStyle-Width="280px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EmpPhone") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmpPhone" Text='<%# Eval("EmpPhone") %>' runat="server" Width="260px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEmpPhoneFooter" runat="server" Width="260px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门名称" ItemStyle-Wrap="false" HeaderStyle-Width="280px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("DptName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDptId" Text='<%# Eval("DptId") %>' runat="server" Width="260px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtDptIdFooter" runat="server" Width="260px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否为管理人" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("IsAdmin") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIsAdmin" Text='<%# Eval("IsAdmin") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtIsAdminFooter" runat="server" Width="140px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false" HeaderStyle-Width="120px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center" FooterStyle-CssClass="align-middle text-center" Visible="True">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/img/编辑.png" CommandName="edit" ToolTip="edit" Width="20px" Height="20px" runat="server" />
                    <asp:ImageButton ImageUrl="~/img/删除.png" CommandName="delete" ToolTip="delete" Width="20px" Height="20px" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/img/icon_添加.png" CommandName="update" ToolTip="update" Width="20px" Height="20px" runat="server" />
                    <asp:ImageButton ImageUrl="~/img/关闭.png" CommandName="cancel" ToolTip="cancel" Width="20px" Height="20px" runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ImageUrl="~/img/icon_添加.png" CommandName="add" ToolTip="add" Width="20px" Height="20px" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
    <br />
    <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />

</asp:Content>
