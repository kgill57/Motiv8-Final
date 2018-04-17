<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rpApplication.aspx.cs" Inherits="rpApplication" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
                        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style ="position: relative; top: 60px;">
        <div style ="float: right; width: 85%;">
            <div style = "margin-left:auto; margin-right:auto; width:85%;">
                <nav class="feednav" >
                    <div class="nav-wrapper">
                        <a class="brand-logo nav1 panel">Apply to Join Motiv8</a>
                        <ul id="nav-mobile" class="right hide-on-med-and-down">
                            <li style="margin-right:10px">
                                <asp:Button ID="btnAdminApp" CssClass="btn" runat="server" Text="Apply to be an Admin" OnClick="btnAdminApp_Click"/>
                            </li>
                            <li style="margin-right:10px">
                                <asp:Button ID="btnProviderApp" CssClass="btn" runat="server" Text="Apply to be a Reward Provider" OnClick="btnProviderApp_Click" />
                            </li>
                            <li>
                                <asp:Button ID="btnLogout" CssClass="btn" runat="server" Text="Logout" OnClick="btnLogout_Click" />
                            </li>
                        </ul>
                    </div>
                </nav>
            </div>
        </div>
            <asp:Panel ID="ProviderPanel" runat="server" BackColor ="white" CssClass ="popupPanel" Visible ="false" Enabled ="false">
                
                    <table class="pagination">
                        <tr>
                            <td>
                                <asp:Label ID="lblProvFName" runat="server" Text="First Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProvFName" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblProvLName" runat="server" Text="Last Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProvLName" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProvNickName" runat="server" Text="Nick Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProvNickName" runat="server" CssClass="validate"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblProvEmail" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProvEmail" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="pagination">
                            <tr>
                                <td>
                                    <asp:Label ID="lblProvCompany" runat="server" Text="Company:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProvCompany" runat="server" CssClass="validate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProvDesc" runat="server" Text="What Company Would you like to partner with?"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpEmployers" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnApply" runat="server" Text="Submit" CssClass = "btn" OnClick="btnApply_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelApp" runat="server" Text="Close" CssClass ="btn" OnClick="btnCancelApp_Click"/>
                                </td>
                            </tr>
                    </table>
                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                
            </asp:Panel>
            <div>
                <asp:Panel ID ="EmployerPanel" runat ="server" BackColor ="white" CssClass ="popupPanel" Visible ="false" Enabled ="false">
                    <table class="pagination">
                        <tr>
                            <td>
                                <asp:Label ID="lblFName" runat="server" Text="First Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFName" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblLName" runat="server" Text="Last Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLName" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNickName" runat="server" Text="Nick Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNickName" runat="server" CssClass="validate"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblNewEmail" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewEmail" runat="server" CssClass ="validate"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="pagination">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewCompanyName" runat="server" CssClass="validate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSubmitUser" runat="server" Text="Submit" CssClass = "btn" OnClick="btnSubmitUser_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass ="btn" OnClick="btnCancel_Click"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                    </table>

                </asp:Panel>
            </div>
        </div>
        
</asp:Content>
