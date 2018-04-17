using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddRewardProviders : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
            loadProfilePicture();

            if (!IsPostBack)
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

    public void fillpayPanel(int providerID)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT COUNT(ProviderID) FROM RewardProvider", con);
        int rows = (int)cmd.ExecuteScalar();
        cmd.CommandText = "";
        cmd.CommandText = "SELECT RewardProvider.ProviderName, RewardProvider.ProviderEmail, SUM(Reward.RewardAmount) AS [Total] FROM Reward, RewardProvider, RewardEarned WHERE RewardProvider.ProviderID = Reward.ProviderID AND Reward.RewardID = RewardEarned.RewardID AND RewardProvider.ProviderID = " + providerID + " AND RewardProvider.EmployerID = " +(int)Session["EmployerID"]+ " GROUP BY RewardProvider.ProviderName, RewardProvider.ProviderEmail";

        cmd.Connection = con;
        SqlDataReader reader = cmd.ExecuteReader();


        //array of panels
        Panel[] panelArray = new Panel[rows];

        //counter
        int count = 0;

        while (reader.Read())
        {
            Label lblName = new Label();
            Label lblTotal = new Label();
            lblName.Text = Convert.ToString(reader.GetValue(0));
            lblTotal.Text = Convert.ToString(reader.GetValue(2));
            panelArray[count] = new Panel();
            panelArray[count].Controls.Add(new LiteralControl("<br />"));
            panelArray[count].Controls.Add(lblName);
            panelArray[count].Controls.Add(new LiteralControl("<br />"));
            panelArray[count].Controls.Add(lblTotal);
            panelArray[count].Controls.Add(new LiteralControl("<br />"));
            panelArray[count].Controls.Add(new LiteralControl("</form>"));
            panelArray[count].Controls.Add(new LiteralControl("<form target=\"paypal\" action=\"https://www.sandbox.paypal.com/cgi-bin/webscr\" method =\"post\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"cmd\" value=\"_cart\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"business\" value=\"" + Convert.ToString(reader.GetValue(1)) + "\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"lc\" value=\"US\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"item_name\" value=\"" + Convert.ToString(reader.GetValue(0)) + "\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"item_number\" value=\"1\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"amount\" value=\"" + Convert.ToInt16(reader.GetValue(2)) + "\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"currency_code\" value=\"USD\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"button_subtype\" value=\"products\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"add\" value=\"1\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"hidden\" name=\"bn\" value=\"PP - ShopCartBF:btn_cart_LG.gif:NonHosted\">"));
            panelArray[count].Controls.Add(new LiteralControl("<input type=\"image\" src=\"https://www.sandbox.paypal.com/en_US/i/btn/btn_cart_LG.gif\" border =\"0\" name=\"submit\" alt=\"PayPal - The safer, easier way to pay online!\">"));
            panelArray[count].Controls.Add(new LiteralControl("<img alt=\"\" border=\"0\" src=\"https://www.sandbox.paypal.com/en_US/i/scr/pixel.gif\" width =\"1\" height=\"1\"></form>"));

            //Add panel array to asp panel
            payPanel.Controls.Add(panelArray[count]);


            count++;
        }

        payPanel.Visible = true;

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

    protected void fillGridView()
    {
        try
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

            sc.Open();
            // Declare the query string.
            SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), sc);
            double totalBalance = Convert.ToDouble(balance.ExecuteScalar());

            lblBalance.Text = totalBalance.ToString("$#.00");

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT * FROM RewardProvider WHERE EmployerID = " + (int)Session["EmployerID"], sc);
            del.ExecuteNonQuery();

            grdProviders.DataSource = del.ExecuteReader();
            grdProviders.DataBind();
            sc.Close();

        }
        catch
        {

        }
    }


   



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Boolean textError = true;
        //Check if the project name Text box is empty
        if (String.IsNullOrEmpty(txtSearch.Text))
        {
            try
            {
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;


                sc.Open();
                //Declare the query string.

                System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT *" +
                    " FROM RewardProvider;", sc);
                del.ExecuteNonQuery();

                grdProviders.DataSource = del.ExecuteReader();
                grdProviders.DataBind();
                sc.Close();
            }
            catch
            {

            }
        }
        else
        {
            try
            {

                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
                sc.Open();
                // Declare the query string.

                System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT * FROM RewardProvider WHERE ProviderName LIKE '%' + @ProviderName;", sc);
                del.Parameters.AddWithValue("@ProviderName", txtSearch.Text);
                del.ExecuteNonQuery();

                grdProviders.DataSource = del.ExecuteReader();
                grdProviders.DataBind();
                sc.Close();

            }
            catch
            {

            }
        }
    }


    protected void btnPay_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        // assuming you store the ID in a Hiddenield:
        int rowIndex = row.RowIndex + 1;

        fillpayPanel(rowIndex);
    }






    protected void btnClosePay_Click(object sender, EventArgs e)
    {
        payPanel.Visible = false;
    }

    
}
