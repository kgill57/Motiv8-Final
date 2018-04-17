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
        try
        {
            if(!IsPostBack)
            {
                lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");
                fillGridView();
                loadProfilePicture();
            }

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

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT * FROM RewardProvider WHERE Approved = 0 AND EmployerID = " +(int)Session["EmployerID"], sc);
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

        

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
        string currentPicture = (String)select.ExecuteScalar();

        profilePicture.ImageUrl = "~/Images/" + currentPicture;
        lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];

        
        con.Close();
    }



    protected void grdProviders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdProviders.EditIndex = -1;
        fillGridView();
    }

    protected void grdProviders_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdProviders.EditIndex = e.NewEditIndex;
        fillGridView();
    }

    protected void grdProviders_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        var ddl = grdProviders.Rows[e.RowIndex].FindControl("drpApproved") as DropDownList;

        sc.Open();

        SqlCommand cmd = new SqlCommand("UPDATE [RewardProvider] SET Approved = @approved WHERE ProviderEmail = @email", sc);
        cmd.Parameters.AddWithValue("@approved", ddl.SelectedIndex);
        cmd.Parameters.AddWithValue("@email", (grdProviders.Rows[e.RowIndex].FindControl("lblProviderEmail") as Label).Text.ToString());

        cmd.ExecuteNonQuery();

        cmd.CommandText = "UPDATE [User] SET EmployedStatus = 1 WHERE Email = @email";
        cmd.ExecuteNonQuery();

        sc.Close();


        Response.Redirect(Request.RawUrl);
    }

   
}