using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class rpApplication : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "INSERT INTO RewardProvider (ProviderName, ProviderEmail, ProviderPicture, PendingReview) VALUES (@providerName, @providerEmail, NULL, 1)";
        cmd.Parameters.AddWithValue("@providerName", txtCompanyName.Text);
        cmd.Parameters.AddWithValue("@providerEmail", txtEmail.Text);
        cmd.Parameters.AddWithValue("@providerDesc", txtDesc.Text);

        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO [Application] (ProviderName, ProviderEmail, ProviderDescription, ProviderID) VALUES (@providerName, @providerEmail, @providerDesc, @providerID)";
        cmd.Parameters.AddWithValue("@providerID", newProviderID());

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        lblResult.Text = "Application Sent!";
    }

    protected int newProviderID()
    {

        //con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "SELECT MAX(ProviderID) FROM RewardProvider";

        int providerID = (int)cmd.ExecuteScalar();

        con.Close();

        return providerID;
    }
}