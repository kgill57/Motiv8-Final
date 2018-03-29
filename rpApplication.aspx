<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rpApplication.aspx.cs" Inherits="rpApplication" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
                        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html>
        <head><title> MOTIV8 | Home
            </title>
              <!--Import Google fonts-->
              <link href="https://fonts.googleapis.com/css?family=Oxygen:700" rel="stylesheet">
              <!--Import Google Icon Font-->
              <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
              <!--Import materialize.css-->
              <link type="text/css" rel="stylesheet" href="css/materialize.css"  media="screen,projection"/>

              <!--Let browser know website is optimized for mobile-->
              <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

              <!--modal style-->
              <style type="text/css">
                .modal {
                      max-height: 90%;
                    }

                    .modal-form-row {
                      margin-bottom: 0px;
                    }
              </style>
        
        </head>
        <body>
            <h1 style="position:center">Apply to Motiv8</h1>
            <div class="row count-down">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Company Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDesc" runat="server" Text="Why do you want to be a part of Motiv8?"></asp:Label>
                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="113px" Width="315px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnApply" runat="server" Text="Send Application" CssClass="btn" OnClick="btnApply_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            </div>
            
        </body>
    </html>
</asp:Content>
