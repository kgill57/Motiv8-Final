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

public partial class rpSettings : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["index"] = 3;
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        try
        {
            if (!IsPostBack)
            {
                loadProfilePicture();
            }
        }
        catch(Exception)
        {
            Response.Redirect("Default.aspx");
        }
        
        
    }

    protected void btnChangePass_Click(object sender, EventArgs e)
    {
        // Check if current password is real password
        String currentPass = txtCurrentPass.Text;

        if (txtNewPass.Text == txtCurrentPass.Text || txtConfirmNewPass.Text == txtCurrentPass.Text)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
            NewDiv.Attributes["class"] = "dialog";
            NewDiv.ID = "dialog";
            NewDiv.Attributes["title"] = "Password Change";
            NewDiv.InnerText = "Your new password cannot be the same as your old password.";
            this.Controls.Add(NewDiv);
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
            if (String.IsNullOrWhiteSpace(txtNewPass.Text) == true)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
                NewDiv.Attributes["class"] = "dialog";
                NewDiv.ID = "dialog";
                NewDiv.Attributes["title"] = "Password Change";
                NewDiv.InnerText = "You must enter a new password.";
                this.Controls.Add(NewDiv);
            }

            else if (txtNewPass.Text == txtConfirmNewPass.Text)
            {
                String newPassHash = SimpleHash.ComputeHash(txtNewPass.Text, "MD5", null);
                select.CommandText = "UPDATE [dbo].[Password] SET [PasswordHash] = @PasswordHash WHERE [UserID] =" + Convert.ToString((int)Session["UserID"]);
                select.Parameters.AddWithValue("@PasswordHash", newPassHash);
                select.ExecuteNonQuery();
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
                NewDiv.Attributes["class"] = "dialog";
                NewDiv.ID = "dialog";
                NewDiv.Attributes["title"] = "Password Change";
                NewDiv.InnerText = "Password successfully changed!";
                this.Controls.Add(NewDiv);
            }

            else
            {
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
                NewDiv.Attributes["class"] = "dialog";
                NewDiv.ID = "dialog";
                NewDiv.Attributes["title"] = "Password Change";
                NewDiv.InnerText = "Passwords do not match.";
                this.Controls.Add(NewDiv);
            }

        }
        else
        {
            System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
            NewDiv.Attributes["class"] = "dialog";
            NewDiv.ID = "dialog";
            NewDiv.Attributes["title"] = "Password Change";
            NewDiv.InnerText = "Incorrect password.";
            this.Controls.Add(NewDiv);
        }

        con.Close();
    }      
    protected int getProviderID()
    {
        con.Close();
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT ProviderID FROM [User] WHERE UserID = " + (int)Session["UserID"];
        cmd.Connection = con;

        int providerID = (int)cmd.ExecuteScalar();

        con.Close();

        return providerID;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        // Get the name of the file
        string fileName = Path.GetFileName(UploadPicture.PostedFile.FileName);

        // Check if a picture was chosen
        if (String.IsNullOrWhiteSpace(fileName) == true)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl PicFailDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
            PicFailDiv.Attributes["class"] = "dialog";
            PicFailDiv.ID = "dialog";
            PicFailDiv.Attributes["title"] = "Picture Change";
            PicFailDiv.InnerText = "You must choose a picture to upload.";
            this.Controls.Add(PicFailDiv);
            return;
        }

        // Save file to server map
        UploadPicture.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName);



        SqlCommand upload = new SqlCommand();
        upload.Connection = con;

        // Change the user's profile picture
        upload.CommandText = "UPDATE [dbo].[RewardProvider] SET [ProviderPicture] = @ProfilePicture WHERE [ProviderID] =" + getProviderID();
        upload.Parameters.AddWithValue("@ProfilePicture", fileName);
        con.Open();
        upload.ExecuteNonQuery();
        con.Close();

        System.Web.UI.HtmlControls.HtmlGenericControl PicDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
        PicDiv.Attributes["class"] = "dialog";
        PicDiv.ID = "dialog";
        PicDiv.Attributes["title"] = "Picture Change";
        PicDiv.InnerText = "Picture successfully uploaded!";
        this.Controls.Add(PicDiv);
        loadProfilePicture();
    }

    protected void loadProfilePicture()
    {
        con.Open();

        
        SqlCommand select = new SqlCommand();
        select.Connection = con;

        select.CommandText = "SELECT ProviderPicture FROM [dbo].[RewardProvider] WHERE ProviderID =" + getProviderID();
        con.Open();
        string currentPicture = (String)select.ExecuteScalar();

        profilePicture.ImageUrl = "~/Images/" + currentPicture;
        lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
       

        con.Close();
    }
}