using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SuperAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["index"] = 4;
        try
        {
            if ((int)Session["EmployerID"] < 1)
            {
                Response.Redirect("Default.aspx");
            }

        }
        catch
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
            fillGridView();

    }

    protected void fillGridView()
    {
        try
        {

            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

            sc.Open();

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT * FROM [User] WHERE Admin = 1 AND EmployedStatus = 0;", sc);
            del.ExecuteNonQuery();

            grdUsers.DataSource = del.ExecuteReader();
            grdUsers.DataBind();
            sc.Close();

        }
        catch
        {

        }
    }

    protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdUsers.EditIndex = -1;
        fillGridView();
    }

    protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int num = Convert.ToInt32(grdUsers.DataKeys[e.RowIndex].Value.ToString());
        Console.WriteLine();
        try
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

            sc.Open();
            //Declare the query string.

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("DELETE" +
                " FROM [Password] WHERE UserID = @userID;", sc);
            del.Parameters.AddWithValue("@userID", Convert.ToInt32(grdUsers.DataKeys[e.RowIndex].Value.ToString()));
            del.ExecuteNonQuery();
            del.CommandText = "DELETE FROM [User] WHERE UserID=@userID";
            del.ExecuteNonQuery();
            sc.Close();
            fillGridView();
        }
        catch
        {

        }
    }

    protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdUsers.EditIndex = e.NewEditIndex;
        fillGridView();
    }

    protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Boolean textError = true;
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        var ddlEmployed = grdUsers.Rows[e.RowIndex].FindControl("drpStatus") as DropDownList;

        sc.Open();

        SqlCommand cmd = new SqlCommand("UPDATE [User] SET EmployedStatus = @employedStatus, Admin = 1, LastUpdatedBy = 'SuperAdmin' WHERE Email = @email", sc);
        cmd.Parameters.AddWithValue("@employedStatus", ddlEmployed.SelectedIndex);
        cmd.Parameters.AddWithValue("@email", (grdUsers.Rows[e.RowIndex].FindControl("txtgvEmail") as TextBox).Text.ToString());

        cmd.ExecuteNonQuery();

        cmd.CommandText = "SELECT EmployerID FROM [User] WHERE Email = @email";
        int employerID = (int)cmd.ExecuteScalar();

        cmd.CommandText = "UPDATE Employer SET Approved = 1 WHERE EmployerID = @employerID";
        cmd.Parameters.AddWithValue("employerID", employerID);

        cmd.ExecuteNonQuery();

        sc.Close();


        Response.Redirect(Request.RawUrl);


    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}