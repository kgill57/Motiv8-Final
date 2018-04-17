using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CompanyValues : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
            loadProfilePicture();

            // On initial page load, fill the gridview with all users in the database
            if (!IsPostBack)
                loadGridView();

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

    protected void loadGridView()
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

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT ValueID, ValueName, LastUpdated, LastUpdatedBy FROM [CompanyValues] WHERE EmployerID = " + (int)Session["EmployerID"], sc);
            del.ExecuteNonQuery();

            valueGrid.DataSource = del.ExecuteReader();
            valueGrid.DataBind();
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

    protected void btnInsertValue_Click(object sender, EventArgs e)
    {
        popup.Visible = true;
        popup.Enabled = true;
    }

    protected void btnAddValue_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[CompanyValues] ([ValueName] ,[EmployerID] ,[LastUpdated] ,[LastUpdatedBy]) VALUES (" +
            "@ValueName, @EmployerID, @LastUpdated, @LastUpdatedBy)", con);

        cmd.Parameters.AddWithValue("@ValueName", txtNewValueName.Text);
        cmd.Parameters.AddWithValue("@EmployerID", (int)Session["EmployerID"]);
        cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@LastUpdatedBy", Session["UserName"]);

        cmd.ExecuteNonQuery();

        lblResult.Text = "Value Added!";

        con.Close();

        loadGridView();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        popup.Visible = false;
    }

    protected void valueGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        valueGrid.EditIndex = -1;
        loadGridView();
    }

    protected void valueGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        valueGrid.EditIndex = e.NewEditIndex;
        loadGridView();
    }

    protected void valueGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        sc.Open();

        SqlCommand cmd = new SqlCommand("UPDATE CompanyValues SET ValueName=@newValueName, LastUpdated=@newLastUpdated, LastUpdatedBy=@newLastUpdatedBy WHERE ValueID=@valueID", sc);

        cmd.Parameters.AddWithValue("@newValueName", (valueGrid.Rows[e.RowIndex].FindControl("txtValueName") as TextBox).Text.ToString());
        cmd.Parameters.AddWithValue("@newLastUpdated", DateTime.Now.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@newLastUpdatedBy", Session["UserName"]);
        cmd.Parameters.AddWithValue("@valueID", (valueGrid.Rows[e.RowIndex].FindControl("lblValueID") as Label).Text.ToString());

        cmd.ExecuteNonQuery();

        sc.Close();
        valueGrid.EditIndex = -1;
        loadGridView();
    }
}