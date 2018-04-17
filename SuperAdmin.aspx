<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SuperAdmin.aspx.cs" Inherits="SuperAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style ="position: relative; top: 60px;">
    <div style ="float: right; width: 85%;">
        <div style = "margin-left:auto; margin-right:auto; width:85%;">
        <nav class="feednav" >
            <div class="nav-wrapper">
                <a class="brand-logo nav1 panel">User Management Dashboard</a>
                <ul id="nav-mobile" class="right hide-on-med-and-down">
                    <li>
                        <asp:Button ID="btnLogout" CssClass="btn" runat="server" Text="Logout" OnClick="btnLogout_Click" />
                    </li>
                    <li><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></li>
                </ul>
            </div>
        </nav>
        <div style ="background-color: white;">
            <asp:GridView ID="grdUsers" class="striped" ValidationGroup="validNewEmp" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowCancelingEdit="grdUsers_RowCancelingEdit" OnRowDeleting="grdUsers_RowDeleting" OnRowEditing="grdUsers_RowEditing" OnRowUpdating="grdUsers_RowUpdating">
                <Columns>
                    <asp:CommandField ShowEditButton="true" CausesValidation="true" ValidationGroup="validNewEmp"/>
                    <asp:TemplateField HeaderText="First Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvFName" runat="server" MaxLength="30" Text='<%# Eval("FName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqgvFName" ControlToValidate="txtgvFName" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewEmp"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFName" runat="server" Text='<%# Eval("FName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvLName" runat="server" MaxLength="30" Text='<%# Eval("LName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqgvLName" ControlToValidate="txtgvLName" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewEmp"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLName" runat="server" Text='<%# Eval("LName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvEmail" runat="server" MaxLength="50" Text='<%# Eval("Email") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqgvEmail" ControlToValidate="txtgvEmail" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewEmp"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nick Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvNickName" runat="server" MaxLength="50" Text='<%# Eval("NickName") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqgvNickName" ControlToValidate="txtgvNickName" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewEmp"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("NickName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drpStatus" runat="server" SelectedValue='<%# Bind("EmployedStatus") %>'>
                                <asp:ListItem value="0">Not Approved</asp:ListItem>
                                <asp:ListItem Value="1">Approved</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("EmployedStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="lblEmptly" runat="server" Text="No Records Available"></asp:Label>
                </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>


</div>


</asp:Content>
