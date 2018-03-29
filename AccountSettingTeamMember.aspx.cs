using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
public partial class AccountSettingTeamMember : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        // Show user's name and balance in sidebar
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");
        }

        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

        loadProfilePicture();

    }

    protected void loadProfilePicture()
    {
        con.Open();

        // Load user's current profile picture in sidebar
        try
        {

            SqlCommand select = new SqlCommand();
            select.Connection = con;

            select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
            string currentPicture = (String)select.ExecuteScalar();

            profilePicture.ImageUrl = "~/Images/" + currentPicture;
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");

        }

        catch (Exception)
        {

        }

        con.Close();
    }



    protected void btnChangePass_Click(object sender, EventArgs e)
    {
        // Check if current password is real password
        String currentPass = txtCurrentPass.Text;

        if (txtNewPass1.Text == txtCurrentPass.Text || txtNewPass2.Text == txtCurrentPass.Text)
        {
            lblResult.Text = "Your new password cannot be the same as your current password.";
            return;
        }

        con.Open();
        SqlCommand select = new SqlCommand();
        select.Connection = con;

        // Get the hash for the current user's password
        select.CommandText = "SELECT PasswordHash FROM [dbo].[Password] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);

        String currentHash = (String)select.ExecuteScalar();

        bool correctHash = SimpleHash.VerifyHash(currentPass, "MD5", currentHash);

        // Check if current password and new password TextBoxes are filled out correctly
        if (correctHash)
        {
            if (String.IsNullOrWhiteSpace(txtNewPass1.Text) == true)
            {
                lblResult.Text = "You must enter a new password.";
            }

            else if (txtNewPass1.Text == txtNewPass2.Text)
            {
                String newPassHash = SimpleHash.ComputeHash(txtNewPass1.Text, "MD5", null);
                select.CommandText = "UPDATE [dbo].[Password] SET [PasswordHash] = @PasswordHash WHERE [UserID] =" + Convert.ToString((int)Session["UserID"]);
                select.Parameters.AddWithValue("@PasswordHash", newPassHash);
                select.ExecuteNonQuery();
                lblResult.Text = "New password confirmed!";
            }

            else
            {
                lblResult.Text = "New passwords do not match.";
            }

        }
        else
        {
            lblResult.Text = "Incorrect password.";
        }

        con.Close();

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // Get the name of the file
        string fileName = Path.GetFileName(UploadPicture.PostedFile.FileName);

        // Check if a picture was chosen
        if (String.IsNullOrWhiteSpace(fileName) == true)
        {
            lblResult.Text = "You must choose a picture to upload.";
            return;
        }

        // Save file to server map
        UploadPicture.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);


        con.Open();
        SqlCommand upload = new SqlCommand();
        upload.Connection = con;

        // Change the user's profile picture
        upload.CommandText = "UPDATE [dbo].[user] SET [ProfilePicture] = @ProfilePicture WHERE [UserID] =" + Convert.ToString((int)Session["UserID"]);
        upload.Parameters.AddWithValue("@ProfilePicture", fileName);
        upload.ExecuteNonQuery();
        con.Close();

        lblResult.Text = "Picture uploaded";
        loadProfilePicture();

    }
}