<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddRewardProviders.aspx.cs" Inherits="AddRewardProviders" EnableEventValidation="false" %>

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
                <a class="brand-logo nav1 panel">Reward Provider Dashboard</a>
                <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                    <li>
                        <asp:TextBox ID="txtSearch"  placeholder="Search" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="False" />
                    </li>
                </ul>
            </div>
        </nav>

            

        <div style ="background-color: white;">
            <asp:Label ID="lblResult" runat="server" Visibile="false" style="margin-left: 500px; font-family:sans-serif; font:bold"></asp:Label>
            

                <asp:Panel ID="payPanel" runat="server" CssClass="popupPanel" Visible="false" Width="10%" style="margin-left:400px;">

                        <asp:Button ID="btnClosePay" runat="server" Text="Close" CssClass="btn" OnClick="btnClosePay_Click" />
                    
                    
                </asp:Panel>


            <asp:GridView ID="grdProviders" ValidationGroup="validNewProvider" AutoGenerateColumns="False" runat="server"  class="striped" style="padding:40px;" 
                          DataKeyNames="ProviderID" ShowHeaderWhenEmpty="True" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Provider ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderID" runat="server" Text='<%# Eval("ProviderID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provider Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provider Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderEmail" runat="server" Text='<%# Eval("ProviderEmail") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("Approved") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay Provider">
                                <ItemTemplate>
                                    <asp:Button ID="btnPay" runat="server" OnClick="btnPay_Click" Text="Pay Provider" CssClass="btn" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lvlgvNoProvider" runat="server" Text="No Records Found"></asp:Label>
                        </EmptyDataTemplate>
                </asp:GridView>             
            </div>          
        </div>
    </div>
</div>

</asp:Content>

