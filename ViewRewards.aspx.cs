using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



public partial class ViewRewards : System.Web.UI.Page
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
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
            loadProfilePicture();

            loadRewardsFeed();
            if(!IsPostBack)
                fillGridView();

            if ((int)Session["Admin"] != 1)
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }




    }

    protected void fillGridView()
    {
        try
        {

            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

            sc.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT RewardProvider.ProviderName, Reward.RewardName, Reward.RewardQuantity, " +
                "Reward.RewardAmount, Reward.PendingReview, Reward.DateAdded FROM RewardProvider, Reward " +
                "WHERE Reward.ProviderID = RewardProvider.ProviderID AND Reward.PendingReview = 1 AND RewardProvider.EmployerID = " +(int)Session["EmployerID"]+ "" +
                "GROUP BY RewardProvider.ProviderName, Reward.RewardName, Reward.RewardQuantity, Reward.RewardAmount, Reward.PendingReview, Reward.DateAdded", sc);
            cmd.ExecuteNonQuery();

            pendingRewardsGrid.DataSource = cmd.ExecuteReader();
            pendingRewardsGrid.DataBind();
            sc.Close();

        }
        catch
        {

        }
    }

    protected void loadProfilePicture()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
        string currentPicture = (String)select.ExecuteScalar();

        profilePicture.ImageUrl = "~/Images/" + currentPicture;
        lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];

        
        con.Close();
    }

    protected void loadRewardsFeed()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand read = new SqlCommand("SELECT Reward.*, RewardProvider.EmployerID FROM Reward INNER JOIN RewardProvider ON Reward.ProviderID = RewardProvider.ProviderID WHERE Reward.PendingReview = 0 AND RewardProvider.EmployerID = " + Convert.ToString((int)Session["EmployerID"]), con);
        SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        double totalBalance = Convert.ToDouble(balance.ExecuteScalar());

        lblBalance.Text = totalBalance.ToString("$#.00");

        //Create Scaler to see how many transactions there are
        SqlCommand scaler = new SqlCommand("SELECT COUNT(Reward.RewardID) FROM Reward INNER JOIN RewardProvider ON Reward.ProviderID = RewardProvider.ProviderID WHERE Reward.PendingReview = 0 AND RewardProvider.EmployerID = " + Convert.ToString((int)Session["EmployerID"]), con);
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
                Convert.ToInt32(reader.GetValue(2)), Convert.ToDouble(reader.GetValue(3)), pictureLink, Convert.ToInt32(reader.GetValue(6)), 
                Convert.ToDateTime(reader.GetValue(7)));
            arrayCounter++;
        }

        panelPost = new Panel[arraySize];
        panelFooter = new Panel[arraySize];
        panelPicture = new Panel[arraySize];
        Button[] deleteButton = new Button[arraySize];

        for (int i = 0; i < arraySize; i++)
        {
            panelPost[i] = new Panel();
            panelFooter[i] = new Panel();
            panelPicture[i] = new Panel();
            panelPicture[i].Controls.Add(new LiteralControl("" +
                "<div class=\"\">" +
                         "<div class=\"card resetBorder\">" +
                                "<div class=\"card-image\">" +
                                    "<img src = \" "+ reward[i].getRewardPicture() +"\">" +
                                    "<span class=\"card-title\"> <strong>$"+ reward[i].getRewardAmount() + " - " + reward[i].getRewardName() +"</strong></span>" +
                                "</div>" +
                        "</div>" +
                "</div>" +
                ""));
            panelPost[i].Controls.Add(panelPicture[i]);

            Label lblQuantity = new Label();
            lblQuantity.Text = "Quantity Left: " + reward[i].getRewardQuantity();
            lblQuantity.CssClass = "quantityCSS";
            panelFooter[i].Controls.Add(lblQuantity);

            deleteButton[i] = new Button();
            deleteButton[i].Text = "Delete";
            deleteButton[i].ID = ("button" + Convert.ToString(i));
            deleteButton[i].Click += new EventHandler(button_Click);
            deleteButton[i].CssClass = "btn buttonMargins";

            deleteButton[i].TabIndex = Convert.ToInt16(i);

            panelPicture[i].CssClass = "w3-container resetBorder";
            panelPost[i].CssClass = "w3-card-4 rewardPost";
            panelFooter[i].CssClass = "w3-container white";

            
            panelFooter[i].Controls.Add(deleteButton[i]);
            panelPost[i].Controls.Add(panelFooter[i]);



            Panel1.Controls.Add(panelPost[i]);
        }
        con.Close();
    }
    protected void button_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        itemNumber = btn.TabIndex;

       
        Session["rewardNumber"] = reward[itemNumber].getRewardID();
       
        Popup.Visible = true;
        Popup.Enabled = true;
        lblMsg.Text = "Are you sure you want to delete " + reward[itemNumber].getRewardName();

    }

    protected void delete_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();
        SqlCommand update = new SqlCommand("UPDATE[dbo].[Reward] SET RewardQuantity = '0' WHERE RewardID = " + Convert.ToString((int)Session["rewardNumber"]), con);
        update.ExecuteNonQuery();
        con.Close();

        Popup.Visible = false;
        Popup.Enabled = false;

        ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);

    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
    }


    public int findProviderID(string providerName)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand select = new SqlCommand("SELECT ProviderID FROM RewardProvider WHERE ProviderName LIKE '%' + @providerName", sc);
        select.Parameters.AddWithValue("@providerName", providerName);

        int providerID = Convert.ToInt32(select.ExecuteScalar());

        return providerID;
    }



    protected void pendingRewardsGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        pendingRewardsGrid.EditIndex = e.NewEditIndex;
        fillGridView();
    }

    protected void pendingRewardsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int num = Convert.ToInt32(pendingRewardsGrid.DataKeys[e.RowIndex].Value);

        //GridViewRow row = (GridViewRow)pendingRewardsGrid.Rows[e.RowIndex];

        //int x = Convert.ToInt32(row);

        try
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

            sc.Open();
            //Declare the query string.

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("DELETE" +
                " FROM [Reward] WHERE RewardName = @rewardName;", sc);
            del.Parameters.AddWithValue("@rewardName", (pendingRewardsGrid.Rows[e.RowIndex].FindControl("lblRewardName") as Label).Text.ToString());
            del.ExecuteNonQuery();
            del.ExecuteNonQuery();
            sc.Close();
            fillGridView();
        }
        catch
        {

        }
    }

    protected void pendingRewardsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        
        // Declare var variables to store the row currently being edited
        var ddl = pendingRewardsGrid.Rows[e.RowIndex].FindControl("drpApproval") as DropDownList;

        sc.Open();

        // Declare the query string
        try
        {
            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("UPDATE [Reward] SET PendingReview = @review " +
                "WHERE RewardName = @rewardName", sc);

            del.Parameters.AddWithValue("@rewardName", (pendingRewardsGrid.Rows[e.RowIndex].FindControl("lblRewardName") as Label).Text.ToString());
            del.Parameters.AddWithValue("@review", ddl.SelectedValue);
            del.ExecuteNonQuery();
            sc.Close();
            pendingRewardsGrid.EditIndex = -1;
            fillGridView();
            loadRewardsFeed();
        }

        catch
        {

        }

        Response.Redirect(Request.RawUrl);
    }

    protected void pendingRewardsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        pendingRewardsGrid.EditIndex = -1;
        fillGridView();
    }
}