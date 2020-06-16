<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="showDept.aspx.cs" Inherits="EMS.View.showDept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <div style="width: 1400px; margin-right: auto; margin-left: auto;">
            <table style="margin-top: 40px; font-family: 华文行楷; font-size: 22px;">
                <tr>
                    <td>部门编号：</td>            
                    <td><asp:TextBox ID="txtBox1" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门名称：</td>            
                    <td><asp:TextBox ID="txtBox2" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门主管：</td>            
                    <td><asp:TextBox ID="txtBox3" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Back" runat="server" CssClass="btn btn-danger" Text="返回" OnClick="btnBack_Click" /></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Search" runat="server" CssClass="btn btn-info" Text="查询" OnClick="btnQury_Click" /></td>
                </tr>          
            </table>                
        </div>
    </asp:Panel>    

    <br /><br />

    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False"  ShowFooter="True" DataKeyNames="DptId"
        ShowHeaderWhenEmpty="true"
        CssClass="table"
        OnRowCommand="gv1_RowCommand"
        OnRowEditing="gv1_RowEditing"
        OnRowCancelingEdit="gv1_RowCancelingEdit"
        OnRowUpdating="gv1_RowUpdating"
        OnRowDeleting="gv1_RowDeleting"
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="True" Height="274px" HorizontalAlign="Center" Width="100px" RowStyle-BorderWidth="1px">
        
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
            <asp:TemplateField HeaderText="DptId" ItemStyle-Wrap="false" HeaderStyle-Width="300px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("DptId") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtId" Text='<%# Eval("DptId") %>' runat="server" Width="280px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtIdFooter" runat="server" Width="280px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门名称" ItemStyle-Wrap="false" HeaderStyle-Width="380px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("DptName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEqpName" Text='<%# Eval("DptName") %>' runat="server" Width="360px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEqpNameFooter" runat="server" Width="360px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门主管" HeaderStyle-Width="380px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EmpName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDptAdmin" Text='<%# Eval("DptAdmin") %>' runat="server" Width="360px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtDptAdminFooter" runat="server" Width="360px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="操   作" ItemStyle-Wrap="false" HeaderStyle-Width="250px" HeaderStyle-CssClass="align-middle text-center" 
                ItemStyle-CssClass="align-middle text-center" FooterStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/img/编辑.png" CommandName="edit" ToolTip="edit" Width="26px" Height="26px" runat="server" />
                    <asp:ImageButton ImageUrl="~/img/删除.png" CommandName="delete" ToolTip="delete" Width="26px" Height="26px" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/img/icon_添加.png" CommandName="update" ToolTip="update" Width="26px" Height="26px" runat="server" />
                    <asp:ImageButton ImageUrl="~/img/关闭.png" CommandName="cancel" ToolTip="cancel" Width="26px" Height="26px" runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ImageUrl="~/img/icon_添加.png" CommandName="add" ToolTip="add" Width="26px" Height="26px" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
    <br />
    <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />


</asp:Content>
