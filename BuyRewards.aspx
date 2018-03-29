<%@ Page Title="Buy Rewards" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BuyRewards.aspx.cs" Inherits="BuyRewards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID ="masterPanel" runat ="server">

 <div style ="position: relative; top: 60px;">

     <div style = "float: left; width: 15%;">
        <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
            <li><div class="user-view">
                <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                <h5><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></h5>
                
            </div></li>
            <li> <a href="BuyRewards.aspx">Buy Rewards</a></li>
            <li> <a href="MyRewards.aspx">My Rewards</a></li>
            <li><a href="AccountSettingTeamMember.aspx">Account Settings</a></li>
            <li><a href="/userCalendar.aspx">Community Events</a></li>
            <li><a href="/Default.aspx">Logout</a></li>
        </ul>
    </div>

    <div style ="float: right; width: 85%;">
    <div style = "margin-left:auto; margin-right:auto; width:85%;">
    <nav class="feednav" >
        <div class="nav-wrapper">
            <a class="brand-logo nav1 panel">Buy Rewards</a>
            <ul id="nav-mobile" class="right hide-on-med-and-down"> 
            
            </ul>
            
        </div>
    </nav>
    <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
    </div>
</div>

    
</div>

 </asp:Panel>
<asp:Panel ID ="Popup" runat ="server" CssClass ="popupPanel" Visible ="false" Enabled ="false">
    
    <table>
        <tr>
            <td>
                <asp:Label ID ="lblMsg" runat ="server" CssClass ="popupLabels"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID ="btnConfirm" Text ="Confirm" OnClick ="confirm_Click" CssClass ="btn" runat ="server"/>
                <asp:Button ID ="btnCancel" Text = "Cancel" OnClick ="cancel_Click" CssClass ="btn" runat ="server" />
            </td>
        </tr>
    </table>    
    
    

</asp:Panel>

<asp:Panel ID="Popup2" runat="server" CssClass ="popupPanel" Visible ="false" Enabled ="false">
        <table>
            <tr>
                <td>
                    <asp:Label ID ="lblMsg2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="btn" OnClick="ok_Click" />
                </td>
            </tr>
            
        </table>
</asp:Panel>
</asp:Content>

