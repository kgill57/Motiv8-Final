<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rpHome.aspx.cs" Inherits="rpHome" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
                        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID ="masterPanel" runat ="server">
    <div style ="position: relative; top: 60px;">

        <div style = "float: left; width: 15%;">
            <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
                <li><div class="user-view">
                    <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                    <h5><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></h5>
                    <asp:Label ID="lblBalance" runat="server" ></asp:Label>
                </div></li>

                <li><a href="rpHome.aspx">Rewards</a></li>
                <li><a href="Calendar.aspx">Calendar</a></li>               
                <li><a href="rpSettings.aspx">Settings</a></li>
                <li><a href="Default.aspx">Logout</a></li>

            </ul>
        </div>

    

    
    



       
            <div style ="float: right; width: 85%;">
        <div style = "margin-left:auto; margin-right:auto; width:85%;">
        
            <nav class="feednav">
            <div class="nav-wrapper">
                <a href="" class="brand-logo nav1 panel">Your Current Rewards</a>
                <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                    <li><asp:Button runat="server" ID="btnAddReward" Text="Add Reward" CssClass="btn" OnClick="btnAddReward_Click"/></li>
                </ul>
            </div>
            </nav>
            <br />
            <br />
                    
               
            <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
       
        </div>
            </div>
    </div>
    <asp:Panel ID ="addReward" runat ="server" CssClass ="popupPanel" Visible ="false" Enabled ="false">
        <asp:TextBox runat="server" ID="txtRewardName" placeholder="Reward Name"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtRewardQuantity" placeholder="Reward Quantity" TextMode="Number"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtRewardAmount" placeholder="Reward Amount" TextMode="Number"></asp:TextBox>
            <asp:FileUpload ID ="UploadPicture" runat ="server" />
            <asp:Button ID="btnInsert" CssClass ="btn" runat="server" Text="Add Reward" OnClick="btnInsert_Click" />
            <asp:Button ID="btnCancel" CssClass ="btn" runat="server" Text="Cancel" OnClick ="btnCancel_Click"/>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </asp:Panel>
        
  
        </asp:Panel>
</asp:Content>
