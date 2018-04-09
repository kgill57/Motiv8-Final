<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyValues.aspx.cs" Inherits="CompanyValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div style ="position: relative; top: 60px;">
        <div style = "float: left; width: 15%;">
            <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
                            <li><div class="user-view">
                                <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                                <asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label>
                                <asp:Label ID="lblBalance" runat="server" CssClass ="user1"></asp:Label>
                            </div></li>
                            <li><a href ="/UserOptions.aspx">User Options</a></li>
                            <li><a href="/ViewRewards.aspx">View Rewards</a></li>
                            <li><a href ="/AddRewardProviders.aspx">View Reward Providers</a></li>
                            <li><a href="/CompanyValues.aspx">View Company Values</a></li>
                            <li><a href="PendingApplications.aspx">Pending Applications</a></li>
                            <li><a href="AnalyticsPage.aspx">View Analytics</a></li>
                            <li><a href="/adminCalendar.aspx">Community Events</a></li>
                            <li><a href="/Default.aspx">Logout</a></li>
                    </ul>
        </div>
        <div style ="float: right; width: 85%;">
            <div style = "margin-left:auto; margin-right:auto; width:85%;">
                <nav class="feednav" >
                    <div class="nav-wrapper">
                        <a class="brand-logo nav1 panel">Company Value Dashboard</a>
                        <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                            <asp:Button ID = "btnInsertValue" CssClass = "btn" Text ="Add Value" runat = "server" OnClick="btnInsertValue_Click"/>
                        </ul>
                    </div>
                </nav>
                <div style ="background-color: white;">
                    <asp:GridView ID="valueGrid" runat="server" AutoGenerateEditButton="true" class="striped" AutoGenerateColumns="False" OnRowCancelingEdit="valueGrid_RowCancelingEdit" OnRowEditing="valueGrid_RowEditing" OnRowUpdating="valueGrid_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="ValueID">
                                <ItemTemplate>
                                    <asp:Label ID="lblValueID" runat="server" Text='<%# Eval("ValueID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValueName" runat="server" Text='<%# Eval("ValueName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValueName" runat="server" Text='<%# Eval("ValueName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Updated">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLastUpdated" runat="server" Text='<%# Eval("LastUpdated") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastUpdated" runat="server" Text='<%# Eval("LastUpdated") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Updated By">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLastUpdatedBy" runat="server" Text='<%# Eval("LastUpdatedBy") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastUpdatedBy" runat="server" Text='<%# Eval("LastUpdatedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>



        </div>


    </div>

    <asp:Panel ID="popup" runat="server" BackColor ="white" CssClass ="popupPanel" Visible ="false" Enabled ="false">
        <table class="pagination">
            <tr>
                <td>
                    <asp:Label ID="lblNewValue" runat="server" Text="Value Name:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNewValueName" runat="server" CssClass="input-field"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddValue" runat="server" Text="Add Value" OnClick="btnAddValue_Click" class="btn"/>
                </td>
                <td>
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

