using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class UserOptions : System.Web.UI.Page
{
    private DataTable UserDataTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
            if (!IsPostBack)
                fillGridView();

            loadProfilePicture();

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
        //lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];

        
        con.Close();
    }

    protected void btnInsertUser_Click(object sender, EventArgs e)
    {
        Popup.Visible = true;
        Popup.Enabled = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserOptions.aspx");
    }
    protected void btnSubmitUser_Click(object sender, EventArgs e)
    {
        // Instantiate SQL objects, set up a SQL connection
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();



        SqlCommand select = new SqlCommand();
        select.Connection = con;

        // Get the username the admin wants to insert into the database
        select.CommandText = "SELECT Email FROM [dbo].[User] WHERE Email = @Email";

        select.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
        select.Parameters["@Email"].Value = txtEmail.Text;

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
                insertString += "@LName, @Email, @nickName, NULL, 0, 0, 0, " + (int)Session["UserID"] + ", NULL, @EmployerID, @AccountBalance, 1, '" + (String)Session["LName"] + "', '2018-01-01')";

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
                select.Parameters["@EmployerID"].Value = (int)Session["EmployerID"];

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
                txtEmail.Text = "";
                txtNickName.Text = "";
                lblError.Text = "";
                Popup.Visible = false;
                Popup.Enabled = false;
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
        fillGridView();
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

            System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("SELECT UserID, FName, LName, MI, Email, " +
                "NickName, Admin, EmployedStatus, AccountBalance FROM [User] WHERE EmployerID = " +(int)Session["EmployerID"] +" AND UserID != " +(int)Session["UserID"]+ "AND SuperAdmin != 1 AND RewardProvider != 1", sc);
            del.ExecuteNonQuery();

            grdUsers.DataSource = del.ExecuteReader();
            grdUsers.DataBind();
            sc.Close();

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

    protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Boolean textError = true;
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        //Check if the project name Text box is empty
        if (String.IsNullOrEmpty((grdUsers.Rows[e.RowIndex].FindControl("txtgvFName") as TextBox).Text.ToString()))
        {
            //projectNameError.Visible = true;
            //projectNameError.Text = "The project name cannot be empty";
            textError = false;
        }

        //Check if the Project Description Text box is empty
        if (String.IsNullOrEmpty((grdUsers.Rows[e.RowIndex].FindControl("txtgvLName") as TextBox).Text.ToString()))
        {
            //projectDescriptionErrror.Visible = true;
            //projectDescriptionErrror.Text = "Field cannot be empty";
            textError = false;
        }

        if (String.IsNullOrEmpty((grdUsers.Rows[e.RowIndex].FindControl("txtgvEmail") as TextBox).Text.ToString()))
        {
            //projectDescriptionErrror.Visible = true;
            //projectDescriptionErrror.Text = "Field cannot be empty";
            textError = false;
        }

        if (String.IsNullOrEmpty((grdUsers.Rows[e.RowIndex].FindControl("txtgvNickName") as TextBox).Text.ToString()))
        {
            //projectDescriptionErrror.Visible = true;
            //projectDescriptionErrror.Text = "Field cannot be empty";
            textError = false;
        }

        if (textError)
        {
            // Declare var variables to store the row currently being edited
            var ddl = grdUsers.Rows[e.RowIndex].FindControl("ddlgvAdmin") as DropDownList;
            var ddlEmployed = grdUsers.Rows[e.RowIndex].FindControl("drpStatus") as DropDownList;
            var newMI = grdUsers.Rows[e.RowIndex].FindControl("txtgvMI") as TextBox;

            sc.Open();

            // Declare the query string
            try
            {
                System.Data.SqlClient.SqlCommand del = new System.Data.SqlClient.SqlCommand("UPDATE [User] SET FName=@newFName, " +
                    "LName=@newLName, MI=@newMI, Email=@newEmail, NickName=@newNickName, Admin=@newAdmin, EmployedStatus=@employedStatus WHERE UserID=@userID", sc);
                del.Parameters.AddWithValue("@newFName", (char.ToUpper((grdUsers.Rows[e.RowIndex].FindControl("txtgvFName") as TextBox).Text[0])
                    + (grdUsers.Rows[e.RowIndex].FindControl("txtgvFName") as TextBox).Text.Substring(1)));
                del.Parameters.AddWithValue("@newLName", (char.ToUpper((grdUsers.Rows[e.RowIndex].FindControl("txtgvLName") as TextBox).Text[0])
                    + (grdUsers.Rows[e.RowIndex].FindControl("txtgvLName") as TextBox).Text.Substring(1)));

                // Check if gridview column for MI is empty or not
                if (String.IsNullOrWhiteSpace(newMI.Text) == true)
                {
                    del.Parameters.AddWithValue("@newMI", DBNull.Value);
                }

                else if (String.IsNullOrWhiteSpace(newMI.Text) == false)
                {
                    del.Parameters.AddWithValue("@newMI", (char.ToUpper((grdUsers.Rows[e.RowIndex].FindControl("txtgvMI") as TextBox).Text[0])));
                }

                del.Parameters.AddWithValue("@newEmail", (grdUsers.Rows[e.RowIndex].FindControl("txtgvEmail") as TextBox).Text.ToString());
                del.Parameters.AddWithValue("@newNickName", (grdUsers.Rows[e.RowIndex].FindControl("txtgvNickName") as TextBox).Text.ToString());
                del.Parameters.AddWithValue("@newAdmin", ddl.SelectedValue);
                del.Parameters.AddWithValue("@employedStatus", ddlEmployed.SelectedValue);
                del.Parameters.AddWithValue("@userID", Convert.ToInt32(grdUsers.DataKeys[e.RowIndex].Value.ToString()));
                del.ExecuteNonQuery();
                sc.Close();
                grdUsers.EditIndex = -1;
                fillGridView();
            }

            catch
            {

            }

        }
    }

    protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdUsers.EditIndex = -1;
        fillGridView();
    }

    public void PullData()
    {

        string connString = "Server=LOCALHOST;Database=Lab4;Trusted_Connection=Yes;";
        string query = "select * from [dbo].[User]";

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();

        // create data adapter
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // this will query your database and return the result to your datatable
        da.Fill(UserDataTable);
        conn.Close();
        da.Dispose();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        PullData();
        WriteDataTableToCSV(UserDataTable, "UserTableExport");
    }

    /// <summary>
    /// Creates a response as a CSV with a header row and results of a data table 
    /// </summary>
    /// <param name="dt">DataTable which holds the data</param>
    /// <param name="fileName">File name for the outputted file</param>
    public static void WriteDataTableToCSV(DataTable dt, string fileName)
    {
        WriteOutCSVResponseHeaders(fileName);
        WriteOutDataTable(dt);
        HttpContext.Current.Response.End();
    }


    /// <summary>
    /// Writes out the response headers needed for outputting a CSV file.
    /// </summary>
    /// <param name="fileName">File name for the outputted file</param>
    public static void WriteOutCSVResponseHeaders(string fileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}-{1}.csv", fileName, DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")));
        HttpContext.Current.Response.AddHeader("Pragma", "public");
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
    }


    /// <summary>
    /// Writes out the header row and data rows from a data table.
    /// </summary>
    /// <param name="dt">DataTable which holds the data</param>
    public static void WriteOutDataTable(DataTable dt)
    {
        WriteOutHeaderRow(dt, dt.Columns.Count);
        WriteOutDataRows(dt, dt.Columns.Count, dt.Rows.Count);
    }

    /// <summary>
    /// Writes the header row from a datatable as Http Response
    /// </summary>
    /// <param name="dt">DataTable which holds the data</param>
    /// <param name="colCount">Number of columns</param>
    private static void WriteOutHeaderRow(DataTable dt, int colCount)
    {
        string CSVHeaderRow = string.Empty;
        for (int col = 0; col <= colCount - 1; col++)
        {
            CSVHeaderRow = string.Format("{0}\"{1}\",", CSVHeaderRow, dt.Columns[col].ColumnName);
        }
        WriteRow(CSVHeaderRow);
    }

    /// <summary>
    /// Writes the data rows of a datatable as Http Responses
    /// </summary>
    /// <param name="dt">DataTable which holds the data</param>
    /// <param name="colCount">Number of columns</param>
    /// <param name="rowCount">Number of columns</param>
    private static void WriteOutDataRows(DataTable dt, int colCount, int rowCount)
    {
        string CSVDataRow = string.Empty;
        for (int row = 0; row <= rowCount - 1; row++)
        {
            var dataRow = dt.Rows[row];
            CSVDataRow = string.Empty;
            for (int col = 0; col <= colCount - 1; col++)
            {
                CSVDataRow = string.Format("{0}\"{1}\",", CSVDataRow, dataRow[col]);
            }
            WriteRow(CSVDataRow);
        }
    }

    /// <summary>
    /// Write out a row as an Http Response.
    /// </summary>
    /// <param name="row">The data row to write out</param>
    private static void WriteRow(string row)
    {
        HttpContext.Current.Response.Write(row.TrimEnd(','));
        HttpContext.Current.Response.Write(Environment.NewLine);
    }

}