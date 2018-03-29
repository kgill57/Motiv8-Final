using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class rpHome : System.Web.UI.Page
{
    SqlConnection con;
    public int itemNumber;
    public static Panel[] panelPost;
    public static Panel[] panelFooter;
    public static Panel[] panelPicture;
    public static Reward[] reward;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        if (!IsPostBack)
        {
            loadProfilePicture();
            
        }
        loadRewardsFeed();
    }

    protected void loadProfilePicture()
    {
        RewardProvider rp = new RewardProvider();
        con.Open();

        try
        {
            SqlCommand select = new SqlCommand();
            select.Connection = con;

            select.CommandText = "SELECT ProviderPicture FROM [dbo].[RewardProvider] WHERE ProviderID = " + Session["ProviderID"];
            string currentPicture = (String)select.ExecuteScalar();

            profilePicture.ImageUrl = "~/Images/" + currentPicture;
            lblUser.Text = (String)Session["ProviderName"];
        }
        catch (Exception)
        {

        }

        con.Close();
    }

    protected void loadRewardsFeed()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand read = new SqlCommand("SELECT * FROM [dbo].[Reward] WHERE [Reward].ProviderID = " + (int)Session["ProviderID"], con);

        //Create Scaler to see how many transactions there are
        SqlCommand scaler = new SqlCommand("SELECT COUNT(RewardID) FROM [dbo].[Reward] WHERE [Reward].ProviderID = " + (int)Session["ProviderID"], con);
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

        //Add a panel to be used as the popup
        Panel popup = new Panel();

        //Add Label for reward name
        Label lblMsg = new Label();
        lblMsg.CssClass = "popupLabels";
        lblMsg.Text = "Are you sure you want to delete " + reward[itemNumber].getRewardName() + "?";
        popup.Controls.Add(lblMsg);

        Button btnDelete = new Button();
        btnDelete.Text = "Delete";
        btnDelete.CssClass = "btn buttonMargins";
        btnDelete.Click += new EventHandler(delete_Click);
        btnDelete.Style.Add("color", "red");
        popup.Controls.Add(btnDelete);

        Button btnCancel = new Button();
        btnCancel.Text = "Cancel";
        btnCancel.CssClass = "btn buttonMargins";
        btnCancel.Click += new EventHandler(cancel_Click);
        popup.Controls.Add(btnCancel);

        popup.CssClass = "popupPanel";
        masterPanel.Controls.Add(popup);


    }
    protected static void delete_Click(object sender, EventArgs e)
    {
        //Delete item by removing the quantity
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
    }

    protected void btnAddReward_Click(object sender, EventArgs e)
    {
        addReward.Visible = true;
    }
}
