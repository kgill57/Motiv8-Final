<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rpHome.aspx.cs" Inherits="rpHome" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
                        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style ="position: relative; top: 60px;">

        <div style = "float: left; width: 15%;">
            <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
                <li><div class="user-view">
                    <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                    <h5><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></h5>
                    <asp:Label ID="lblBalance" runat="server" ></asp:Label>
                </div></li>
                <li><a href="Calendar.aspx">Calendar</a></li>
                <li><a href="Default.aspx">Logout</a></li>
                <li><a href="rpSettings.aspx">Settings</a></li>
            </ul>
        </div>

    </div>

    <asp:Panel ID ="masterPanel" runat ="server">
    



       
            <div style ="float: right; width: 85%;">
        <div style = "margin-left:auto; margin-right:auto; width:85%;">
        <div class="container rewards">
            <nav class="feednav">
            <div class="nav-wrapper">
                <a href="" class="brand-logo nav1 panel">Your Current Rewards</a>
                <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                    <li><asp:Button runat="server" ID="btnAddReward" Text="Add Reward" CssClass="nav1 modal-trigger" data-target="modal2" OnClick="btnAddReward_Click"/></li>
                </ul>
            </div>
            </nav>
            <br />
            <br />
            <div runat="server" id="addReward" visible="false" style="width: 40%; margin-left:auto; margin-right:auto; background-color:white; padding:20px;">
                <asp:Panel runat="server" Visible="true" ID="RewardPanel">
                <asp:TextBox runat="server" ID="txtRewardName" placeholder="Reward Name"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRewardQuantity" placeholder="Reward Quantity"></asp:TextBox>
                <asp:TextBox runat="server" ID="txtRewardAmount" placeholder="Reward Amount"></asp:TextBox>
                <asp:Button runat="server" ID="btnUploadPic" Text="Upload Photo" />
                <asp:Button ID="btnInsert" runat="server" Text="Add Reward" />
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </asp:Panel>
                    
                </div>
            <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
       
        </div>
    </div>
                </div>
        
  
        </asp:Panel>
</asp:Content>
