<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountSettingTeamMember.aspx.cs" Inherits="AccountSettingTeamMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <a class="brand-logo nav1 panel">Account Settings</a>
                        <ul id="nav-mobile" class="right hide-on-med-and-down"> 
            
                        </ul>
            
                    </div>
                </nav>
                <asp:Panel ID ="Panel1" runat="server" BackColor ="white">
                    <table >
                    <tr>
                        <td style="width: 160px">Upload Profile Picture:</td>
                        <td>   
                            <asp:FileUpload ID ="UploadPicture" runat ="server" />
                            <asp:Button ID ="btnUpload" CssClass="btn btn-primary" runat ="server" Text ="Upload" OnClick="btnUpload_Click" />
                        </td>
                    </tr>
                    </table>
                    <asp:Panel runat ="server" ID ="changePassPanel">

                    
                    <table >
                    <tr>
                        <td style="width: 160px">Change Password:</td>
                    
                    </tr>
                    <tr>
                        <td>   
                            <asp:TextBox ID="txtCurrentPass" placeholder="Current Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNewPass1" placeholder="New Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNewPass2" placeholder="Confirm New Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnChangePass" CssClass="btn" runat="server" OnClick="btnChangePass_Click" Text="Confirm Password Change"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    </asp:Panel>
                </asp:Panel>
    </div>
</div>

    
</div>
<%--<center>
    <h1 class="display-4">Account Settings</h1>
    <div class="jumbotron agent-1" style="width:78%; background-color:lightblue; opacity:0.88; border-radius:25px; padding-top:1px;">
        <br />
        <br />
        <div>
            <div class="form-group">
                <table >
                    <tr>
                        <td style="width: 160px">Upload Profile Picture:</td>
                        <td>   
                            <asp:FileUpload ID ="UploadPicture" runat ="server" />
                            <asp:Button ID ="btnUpload" CssClass="btn btn-primary" runat ="server" Text ="Upload" OnClick="btnUpload_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtCurrentPass" CssClass="form-control" placeholder="Current Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
            </div>
            <div class="form-group">
                 <asp:TextBox ID="txtNewPass1" CssClass="form-control" placeholder="New Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtNewPass2" CssClass="form-control" placeholder="Confirm New Password" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnChangePass" CssClass="btn btn-primary" runat="server" OnClick="btnChangePass_Click" Text="Confirm Password Change" Width="200px" />
            </div>
        </div>

        <br />

        <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>

    </div>
</center>         --%>
           
</asp:Content>
