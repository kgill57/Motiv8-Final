<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AnalyticsPage.aspx.cs" Inherits="AnalyticsPage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
                        
    

</asp:Content>

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
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                            <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                                <li>
                                    <asp:DropDownList ID="drpReportOptions" runat="server" CssClass="select-dropdown">
                                        <asp:ListItem>Account Balance Recap</asp:ListItem>
                                        <asp:ListItem>Transaction History</asp:ListItem>
                                        <asp:ListItem>Value Breakdown</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:Button ID="btnDownload" runat="server" Text="Send Reports" CssClass="btn" OnClick="btnDownload_Click" />
                                </li>
                            </ul>
                        </div>
                    </nav>

                        <div class="container featured">
                            <asp:DropDownList ID="tableauDropDown" runat="server" OnSelectedIndexChanged="tableauDropDown_SelectedIndexChanged" AutoPostBack="true" style="width: 20%; margin-left:250px; margin-top: 10px;">
                                <asp:ListItem>Account Balance</asp:ListItem>
                                <asp:ListItem>User Breakdown</asp:ListItem>
                                <asp:ListItem>Value Breakdown</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Panel ID="panel1" runat="server" >
                                <div class='tableauPlaceholder' id='viz1523113703377' style="position:relative; right:100px; top: 30px;"><noscript><a href='#'><img alt='Account Balance Dashboard ' src='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ac&#47;AccountBalance&#47;AccountBalanceDashboard&#47;1_rss.png' style='border: none' /></a></noscript><object class='tableauViz'  style='display:none;'><param name='host_url' value='https%3A%2F%2Fpublic.tableau.com%2F' /> <param name='embed_code_version' value='3' /> <param name='site_root' value='' /><param name='name' value='AccountBalance&#47;AccountBalanceDashboard' /><param name='tabs' value='no' /><param name='toolbar' value='yes' /><param name='static_image' value='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ac&#47;AccountBalance&#47;AccountBalanceDashboard&#47;1.png' /> <param name='animate_transition' value='yes' /><param name='display_static_image' value='yes' /><param name='display_spinner' value='yes' /><param name='display_overlay' value='yes' /><param name='display_count' value='yes' /></object></div>                <script type='text/javascript'>                    var divElement = document.getElementById('viz1523113703377'); var vizElement = divElement.getElementsByTagName('object')[0]; vizElement.style.width = '1000px'; vizElement.style.height = '827px'; var scriptElement = document.createElement('script'); scriptElement.src = 'https://public.tableau.com/javascripts/api/viz_v1.js'; vizElement.parentNode.insertBefore(scriptElement, vizElement);                </script>
                            </asp:Panel>
                            <asp:Panel ID="panel2" runat="server" >
                                <div class='tableauPlaceholder' id='viz1523113720596' style="position:relative; right:100px; top: 30px;"><noscript><a href='#'><img alt='User Rewards Sent&#47;Received ' src='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ad&#47;AdminAnalytics&#47;UserRewardsSentReceived&#47;1_rss.png' style='border: none' /></a></noscript><object class='tableauViz'  style='display:none;'><param name='host_url' value='https%3A%2F%2Fpublic.tableau.com%2F' /> <param name='embed_code_version' value='3' /> <param name='site_root' value='' /><param name='name' value='AdminAnalytics&#47;UserRewardsSentReceived' /><param name='tabs' value='no' /><param name='toolbar' value='yes' /><param name='static_image' value='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ad&#47;AdminAnalytics&#47;UserRewardsSentReceived&#47;1.png' /> <param name='animate_transition' value='yes' /><param name='display_static_image' value='yes' /><param name='display_spinner' value='yes' /><param name='display_overlay' value='yes' /><param name='display_count' value='yes' /><param name='filter' value='publish=yes' /></object></div>                <script type='text/javascript'>                    var divElement = document.getElementById('viz1523113720596'); var vizElement = divElement.getElementsByTagName('object')[0]; vizElement.style.width = '1000px'; vizElement.style.height = '827px'; var scriptElement = document.createElement('script'); scriptElement.src = 'https://public.tableau.com/javascripts/api/viz_v1.js'; vizElement.parentNode.insertBefore(scriptElement, vizElement);                </script>
                            </asp:Panel>
                            <asp:Panel ID="panel3" runat="server" >
                                <div class='tableauPlaceholder' id='viz1523113732457' style="position:relative; right:100px; top: 30px;"><noscript><a href='#'><img alt='Value Breakdown Dashboard ' src='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ad&#47;AdminAnalytics&#47;ValueBreakdownDashboard&#47;1_rss.png' style='border: none' /></a></noscript><object class='tableauViz'  style='display:none;'><param name='host_url' value='https%3A%2F%2Fpublic.tableau.com%2F' /> <param name='embed_code_version' value='3' /> <param name='site_root' value='' /><param name='name' value='AdminAnalytics&#47;ValueBreakdownDashboard' /><param name='tabs' value='no' /><param name='toolbar' value='yes' /><param name='static_image' value='https:&#47;&#47;public.tableau.com&#47;static&#47;images&#47;Ad&#47;AdminAnalytics&#47;ValueBreakdownDashboard&#47;1.png' /> <param name='animate_transition' value='yes' /><param name='display_static_image' value='yes' /><param name='display_spinner' value='yes' /><param name='display_overlay' value='yes' /><param name='display_count' value='yes' /></object></div>                <script type='text/javascript'>                    var divElement = document.getElementById('viz1523113732457'); var vizElement = divElement.getElementsByTagName('object')[0]; vizElement.style.width = '1000px'; vizElement.style.height = '827px'; var scriptElement = document.createElement('script'); scriptElement.src = 'https://public.tableau.com/javascripts/api/viz_v1.js'; vizElement.parentNode.insertBefore(scriptElement, vizElement);                </script>
                            </asp:Panel>
                            
                        </div> 
                    </div>
                  
                  
                </div>
                
                
          
        </div>
        
            
    

</asp:Content>