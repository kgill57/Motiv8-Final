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

        cmd.CommandText = "INSERT INTO RewardProvider (ProviderName, ProviderEmail, ProviderPicture, Approved, EmployerID) VALUES (@providerName, @providerEmail, NULL, 0, @employerID)";
        cmd.Parameters.AddWithValue("@providerName", txtProvCompany.Text);
        cmd.Parameters.AddWithValue("@providerEmail", txtProvEmail.Text);
        cmd.Parameters.AddWithValue("@employerID", selectEmployer(Convert.ToString(drpEmployers.SelectedValue)));

        cmd.ExecuteNonQuery();
        

        cmd.CommandText = "SELECT MAX(ProviderID) FROM RewardProvider";
        int providerID = (int)cmd.ExecuteScalar();

        cmd.CommandText = "INSERT INTO [USER] (FName, LName, Email, NickName, Admin, RewardProvider, SuperAdmin, ProviderID, EmployerID, AccountBalance, " +
                    "EmployedStatus, LastUpdatedBy, LastUpdated) VALUES (@FName, @LName, @Email, @NickName, 0, 1, 0, @ProviderID, 1, " +
                    "0, 0, 'System', @LastUpdated)";
        cmd.Parameters.AddWithValue("@FName", txtProvFName.Text);
        cmd.Parameters.AddWithValue("@LName", txtProvLName.Text);
        cmd.Parameters.AddWithValue("@Email", txtProvEmail.Text);
        cmd.Parameters.AddWithValue("@NickName", txtProvNickName.Text);
        cmd.Parameters.AddWithValue("@ProviderID", providerID);
        cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now.ToString("yyyy-MM-dd"));

        cmd.ExecuteNonQuery();

        // Create a password and password hash for the new user
        string password = "password";

        string passwordHashNew =
                   SimpleHash.ComputeHash(password, "MD5", null);
        cmd.CommandText = "SELECT [UserID] FROM [USER] WHERE [Email] = @Email";
        int userID = (int)cmd.ExecuteScalar();
        cmd.CommandText = "INSERT INTO[dbo].[Password] Values (" + userID + ", '" + passwordHashNew + "')";
        cmd.ExecuteNonQuery();

        con.Close();

        lblResult.Text = "Application Sent!";
    }

    protected int selectEmployer(string employerName)
    {
        //will determine the employerID based on the dropdown selected value
        SqlCommand cmd = new SqlCommand("SELECT EmployerID FROM Employer WHERE CompanyName = @companyName");
        cmd.Parameters.AddWithValue("@companyName", employerName);
        cmd.Connection = con;

        int employerID = (int)cmd.ExecuteScalar();

        return employerID;
    }

    protected void loadDropDown()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT CompanyName FROM Employer WHERE Approved = 1", con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        da.Fill(dt);

        drpEmployers.DataSource = dt;
        drpEmployers.DataTextField = "CompanyName";
        drpEmployers.DataValueField = "CompanyName";
        drpEmployers.DataBind();
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


    protected void btnAdminApp_Click(object sender, EventArgs e)
    {
        EmployerPanel.Visible = true;
        EmployerPanel.Enabled = true;
        ProviderPanel.Visible = false;
        ProviderPanel.Enabled = false;
    }

    protected void btnSubmitUser_Click(object sender, EventArgs e)
    {
        // Instantiate SQL objects, set up a SQL connection
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Employer(CompanyName, TotalBalance, Approved) VALUES (@companyName, 0, 0)", con);
        cmd.Parameters.AddWithValue("@companyName", txtNewCompanyName.Text);

        cmd.ExecuteNonQuery();

        cmd.CommandText = "SELECT MAX(EmployerID) FROM Employer";
        cmd.Connection = con;

        int EmployerID = (int)cmd.ExecuteScalar();
        

        SqlCommand select = new SqlCommand();
        select.Connection = con;

        // Get the username the admin wants to insert into the database
        select.CommandText = "SELECT Email FROM [dbo].[User] WHERE Email = @Email";

        select.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
        select.Parameters["@Email"].Value = txtNewEmail.Text;

        // Check if the desired username is already in the database
        String existingUserName = (String)select.ExecuteScalar();
        if (existingUserName == null)
        {
            try
            {

                String insertString;

                // Insert the new user into the database
                insertString = "INSERT INTO [dbo].[User] VALUES(@FName, Null,";

                // SQL insert statement
                insertString += "@LName, @Email, @nickName, 'admin.png', 1, 0, 0, NULL, NULL, @EmployerID, @AccountBalance, 0, '" + (String)Session["LName"] + "', '2018-01-01')";

                select.CommandText = insertString;

                //find the NickName
                select.Parameters.AddWithValue("@nickName", txtNickName.Text);

                // Make the first letter in the First Name TextBox uppercase
                select.Parameters.Add(new SqlParameter("@FName", SqlDbType.VarChar));
                select.Parameters["@FName"].Value = char.ToUpper(txtFName.Text[0]) + txtFName.Text.Substring(1);


                // Make the first letter in the Last Name TextBox uppercase
                select.Parameters.Add(new SqlParameter("@LName", SqlDbType.VarChar));
                select.Parameters["@LName"].Value = char.ToUpper(txtLName.Text[0]) + txtLName.Text.Substring(1);


                // Set the EmployerID equal to the selected index of the corresponding drop down list + 1 to avoid indexing errors
                select.Parameters.Add(new SqlParameter("@EmployerID", SqlDbType.Int));
                select.Parameters["@EmployerID"].Value = EmployerID;

                // Set the new user's account balance equal to $0
                select.Parameters.Add(new SqlParameter("@AccountBalance", SqlDbType.Money));
                select.Parameters["@AccountBalance"].Value = 0;


                select.ExecuteNonQuery();

                // Create a password and password hash for the new user
                string password = "password";

                string passwordHashNew =
                           SimpleHash.ComputeHash(password, "MD5", null);
                select.CommandText = "SELECT [UserID] FROM [USER] WHERE [Email] = @Email";
                int userID = (int)select.ExecuteScalar();
                select.CommandText = "INSERT INTO[dbo].[Password] Values (" + userID + ", '" + passwordHashNew + "')";
                select.ExecuteNonQuery();

                txtFName.Text = "";
                txtLName.Text = "";
                //txtEmail.Text = "";
                lblError.Text = "";
                txtNewCompanyName.Text = "";
                //ddlAccountType.SelectedIndex = 0;
                //ddlCompanies.SelectedIndex = 0;
                lblError.Text = "Application Sent!";
            }
            catch (Exception)
            {
                lblError.Text = "Please fill out the whole form";
            }

        }

        // Display an error message if the username already exists within the database
        else
        {
            lblError.Text = "This username is already taken";
        }

        // Close the SQL connection and update the gridview
        con.Close();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EmployerPanel.Visible = false;
        EmployerPanel.Enabled = false;
    }

    protected void btnProviderApp_Click(object sender, EventArgs e)
    {
        ProviderPanel.Visible = true;
        ProviderPanel.Enabled = true;
        EmployerPanel.Visible = false;
        EmployerPanel.Enabled = false;
        loadDropDown();
    }

    protected void btnCancelApp_Click(object sender, EventArgs e)
    {
        ProviderPanel.Visible = false;
        ProviderPanel.Enabled = false;
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}