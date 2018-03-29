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
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
        //Calls the method to load the news feed
        
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

        SqlCommand read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] ORDER BY [TransID] DESC", con);
        SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        double totalBalance = Convert.ToDouble(balance.ExecuteScalar());

        lblBalance.Text = totalBalance.ToString("$#.00");

        //Create Scaler to see how many transactions there are
        SqlCommand scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION]", con);
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
            panelPost[i] = new Panel();
            panelPost[i].Controls.Add(new LiteralControl("<div class=\"col s12 m8 offset-m2 l6 offset-l3 card-panel grey lighten-5 z-depth-1 row valign-wrapper\"> "));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 20%\"> <img src = \"" + finalSenderPic + "\" alt = \"\" class=\"circle feed responsive-img\"> </br> <img src=\"" + finalRecieverPic + "\" alt=\"#\" class=\"circle feed responsive-img\"> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 59%\"> <span style = \"display: inline; width:60%; font-size:200%;\" cssclass=\"black-text\"><strong>" + transaction[i].getGiverNickName(transaction[i].getGiverID()) + "</strong> rewarded <strong>" + transaction[i].getReceiverNickName(transaction[i].getReceiverID()) + "</strong> $" + transaction[i].getRewardValue() + "</span > " +
                " <br/><br/> <span style = \"display: inline; width:60%;\">" + transaction[i].getDescription() + "</span> <br/><br/> <span style = \"display: inline; width:60%;\"> Category: " + transaction[i].getCategory() + "</span> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: right; \"> <img src = \"" + getValueImageSrc(transaction[i].getValue()) + "\" alt = \"\" class=\"iconforvalue\" width = \"80%\"> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("</div>"));










            //panelHeader[i] = new Panel();

            //Label[] labelPost = new Label[5];


            //labelPost[0] = new Label();

            //if (transaction[i].getIsPrivate() == true)
            //{
            //    labelPost[0].Text = ("Anonymous" + " gifted " + "Anonymous");
            //}
            //else
            //{

            //    SqlCommand select = new SqlCommand("SELECT [FName] + ' ' + [LName] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getGiverID(), con);
            //    String giver = (String)select.ExecuteScalar();

            //    select.CommandText = "SELECT [FName] + ' ' + [LName] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getReceiverID();
            //    String reciever = (String)select.ExecuteScalar();

            //    labelPost[0].Text = (giver + " gifted " + reciever + " $" + transaction[i].getRewardValue());
            //}

            //panelHeader[i].Controls.Add(labelPost[0]);

            //labelPost[1] = new Label();
            //labelPost[1].Text = ("Value: " + transaction[i].getValue());
            //panelPost[i].Controls.Add(labelPost[1]);

            //panelPost[i].Controls.Add(new LiteralControl("<br />"));

            //labelPost[2] = new Label();
            //labelPost[2].Text = ("Category: " + transaction[i].getCategory());
            //panelPost[i].Controls.Add(labelPost[2]);

            //panelPost[i].Controls.Add(new LiteralControl("<br />"));

            //labelPost[3] = new Label();
            //labelPost[3].Text = ("Description: " + transaction[i].getDescription());
            //panelPost[i].Controls.Add(labelPost[3]);

            //panelPost[i].Controls.Add(new LiteralControl("<br />"));

            //labelPost[4] = new Label();

            //TimeSpan difference = DateTime.Now - transaction[i].getPostDate();
            //labelPost[4].Text = "Posted " + Convert.ToString((int)difference.TotalMinutes) + " Minutes Ago";

            //panelPost[i].Controls.Add(labelPost[4]);



            //mainPanels[i].CssClass = "w3 - card - 4";
            //panelHeader[i].CssClass = "w3-container w3-blue";
            //panelPost[i].CssClass = "w3-container";

            //mainPanels[i].Style.Add("text-align", "left");
            //panelHeader[i].Style.Add("text-align", "left");

            //mainPanels[i].Style.Add("margin-top", "4px");
            //mainPanels[i].Style.Add("margin-bottom", "16px");
            //panelHeader[i].Style.Add("font-size", "200%");

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