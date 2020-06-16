<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodePage="936" CodeBehind="showEqp.aspx.cs" Inherits="EMS.View.showEqp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 1400px; margin-right: auto; margin-left: auto;">
        <table style="margin-top: 40px; font-family: 华文行楷; font-size: 22px;">
            <tr>
                <td>设备编号：</td>            
                <td><asp:TextBox ID="txtBox1" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;设备名称：</td>            
                <td><asp:TextBox ID="txtBox2" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;购入日期：</td>            
                <td><asp:TextBox ID="txtBox3" runat="server" Height="35px" Width="210px" CssClass="form-control"></asp:TextBox></td>
                
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Back" runat="server" CssClass="btn btn-danger" Text="返回" OnClick="btnBack_Click" /></td>
            </tr>
            <tr>
                <td>存放位置：</td>
                <td><asp:TextBox ID="txtBox4" runat="server" Height="35px" CssClass="form-control"></asp:TextBox></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;设备负责人姓名：</td>            
                <td><asp:TextBox ID="txtBox5" runat="server" Height="35px" CssClass="form-control"></asp:TextBox></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门名称：</td>            
                <td><asp:TextBox ID="txtBox6" runat="server" Height="35px" CssClass="form-control"></asp:TextBox></td>

                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Search" runat="server" CssClass="btn btn-info" Text="查询" OnClick="btnQury_Click" /></td>
            </tr> 
        </table>         
        
    </div>
    <br />

    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" ShowFooter="true" DataKeyNames="EqpId"
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
            <asp:TemplateField HeaderText="EqpId" ItemStyle-Wrap="false" HeaderStyle-Width="140px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EqpId") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtId" Text='<%# Eval("EqpId") %>' runat="server" Width="120px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtIdFooter" runat="server" Width="120px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="设备名称" ItemStyle-Wrap="false" HeaderStyle-Width="180px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EqpName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEqpName" Text='<%# Eval("EqpName") %>' runat="server" Width="160px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEqpNameFooter" runat="server" Width="160px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="设备规格" HeaderStyle-Width="180px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EqpSpecification") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEqpSpe" Text='<%# Eval("EqpSpecification") %>' runat="server" Width="160px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEqpSpeFooter" runat="server" Width="160px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="设备图片" ItemStyle-Wrap="false" HeaderStyle-Width="280px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <div class="text-center">
                        <asp:Image runat="server" ImageAlign="Middle" ImageUrl='<%# Eval("EqpImg") %>' CssClass="img-thumbnail rounded-lg"/>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="text-center">
                        <asp:Image runat="server" ImageAlign="Middle" ID="showEqpImg" ImageUrl='<%# Eval("EqpImg") %>' CssClass="img-thumbnail rounded-lg"/>
                    </div>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="imgEqpAddon01">上传</span>
                        </div>
                        <div class="custom-file">
                            <asp:FileUpload ID="imgEqp" runat="server" Width="260px" CssClass="custom-file-input" aria-describedby="imgEqpAddon01" />
                            <label class="custom-file-label" for="imgEqp">Choose file</label>
                        </div>
                    </div>       
                </EditItemTemplate>
                <FooterTemplate>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="imgEqpFooterAddon02">上传</span>
                        </div>
                        <div class="custom-file">
                            <asp:FileUpload ID="imgEqpFooter" runat="server" Width="260px" CssClass="custom-file-input"  aria-describedby="imgEqpFooterAddon02" />
                            <label class="custom-file-label" for="imgEqpFooter">Choose file</label>
                        </div>
                    </div>                    
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="价格" ItemStyle-Wrap="false" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EqpPrice") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEqpPrice" Text='<%# Eval("EqpPrice") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEqpPriceFooter" runat="server" Width="140px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="购入日期" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("DateOfPurchase") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDateOfPurchase" Text='<%# Eval("DateOfPurchase") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtDateOfPurFooter" runat="server" Width="140px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="存放位置" ItemStyle-Wrap="false" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("Position") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPosition" Text='<%# Eval("Position") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtPositionFooter" runat="server" Width="140px" CssClass="form-control" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="设备负责人" ItemStyle-Wrap="false" HeaderStyle-Width="160px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EmpName") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmpId" Text='<%# Eval("EmpId") %>' runat="server" Width="140px" CssClass="form-control" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtEmpIdFooter" runat="server" Width="140px" CssClass="form-control" placeholder="负责人编号" data-toggle="tooltip" data-placement="top" title="这里填的是员工编号哦~" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="操作" ItemStyle-Wrap="false" HeaderStyle-Width="100px" HeaderStyle-CssClass="align-middle text-center" ItemStyle-CssClass="align-middle text-center" FooterStyle-CssClass="align-middle text-center" Visible="True">
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