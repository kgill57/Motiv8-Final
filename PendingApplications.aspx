<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PendingApplications.aspx.cs" Inherits="PendingApplications" %>

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
                    <a class="brand-logo nav1 panel">Pending Applications</a>
                    <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                        <li>
                        </li>
                    </ul>
                </div>
            </nav>

            <div style ="background-color: white; padding: 20px;">

                <asp:GridView ID="grdProviders" ValidationGroup="validNewProvider" runat="server"  class="striped" style="padding:40px;"
                           DataKeyNames="ProviderID" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AutoGenerateEditButton="true" OnRowCancelingEdit="grdProviders_RowCancelingEdit" OnRowEditing="grdProviders_RowEditing" OnRowUpdating="grdProviders_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Provider Name">
                            <ItemTemplate>
                                <asp:Label ID="lblProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Provider Email">
                            <ItemTemplate>
                                <asp:Label ID="lblProviderEmail" runat="server" Text='<%# Eval("ProviderEmail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved">
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpApproved" runat="server" SelectedValue='<%# Bind("Approved") %>'>
                                    <asp:ListItem Value="1">Not Approved</asp:ListItem>
                                    <asp:ListItem Value="0">Approved</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("Approved") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                        
                </asp:GridView>

            </div>


        </div>
    </div>

</div>

</asp:Content>
