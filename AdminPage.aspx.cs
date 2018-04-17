using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");
            loadNewsFeed();
            loadProfilePicture();

            if((int)Session["Admin"] != 1)
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
        
    }

    protected void loadProfilePicture()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        try
        {

            SqlCommand select = new SqlCommand();
            select.Connection = con;

            select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
            string currentPicture = (String)select.ExecuteScalar();

            profilePicture.ImageUrl = "~/Images/" + currentPicture;
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];

        }
        catch (Exception)
        {

        }
        con.Close();
    }

    protected void loadNewsFeed()
    {
        //Populates the nav bar with the admin's first and last name
        lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];



        //sql connection
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] WHERE EmployerID = " + Convert.ToString((int)Session["EmployerID"]) + " ORDER BY [TransID] DESC", con);
        SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        double totalBalance = Convert.ToDouble(balance.ExecuteScalar());

        lblBalance.Text = totalBalance.ToString("$#.00");

        //Create Scaler to see how many transactions there are
        SqlCommand scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION] WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        int arraySize = (int)scaler.ExecuteScalar();

        SqlDataReader reader = read.ExecuteReader();

        Post[] transaction = new Post[arraySize];
        int arrayCounter = 0;
        while (reader.Read())
        {
            transaction[arrayCounter] = new Post(Convert.ToInt32(reader.GetValue(0)), Convert.ToString(reader.GetValue(1)),
                Convert.ToString(reader.GetValue(2)), Convert.ToString(reader.GetValue(3)), Convert.ToDouble(reader.GetValue(4)), Convert.ToDateTime(reader.GetValue(5)), Convert.ToBoolean(reader.GetValue(6)), Convert.ToInt32(reader.GetValue(7)), Convert.ToInt32(reader.GetValue(8)));
            arrayCounter++;
        }
        con.Close();
        
        
        //Panel[] panelHeader = new Panel[arraySize];
        Panel[] panelPost = new Panel[arraySize];
        //Panel[] mainPanels = new Panel[arraySize];
        con.Open();

        for (int i = 0; i < arraySize; i++)
        {
            String senderPic;
            String finalSenderPic;
            try
            {
                SqlCommand select = new SqlCommand();
                select.Connection = con;

                select.CommandText = "SELECT [ProfilePicture] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getGiverID();
                senderPic = (String)select.ExecuteScalar();
                finalSenderPic = "Images/" + senderPic;
            }
            catch (Exception)
            {
                finalSenderPic = "Images/admin.png";
            }
            String recieverPic;
            String finalRecieverPic;
            try
            {
                SqlCommand select = new SqlCommand();
                select.Connection = con;

                select.CommandText = "SELECT [ProfilePicture] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getReceiverID();
                recieverPic = (String)select.ExecuteScalar();
                finalRecieverPic = "Images/" + recieverPic;
            }
            catch (Exception)
            {
                finalRecieverPic = "Images/admin.png";
            }

            //String recieverPic;
            //panelPost[i] = new Panel();
            //panelPost[i].Controls.Add(new LiteralControl("<div class=\"col s12 m8 offset-m2 l6 offset-l3 card-panel grey lighten-5 z-depth-1 row valign-wrapper\"> "));
            //panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 20%\"> <img src = \"" + finalSenderPic + "\" alt = \"\" class=\"circle feed responsive-img\"> </br> <img src=\"" + finalRecieverPic + "\" alt=\"#\" class=\"circle feed responsive-img\"> </div>"));
            //panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 59%\"> <span style = \"display: inline; width:60%; font-size:200%;\" cssclass=\"black-text\"><strong>" + transaction[i].getGiverNickName(transaction[i].getGiverID()) + "</strong> rewarded <strong>" + transaction[i].getReceiverNickName(transaction[i].getReceiverID()) + "</strong> $" + transaction[i].getRewardValue() + "</span > " +
            //    " <br/><br/> <span style = \"display: inline; width:60%;\">" + transaction[i].getDescription() + "</span> <br/><br/> <span style = \"display: inline; width:60%;\"> Category: " + transaction[i].getCategory() + "</span> </div>"));
            //panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: right; \"> <img src = \"" + getValueImageSrc(transaction[i].getValue()) + "\" alt = \"\" class=\"iconforvalue\" width = \"80%\"> </div>"));
            //panelPost[i].Controls.Add(new LiteralControl("</div>"));

            panelPost[i] = new Panel();
            panelPost[i].Controls.Add(new LiteralControl("<div class=\"col s12 m8 offset-m2 l6 offset-l3 card-panel grey lighten-5 z-depth-1 row valign-wrapper\"> "));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 10%\"> <img src = \"" + finalSenderPic + "\" alt = \"\" class=\"circle feed responsive-img\"> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 80%\"> <span style = \"display: inline; width:60%; font-size:200%;\" cssclass=\"black-text\"><strong>" + transaction[i].getGiverNickName(transaction[i].getGiverID()) + "</strong> rewarded <strong>" + transaction[i].getReceiverNickName(transaction[i].getReceiverID()) + "</strong> $" + transaction[i].getRewardValue() + "</span > " +
                " <br/><br/> <span style = \"display: inline; width:60%;\">" + transaction[i].getDescription() + "</span> <br/><br/> <span style = \"display: inline; width:60%;\"> Value: " + transaction[i].getValue() + "</span>" +
                " <br/> <span style = \"display: inline; width:60%;\"> Category: " + transaction[i].getCategory() + "</span> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 10%\"> <img src=\"" + finalRecieverPic + "\" alt=\"#\" class=\"circle feed responsive-img\"> </div>")); //<img src = \"" + getValueImageSrc(transaction[i].getValue()) + "\" alt = \"\" class=\"iconforvalue\" width = \"80%\">
            panelPost[i].Controls.Add(new LiteralControl("</div>"));
            

            Panel1.Controls.Add(panelPost[i]);
            //mainPanels[i].Controls.Add(panelHeader[i]);
            //mainPanels[i].Controls.Add(panelPost[i]);
        }
        con.Close();
    }
    public static string getValueImageSrc(String value)
    {
        String imgSrc;
        //if statemets to select which value and image to show 
        if (value.Equals("Health, Well Being and Safety Of Team Members"))
        {
            imgSrc = "icons/grouphealth.png";
        }
        else if (value.Equals("Community Involvement"))
        {
            imgSrc = "icons/communityinv.png";
        }
        else if (value.Equals("Customer Service"))
        {
            imgSrc = "icons/customerservice.png";
        }
        else if (value.Equals("Retaining/Attracting New Customers"))
        {
            imgSrc = "icons/addcustomer.png";
        }
        else if (value.Equals("Leadership"))
        {
            imgSrc = "icons/leadership.png";
        }
        else if (value.Equals("Process Improvement Initiatives"))
        {
            imgSrc = "icons/process.png";
        }
        else if (value.Equals("Education"))
        {
            imgSrc = "icons/education.png";
        }
        else
        {
            imgSrc = "icons/teambuilding.png";
        }

        return imgSrc;
    }
}