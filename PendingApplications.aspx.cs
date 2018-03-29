using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class PendingApplications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            fillGridView();
    }

    public void fillGridView()
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

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT * FROM RewardProvider WHERE PendingReview = 1;", sc);
            del.ExecuteNonQuery();

            grdProviders.DataSource = del.ExecuteReader();
            grdProviders.DataBind();
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

    protected void btnViewApp_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        // assuming you store the ID in a Hiddenield:
        int rowIndex = row.RowIndex + 1;

        //int providerID = Convert.ToInt16(grdProviders.Rows[RowIndex].Cells[0].Text);

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT ProviderName, ProviderEmail, ProviderDescription FROM [Application] WHERE ApplicationID = " + rowIndex;
        cmd.Connection = con;
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string name = Convert.ToString(reader.GetValue(0));
            string email = Convert.ToString(reader.GetValue(1));
            string desc = Convert.ToString(reader.GetValue(2));

            txtCompanyName.Text = name;
            txtEmail.Text = email;
            txtDesc.Text = desc;
        }

        appPanel.Visible = true;
    }

    protected void btnExitApp_Click(object sender, EventArgs e)
    {
        appPanel.Visible = false;
        Response.Redirect(Request.RawUrl);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UPDATE RewardProvider SET PendingReview = 0 WHERE ProviderName = @providerName";
        cmd.Parameters.AddWithValue("@providerName", txtCompanyName.Text);
        cmd.Connection = con;

        cmd.ExecuteNonQuery();

        con.Close();

        lblResult.Text = "Application Approved!";

        txtCompanyName.Text = "";
        txtDesc.Text = "";
        txtEmail.Text = "";
    }
}