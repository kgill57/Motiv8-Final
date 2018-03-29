using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class BuyRewards : System.Web.UI.Page
{
    public int itemNumber;
    public int finalPurchase;
    public static Panel[] panelPost;
    public static Panel[] panelFooter;
    public static Panel[] panelPicture;
    public static Reward[] reward;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
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

    public Boolean checkFunds()
    {
        Boolean valid = true;

        SqlConnection con = new SqlConnection();


        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "SELECT AccountBalance FROM [User] WHERE UserID = @userID";
        cmd.Parameters.AddWithValue("@userID", (int)Session["UserID"]);
        double balance = Convert.ToDouble(cmd.ExecuteScalar());
       
        if (balance < reward[itemNumber].getRewardAmount())
        {
            valid = false;
        }
        con.Close();

        return valid;
    }


    protected void btnBuy_Click(object sender, EventArgs e)
    {

        //if (checkFunds() == false)
        //    return;

        //SqlConnection con = new SqlConnection();
        //con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        //con.Open();


        //bool itemSelected = false;

        //for (int i = 0; i < arraySize; i++)
        //{
        //    if (chkBuy[i].Checked == true)
        //    {
        //        itemSelected = true;
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "INSERT INTO RewardEarned (UserID, RewardID, DateClaimed) VALUES (@userID, @rewardID, @dateClaimed)";
        //        cmd.Parameters.AddWithValue("@userID", (int)Session["UserID"]);
        //        cmd.Parameters.AddWithValue("@rewardID", reward[i].getRewardID());
        //        cmd.Parameters.AddWithValue("@dateClaimed", DateTime.Today.Date);
        //        cmd.ExecuteNonQuery();
        //    }
        //}



        //for (int i = 0; i < arraySize; i++)
        //{
        //    if (chkBuy[i].Checked == true)
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "UPDATE [Reward] SET RewardQuantity = RewardQuantity - 1 WHERE RewardID = @reward";
        //        cmd.Parameters.AddWithValue("@reward", reward[i].getRewardID());
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //for (int i = 0; i < arraySize; i++)
        //{
        //    if (chkBuy[i].Checked == true)
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "UPDATE [User] SET AccountBalance = AccountBalance - @rewardAmount WHERE UserID = @userID";
        //        cmd.Parameters.AddWithValue("@userID", (int)Session["UserID"]);
        //        cmd.Parameters.AddWithValue("@rewardAmount", reward[i].getRewardAmount());
        //        cmd.ExecuteNonQuery();

        //        Session["AccountBalance"] = (decimal)Session["AccountBalance"] - Convert.ToDecimal(reward[i].getRewardAmount());
        //    }
        //}

        //if (itemSelected == true)
        //{
        //    //lblResult.Text = "Reward Claimed!";
        //    createRewardFeed();
        //}
        //else
        //{
        //    //lblResult.Text = "Please Select An Item";
        //}


        //con.Close();

        //Response.Redirect(Request.RawUrl);

    }

    public void createRewardFeed()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand read = new SqlCommand("SELECT * FROM [dbo].[Reward] WHERE RewardQuantity > 0 ORDER BY [DateAdded] DESC", con);
        SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        double totalBalance = Convert.ToDouble(balance.ExecuteScalar());


        //Create Scaler to see how many transactions there are
        SqlCommand scaler = new SqlCommand("SELECT COUNT(RewardID) FROM [dbo].[Reward] WHERE RewardQuantity > 0", con);
        int arraySize = (int)scaler.ExecuteScalar();

        SqlDataReader reader = read.ExecuteReader();

        reward = new Reward[arraySize];
        int arrayCounter = 0;
        while (reader.Read())
        {
            String pictureLink;
            //checks to see if there is a picture for the item
            if (!reader.IsDBNull(4))
            {
                pictureLink = Convert.ToString(reader.GetValue(4));
            }
            else
            {
                pictureLink = "Images/admin.png";
            }
            reward[arrayCounter] = new Reward(Convert.ToInt32(reader.GetValue(0)), Convert.ToString(reader.GetValue(1)),
                Convert.ToInt32(reader.GetValue(2)), Convert.ToDouble(reader.GetValue(3)), pictureLink, Convert.ToInt32(reader.GetValue(5)),
                Convert.ToDateTime(reader.GetValue(6)));
            arrayCounter++;
        }

        panelPost = new Panel[arraySize];
        panelFooter = new Panel[arraySize];
        panelPicture = new Panel[arraySize];
        Button[] claimButton = new Button[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            panelPost[i] = new Panel();
            panelFooter[i] = new Panel();
            panelPicture[i] = new Panel();
            panelPicture[i].Controls.Add(new LiteralControl("" +
                "<div class=\"\">" +
                         "<div class=\"card resetBorder\">" +
                                "<div class=\"card-image\">" +
                                    "<img src = \" " + reward[i].getRewardPicture() + "\">" +
                                    "<span class=\"card-title\"> <strong>$" + reward[i].getRewardAmount() + " - " + reward[i].getRewardName() + "</strong></span>" +
                                "</div>" +
                        "</div>" +
                "</div>" +
                ""));
            panelPost[i].Controls.Add(panelPicture[i]);

            Label lblQuantity = new Label();
            lblQuantity.Text = "Quantity Left: " + reward[i].getRewardQuantity();
            lblQuantity.CssClass = "quantityCSS";
            panelFooter[i].Controls.Add(lblQuantity);

            claimButton[i] = new Button();
            claimButton[i].Text = "Claim";
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
        Button btn = sender as Button;
        itemNumber = btn.TabIndex;
       
        Session["rewardNumber"] = itemNumber;
        if (checkFunds())
        {
            Popup.Visible = true;
            Popup.Enabled = true;
            

            //Add a panel to be used as the popup
            

            //Add Label for reward name
            lblMsg.Text = "Are you sure you want to purchase " + reward[itemNumber].getRewardName() + " for $" + reward[itemNumber].getRewardAmount() + "?";

        }
        else
        {

            Popup2.Visible = true;
            Popup2.Enabled = true;
            lblMsg2.Text = "You do not have enough funds for this reward";

        }

    }
    protected void confirm_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        SqlCommand sp = new SqlCommand("insertRewardEarned", con);
        sp.CommandType = CommandType.StoredProcedure;
        sp.Parameters.AddWithValue("@userID", (int)Session["UserID"]);
        sp.Parameters.AddWithValue("@rewardID", reward[(int)Session["rewardNumber"]].getRewardID());
        sp.Parameters.AddWithValue("@dateClaimed", DateTime.Today.Date);
        sp.ExecuteNonQuery();

        cmd.CommandText = "UPDATE [Reward] SET RewardQuantity = RewardQuantity - 1 WHERE RewardID = @reward";
        cmd.Parameters.AddWithValue("@reward", reward[(int)Session["rewardNumber"]].getRewardID());
        cmd.ExecuteNonQuery();

        Session["AccountBalance"] = (decimal)Session["AccountBalance"] - Convert.ToDecimal(reward[(int)Session["rewardNumber"]].getRewardAmount());

        Popup.Visible = false;
        Popup.Enabled = false;

        ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);



    }
    protected void cancel_Click(object sender, EventArgs e)
    {
        Popup.Visible = false;
        Popup.Enabled = false;
    }
    protected void ok_Click(object sender, EventArgs e)
    {
        Popup2.Visible = false;
        Popup2.Enabled = false;

    }
}