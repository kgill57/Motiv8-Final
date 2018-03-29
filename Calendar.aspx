<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.css" rel="stylesheet" />

    <!--Import Google fonts-->
              <link href="https://fonts.googleapis.com/css?family=Oxygen:700" rel="stylesheet">
              <!--Import Google Icon Font-->
              <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
              <!--Import materialize.css-->
              <link type="text/css" rel="stylesheet" href="css/materialize.css"  media="screen,projection"/>

              <!--Let browser know website is optimized for mobile-->
              <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

    <style type='text/css'>
        h5 {
            margin-left: auto;
            margin-right: auto;
            text-align: center;
           }

        #calendar
        {
            font-size: large;
            width: 1200px;
            margin: 0 auto;
            margin-right: 230px;
        }
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
        .modal 
        {
            max-height: 90%;
        }

        .modal-form-row 
        {
            margin-bottom: 0px;
        }
    </style>

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
               
                  <li><a class="waves-effect" href="Default.aspx">Logout</a></li>
                  <li><a><i class="material-icons nav1 modal-trigger" data-target="modal4"> help</i></a>
                  </ul>
            </div>

    <div style ="float: right; width: 85%;">
    <div style = "margin-left:auto; margin-right:auto; width:85%;">
    <nav class="feednav" >
        <div class="nav-wrapper">
            <a class="brand-logo nav1 panel">Calender</a>
            <ul id="nav-mobile" class="right hide-on-med-and-down"> 
            
            </ul>
            
        </div>
    </nav>
    <div style ="background-color: white;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="calendar">
    </div>
    <div id="updatedialog" style="font: 90%, sans-serif; margin: 50px; display: none;"
        title="Update or Delete Event">
        <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End: </td>
                <td class="alignLeft">
                    <span id="eventEnd"></span><input type="hidden" id="eventId" /></td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="font: 90% sans-serif; margin: 50px;" title="Add Event">
    <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" ></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" ></span></td>
            </tr>
        </table>
        
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.js"></script>
    <script src="Scripts/calendarscript.js" type="text/javascript"></script>
    <script src="Scripts/Sidebar.js"></script>



    </div>
    </div>
    </div>
   

</div>

    </asp:Panel>


    <!-- side nav bar for profile-->
   
        <%--<div style = "float: left; width: 15%;">
         <ul id="slide-out" class="side-nav fixed" style = "width:15%;">
            <li><div class="user-view">
                <asp:Image ID ="profilePicture" Height ="120px" Width ="120px" CssClass ="circle user" runat ="server"/>
                <h5><asp:Label ID="lblUser" runat="server" Text="" CssClass ="user1"></asp:Label></h5>
                <asp:Label ID="lblBalance" runat="server" ></asp:Label>
            </div></li>
          <li><a href="rpRewards.aspx">Rewards</a></li>
          <li><a href="Calendar.aspx">Calendar</a></li>
          <li><a href="rpSettings.aspx">Settings</a></li>
          <li><a class="waves-effect" href="Default.aspx">Logout</a></li>
          <li><a><i class="material-icons nav1 modal-trigger" data-target="modal4"> help</i></a>
          </ul>
            </div>
          <a href="#" data-activates="slide-out" class="button-collapse"><i class="material-icons">menu</i></a>

    <!-- End Side Nav-->

    <div style="position: relative; top: 60px;">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="calendar">
    </div>
    <div id="updatedialog" style="font: 90%, sans-serif; margin: 50px; display: none;"
        title="Update or Delete Event">
        <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="eventStart"></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End: </td>
                <td class="alignLeft">
                    <span id="eventEnd"></span><input type="hidden" id="eventId" /></td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="font: 90% sans-serif; margin: 50px;" title="Add Event">
    <table class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="33" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" ></span></td>
            </tr>
            <tr>
                <td class="alignRight">
                    End:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" ></span></td>
            </tr>
        </table>
        
    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.js"></script>
    <script src="Scripts/calendarscript.js" type="text/javascript"></script>
    <script src="Scripts/Sidebar.js"></script>
      
    </div>--%>
</asp:Content>
