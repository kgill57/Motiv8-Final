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

                <asp:Panel ID="appPanel" CssClass="popupPanel"  runat="server" Visible="false" >
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input-field"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text="Company Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text="Why do you want to be a part of Motiv8?"></asp:Label>
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="113px" Width="315px" CssClass="input-field"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnExitApp" runat="server" Text="Close" CssClass="btn" OnClick="btnExitApp_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn" OnClick="btnApprove_Click" />
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </asp:Panel>

                <asp:GridView ID="grdProviders" ValidationGroup="validNewProvider" runat="server"  class="striped" style="padding:40px;"
                           DataKeyNames="ProviderID" ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Provider ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderID" runat="server" Text='<%# Eval("ProviderID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provider Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqgvProviderName" ControlToValidate="txtgvProviderName" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewProvider"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Provider Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvProviderEmail" runat="server" Text='<%# Eval("ProviderEmail") %>' TextMode="SingleLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqgvProviderEmail" ControlToValidate="txtgvProviderEmail" Text="(Required)" Display="Dynamic" Runat="server" Font-Bold="True" ForeColor="Red" ValidationGroup="validNewProvider"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProviderEmail" runat="server" Text='<%# Eval("ProviderEmail") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View Application">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewApp" runat="server" Text="View Application" OnClick="btnViewApp_Click" CommandName="ViewApp" CssClass="btn" />
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
