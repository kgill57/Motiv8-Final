<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TeamMemberPage.aspx.cs" Inherits="TeamMemberPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           
    
<div style ="position: relative; top: 60px;">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
          <link rel="stylesheet" href="/resources/demos/style.css">

          <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
          <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
          <script>
            $(function() {
                $("#dialog").dialog();
            } );
          </script>
          <style type ="text/css">
              .lblFix{
                  position:relative;
                  top: -100px;
              }
          </style>
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
            <li><a href="userCalendar.aspx">Community Events</a></li>
            <li><a href="/Default.aspx">Logout</a></li>

    </ul>
    </div>
    
            
    

<div style ="float: right; width: 85%;">
    <div style = "margin-left:auto; margin-right:auto; width:85%;">
    <nav class="feednav" >
        <div class="nav-wrapper">
            <a class="brand-logo nav1 panel">Activity Panel</a>
            <ul id="nav-mobile" class="right hide-on-med-and-down"> 
                <li>
                    <asp:Button ID="btnGiveReward" runat="server" Text="Give Reward" CssClass="btn" OnClick="btnGiveReward_Click"/> 
                </li>
              <%--  <li>
                    <asp:DropDownList ID="giverAndReceiver" runat="server" OnSelectedIndexChanged="giverAndReceiver_SelectedIndexChanged" CssClass ="" AutoPostBack ="true">
                    <asp:ListItem>All Transactions</asp:ListItem>
                    <asp:ListItem>Your Transactions</asp:ListItem>
                    <asp:ListItem>Your Rewards Given</asp:ListItem>
                    <asp:ListItem>Your Rewards Received</asp:ListItem>
            </asp:DropDownList>
                </li>--%>
           
            
            </ul>
         
        </div>
    </nav>
    <asp:Panel ID ="Panel1" runat="server"></asp:Panel>
    </div>
   
</div>
           
    <%--<asp:Panel ID = "Notification" runat="server" BackColor ="White" CssClass ="popupNotification" Visible ="false" Enabled ="false">
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </asp:Panel>--%>
        

    <asp:Panel ID ="Popup" runat ="server" BackColor ="white" CssClass ="popupPanel" Visible ="false" Enabled ="false">
        <h4>Reward</h4>
        <table class="pagination">
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Reciever Name:" CssClass ="lblFix"></asp:Label>
                    
                </td>
                <td>
                    <table class="pagination">
                        <tr>
                            <td>
                                <asp:TextBox ID ="txtSearch" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="btn" runat="server" Text="Search" OnClick ="btnSearch_Click"/>
                            </td>

                        </tr>
                    </table>
                    <asp:ListBox ID="lbResults" runat="server" Height = "200px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="lblRewardValue" runat="server" Text="Reward Value:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRewardValue" runat="server">
                    <asp:ListItem Value="10">$10</asp:ListItem>
                    <asp:ListItem Value="25">$25</asp:ListItem>
                    <asp:ListItem Value="50">$50</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCompanyValue" runat="server" Text="Company Value:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompanyValue" runat="server">
                      <%--<asp:ListItem Value="Health, Well Being and Safety of Team Members">Health, Well Being and Safety of Team Members</asp:ListItem>
                      <asp:ListItem Value="Community Involvement">Community Involvement</asp:ListItem>
                      <asp:ListItem Value="Education">Education</asp:ListItem>
                      <asp:ListItem Value="Customer Service">Customer Service</asp:ListItem>
                      <asp:ListItem Value="Retaining/Attracting New Customers">Retaining/Attracting New Customers</asp:ListItem>
                      <asp:ListItem Value="Process Improvement Initiatives">Process Improvement Initiatives</asp:ListItem>
                      <asp:ListItem Value="Leadership Development">Leadership Development</asp:ListItem>
                      <asp:ListItem Value="Team Building">Team Building</asp:ListItem>--%>
                  </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server">
                        <asp:ListItem text ="Creative" Value ="Creative"></asp:ListItem>
                         <asp:ListItem text="Distinguished" Value ="Distinguished"></asp:ListItem>
                         <asp:ListItem Text ="Exceptional" Value ="Exceptional"></asp:ListItem>
                         <asp:ListItem Text ="Fresh Approach" Value ="Fresh Approach"></asp:ListItem>
                         <asp:ListItem Text ="Superior" Value ="Superior"></asp:ListItem>
                         <asp:ListItem Text ="Ingenious" Value ="Ingenious"></asp:ListItem>
                         <asp:ListItem Text ="Outstanding" Value ="Outstanding"></asp:ListItem>
                         <asp:ListItem Text ="Surprising" Value ="Surprising"></asp:ListItem>
                         <asp:ListItem Text ="Symbolic" Value ="Symbolic"></asp:ListItem>
                         <asp:ListItem Text ="Unexpected" Value ="Unexpected"></asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID = "txtDescription" runat ="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPrivate" runat="server" Text ="Private Transaction"/>
                </td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Text="Send Reward!" onclick="btnSend_Click" CssClass ="btn"/>
                    <asp:Button ID="btnCancel" runat="server" Text ="Close" CssClass="btn" OnClick="btnCancel_Click"/>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblResult" runat="server" Text="" ForeColor ="Red"></asp:Label>
    </asp:Panel>
</div>
    
      

   

</asp:Content>
