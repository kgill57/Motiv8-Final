using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["index"] = 2;
        //Load Total Rewards given
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.CommandText = "SELECT SUM([RewardValue]) FROM [dbo].[Transaction]";

        
        lblTotalRewards.Text = String.Format("{0:C0}", (decimal)select.ExecuteScalar());

        con.Close();
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        String email = txtEmail.Text;
        String password = txtPassword.Text;

        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand select = new SqlCommand();
        select.Connection = con;


        select.Parameters.Add(new System.Data.SqlClient.SqlParameter("@email", System.Data.SqlDbType.VarChar));
        select.Parameters["@email"].Value = txtEmail.Text;

        select.CommandText = "SELECT EmployedStatus FROM [User] WHERE Email = @email";


        bool status = Convert.ToBoolean(select.ExecuteScalar());
        if (status == false)
        {
            lblError.Visible = true;
            lblError.Text = "Email does not exist";
            return;
        }

        select.CommandText = "SELECT [PasswordHash] FROM [dbo].[Password] WHERE [UserID] = (SELECT [UserID] FROM [dbo].[User] WHERE [Email] = @email)";

        String hash = (String)select.ExecuteScalar();
        con.Close();

        bool provider = checkProvider();
        con.Open();
        bool admin;
        select.CommandText = "(SELECT [Admin] FROM [dbo].[User] WHERE [Email] = @email)";
        admin = Convert.ToBoolean(select.ExecuteScalar());

        if (provider)
        {
            select.CommandText = "SELECT ProviderID FROM [User] WHERE Email = @email";
            Session["ProviderID"] = (int)select.ExecuteScalar();

            select.CommandText = "SELECT ProviderName FROM [RewardProvider] WHERE ProviderID = " + Convert.ToString((int)Session["ProviderID"]);
            Session["ProviderName"] = (String)(select.ExecuteScalar());
        }

        con.Close();

        bool verify = SimpleHash.VerifyHash(password, "MD5", hash);

        if (verify)
        {
            getUser(txtEmail.Text);

            if (provider)
            {
                Response.Redirect("rpHome.aspx");
            }
            else if (admin)
            {
                Response.Redirect("AdminPage.aspx");
            }
            else
            {
                Response.Redirect("TeamMemberPage.aspx");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Invalid email and/or password.";
        }

    }
    public bool checkProvider()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();
        bool rt = false;
        SqlCommand read = new SqlCommand("SELECT * FROM [dbo].[User] WHERE [ProviderID] IS NOT NULL", con);
        SqlDataReader reader = read.ExecuteReader();
        while (reader.Read())
        {
            if (txtEmail.Text == Convert.ToString(reader.GetValue(4)))
            {
                rt = true;
            }
        }
        con.Close();
        return rt;
    }
    public void getUser(string email)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.Parameters.AddWithValue("@email", email);

        select.CommandText = "SELECT UserID  FROM [User] WHERE Email = @email";
        Session["UserID"] = (int)select.ExecuteScalar();

        select.CommandText = "SELECT FName FROM [User] WHERE Email = @email";
        Session["FName"] = (String)(select.ExecuteScalar());

        try
        {
            select.CommandText = "SELECT MI FROM [User] WHERE Email = @email";
            Session["MI"] = (String)select.ExecuteScalar();
        }
        catch (Exception)
        {
            Session["MI"] = "";
        }


        select.CommandText = "SELECT LName FROM [User] WHERE Email = @email";
        Session["LName"] = (String)(select.ExecuteScalar());

        select.CommandText = "SELECT NickName FROM [User] WHERE Email = @email";
        Session["UserName"] = (String)(select.ExecuteScalar());

        select.CommandText = "SELECT Email FROM [User] WHERE Email = @email";
        Session["Email"] = (String)(select.ExecuteScalar());

        select.CommandText = "SELECT Admin FROM [User] WHERE Email = @email";
        Session["Admin"] = Convert.ToInt32(select.ExecuteScalar());
        Session["index"] = (int)Session["Admin"];

        select.CommandText = "SELECT EmployerID FROM [User] WHERE Email = @email";
        Session["EmployerID"] = (int)(select.ExecuteScalar());

        select.CommandText = "SELECT AccountBalance FROM [User] WHERE Email = @email";
        Session["AccountBalance"] = (Convert.ToDecimal(select.ExecuteScalar()));

        




    }

    protected void btnCreateAdmin_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.CommandText = "SELECT UserName FROM [dbo].[User] WHERE UserName = 'admin'";
        String existingUserName;
        existingUserName = (String)select.ExecuteScalar();
        if (existingUserName == null)
        {
            select.CommandText = "INSERT INTO [dbo].[Employer] VALUES('Elk Logistics', 5000)";
            select.ExecuteNonQuery();

            select.CommandText = "INSERT INTO [dbo].[User] VALUES('Chris', 'J', 'Bennsky', 'Bennskych@gmail.com', 'admin', 'elk-logo.png', 1, NULL, 1, 100, 1, 'Bennsky', '2018-01-01')";
            select.ExecuteNonQuery();

            string password = "password";

            string passwordHashNew =
                       SimpleHash.ComputeHash(password, "MD5", null);

            select.CommandText = "INSERT INTO[dbo].[Password] Values (1, '" + passwordHashNew + "')";
            select.ExecuteNonQuery();
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "This username is already taken";
        }


        con.Close();

    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("/rpApplication.aspx");
    }
}