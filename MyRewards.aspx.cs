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
            claimButton[i].Click += new EventHandler(detailsButton_Click);
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
    protected void ok_Click(object sender, EventArgs e)
    {
        Popup.Enabled = false;
        Popup.Visible = false;
    }
    protected void detailsButton_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        itemNumber = btn.TabIndex;
        Popup.Visible = true;
        Popup.Enabled = true;

        lblItemName.Text = reward[itemNumber].getRewardName();
        lblItemInfo.Text = "*Instructions to pick up item or claim item online*";
    }
    
    
}