﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyRewards.aspx.cs" Inherits="MyRewards" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<asp:Panel ID ="masterPanel" runat ="server">
   
    <div style ="position: relative; top: 60px;">
        <div style = "float: left; width: 15%;">
        <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
            <li><div class="user-view">
                <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                <h5><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></h5>
                <asp:Label ID="lblBalance" runat="server" ></asp:Label>
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
                        <a class="brand-logo nav1 panel">My Rewards</a>
                        <ul id="nav-mobile" class="right hide-on-med-and-down"> 
            
                        </ul>
            
                    </div>
                </nav>
    <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
    </div>
</div>

    
</div>
   




</asp:Panel>


<%--    
<center>
    <h1 class="display-4">My Rewards</h1>
    <div class="jumbotron agent-1" style="width:78%; background-color:lightblue; opacity:0.88; border-radius:25px; padding-top:1px;">
        <div class="container" Style ="width:75%; padding-top: 50px;">
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </div>
</center>--%>

        

    

    
    
     




</asp:Content>
