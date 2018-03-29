using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class MyRewards : System.Web.UI.Page
{
    public int itemNumber;
    public static Panel[] panelPost;
    public static Panel[] panelFooter;
    public static Panel[] panelPicture;
    public static Reward[] reward;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");
        }

        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
        createRewardFeed();
        loadProfilePicture();

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        // Show user's name and balance in sidebar
       
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
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");

        }
        catch (Exception)
        {

        }
        con.Close();
    }


    public void createRewardFeed()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand scaler = new SqlCommand("SELECT Count(RewardEarned.DateClaimed) FROM RewardEarned WHERE RewardEarned.UserID = " + Convert.ToString((int)Session["UserID"]), con);
        int size = (int)scaler.ExecuteScalar();

        SqlCommand read = new SqlCommand("SELECT Reward.RewardName, Reward.RewardAmount, Reward.RewardPicture, RewardEarned.DateClaimed FROM Reward inner join RewardEarned ON Reward.[RewardID] = RewardEarned.RewardID WHERE RewardEarned.[UserID] = @UserID", con);
        read.Parameters.AddWithValue("@UserID", (int)Session["UserID"]);
        SqlDataReader reader = read.ExecuteReader();
        Panel[] panel = new Panel[size];
        Panel[] panelHeader = new Panel[size];
        Panel[] panelText = new Panel[size];
        int arrayCounter = 0;
        reward = new Reward[size];
        while (reader.Read())
        {
            String pictureLink;
            //checks to see if there is a picture for the item
            if (!reader.IsDBNull(2))
            {
                pictureLink = Convert.ToString(reader.GetValue(2));
            }
            else
            {
                pictureLink = "Images/admin.png";
            }

            reward[arrayCounter] = new Reward(Convert.ToString(reader.GetValue(0)), Convert.ToDouble(reader.GetValue(1)), Convert.ToString(reader.GetValue(2)), Convert.ToDateTime(reader.GetValue(3)));
            
            arrayCounter++;
        }

        panelPost = new Panel[arrayCounter];
        panelFooter = new Panel[arrayCounter];
        panelPicture = new Panel[arrayCounter];
        Button[] claimButton = new Button[arrayCounter];

        for (int i = 0; i < arrayCounter; i++)
        {
            panelPost[i] = new Panel();
            panelFooter[i] = new Panel();
            panelPicture[i] = new Panel();
            panelPicture[i].Controls.Add(new LiteralControl("" +
                "<div class=\"\">" +
                         "<div class=\"card resetBorder\">" +
                                "<div class=\"card-image\">" +
                                    "<img src = \" " + reward[i].getRewardPicture() + "\">" +
                                    "<span class=\"card-title\"> <strong>" + reward[i].getRewardName() + "</strong></span>" +
                                "</div>" +
                        "</div>" +
                "</div>" +
                ""));
            panelPost[i].Controls.Add(panelPicture[i]);

            claimButton[i] = new Button();
            claimButton[i].Text = "Details";
            claimButton[i].ID = ("button" + Convert.ToString(i));
            claimButton[i].Click += new EventHandler(claimButton_Click);
            claimButton[i].CssClass = "btn buttonMargins";


            claimButton[i].TabIndex = Convert.ToInt16(i);

            panelPicture[i].CssClass = "w3-container resetBorder";
            panelPost[i].CssClass = "w3-card-4 rewardPost";
            panelFooter[i].CssClass = "w3-container white";


            panelFooter[i].Controls.Add(claimButton[i]);
            panelPost[i].Controls.Add(panelFooter[i]);


            Panel1.Controls.Add(panelPost[i]);
        }
        con.Close();

    }
    protected void claimButton_Click(object sender, EventArgs e)
    {
        
    }
    
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //        fillRewards();

    //    loadProfilePicture();

    //}

    //public void fillRewards()
    //{
    //    SqlConnection con = new SqlConnection();
    //    con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
    //    con.Open();

    //    SqlCommand scaler = new SqlCommand("SELECT Count(RewardEarned.DateClaimed) FROM RewardEarned WHERE RewardEarned.UserID = " + Convert.ToString((int)Session["UserID"]), con);
    //    int size = (int)scaler.ExecuteScalar();

    //    SqlCommand read = new SqlCommand("SELECT Reward.RewardName, Reward.RewardAmount, RewardEarned.DateClaimed FROM Reward inner join RewardEarned ON Reward.[RewardID] = RewardEarned.RewardID WHERE RewardEarned.[UserID] = @UserID", con);
    //    read.Parameters.AddWithValue("@UserID", (int)Session["UserID"]);
    //    SqlDataReader reader = read.ExecuteReader();
    //    Panel[] panel = new Panel[size];
    //    Panel[] panelHeader = new Panel[size];
    //    Panel[] panelText = new Panel[size];
    //    int counter = 0;

    //    while (reader.Read())
    //    {
    //        panel[counter] = new Panel();
    //        panelHeader[counter] = new Panel();
    //        panelText[counter] = new Panel();

    //        Label[] lblArray = new Label[3];

    //        lblArray[0] = new Label();

    //        lblArray[0].Text = "Reward: " + (String)reader.GetValue(0);
    //        panelHeader[counter].Controls.Add(lblArray[0]);

    //        lblArray[1] = new Label();

    //        lblArray[1].Text = "Value: $" + ((Decimal)reader.GetValue(1)).ToString("0.##");

    //        panelText[counter].Controls.Add(lblArray[1]);

    //        panelText[counter].Controls.Add(new LiteralControl("<br />"));

    //        lblArray[2] = new Label();

    //        DateTime date = (DateTime)reader.GetValue(2);

    //        lblArray[2].Text = "Purchase Date: " + Convert.ToString(date.Month) + "/" + Convert.ToString(date.Day) + "/" + Convert.ToString(date.Year);
    //        panelText[counter].Controls.Add(lblArray[2]);

    //        panel[counter].CssClass = "w3 - card - 4";
    //        panelHeader[counter].CssClass = "w3-container w3-blue";
    //        panelText[counter].CssClass = "w3-container";

    //        panelText[counter].Style.Add("text-align", "left");
    //        panelHeader[counter].Style.Add("text-align", "left");

    //        panel[counter].Style.Add("margin-top", "4px");
    //        panel[counter].Style.Add("margin-bottom", "16px");
    //        panelHeader[counter].Style.Add("font-size", "200%");



    //        Panel1.Controls.Add(panel[counter]);
    //        panel[counter].Controls.Add(panelHeader[counter]);
    //        panel[counter].Controls.Add(panelText[counter]);

    //        counter++;


    //    }

    //}

    //protected void loadProfilePicture()
    //{
    //    SqlConnection con = new SqlConnection();
    //    con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
    //    con.Open();

    //    try
    //    {

    //        SqlCommand select = new SqlCommand();
    //        select.Connection = con;

    //        select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
    //        string currentPicture = (String)select.ExecuteScalar();

    //        profilePicture.ImageUrl = "~/Images/" + currentPicture;
    //        lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");

    //    }
    //    catch (Exception)
    //    {

    //    }
    //    con.Close();
    //}
}