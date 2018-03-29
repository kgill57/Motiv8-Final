<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRewards.aspx.cs" Inherits="ViewRewards" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID ="masterPanel" runat ="server">
<div style ="position: relative; top: 60px;">

    <%-- Sidebar --%>
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
                            <li><a href="PendingApplications.aspx">Pending Applications</a></li>
                            <li><a href="AnalyticsPage.aspx">View Analytics</a></li>
                            <li><a href="/adminCalendar.aspx">Community Events</a></li>
                            <li><a href="/Default.aspx">Logout</a></li>
                    </ul>
                </div>

    <%-- Content --%>
    <div style ="float: right; width: 85%;">
        <div style = "margin-left:auto; margin-right:auto; width:85%;">
        <nav class="feednav" >
            <div class="nav-wrapper">
                <a class="brand-logo nav1 panel">Rewards</a>
                <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                    
    
                    <asp:Label ID = "lblResult" runat ="server"/>
                    <%-- Options Here --%>
                </ul>
            </div>
        </nav>
            <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
       
        </div>
    </div>
</div>

<asp:Panel ID ="Popup" runat ="server" BackColor ="white" CssClass ="popupPanel" Visible ="false" Enabled ="false">
    <table>
        <tr>
            <td>
                <asp:Label ID ="lblMsg" runat ="server" CssClass ="popupLabels"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID ="btnDelete" Text ="Delete" OnClick ="delete_Click" CssClass ="btn" runat ="server" ForeColor ="Red"/>
            </td>
            <td>
                <asp:Button ID ="btnCancel" Text = "Cancel" OnClick ="cancel_Click" CssClass ="btn" runat ="server" />
            </td>
        </tr>
    </table>
    
</asp:Panel>
</asp:Panel>
    



    
</asp:Content>
