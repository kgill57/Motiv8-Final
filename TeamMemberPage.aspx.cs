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
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

public partial class TeamMemberPage : System.Web.UI.Page
{
    //Creates sql commands to be re-used when different sorting methods are selected
    SqlCommand read;
    SqlCommand scaler;
    SqlConnection con;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");

            loadProfilePicture();
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

        connectDB();
        read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] WHERE EmployerID = "+ Convert.ToString((int)Session["EmployerID"]) +" AND Private = 0 ORDER BY [TransID] DESC", con);
        scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION] WHERE Private = 0 and EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        loadNewsFeed();



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
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"] + "  $" + ((Decimal)Session["AccountBalance"]).ToString("0.##");

        }
        catch (Exception)
        {

        }
        con.Close();
    }

    protected void loadValueDropDown()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT ValueName FROM CompanyValues", con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        da.Fill(dt);

        ddlCompanyValue.DataSource = dt;
        ddlCompanyValue.DataTextField = "ValueName";
        ddlCompanyValue.DataValueField = "ValueName";
        ddlCompanyValue.DataBind();

    }


    protected void loadNewsFeed()
    {
        con.Open();

        int arraySize = (int)scaler.ExecuteScalar();

        SqlDataReader reader = read.ExecuteReader();

        Post[] transaction = new Post[arraySize];
        int arrayCounter = 0;
        while (reader.Read())
        {
            if (!(bool)reader.GetValue(6))
            {
                transaction[arrayCounter] = new Post(Convert.ToInt32(reader.GetValue(0)), Convert.ToString(reader.GetValue(1)),
                Convert.ToString(reader.GetValue(2)), Convert.ToString(reader.GetValue(3)), Convert.ToDouble(reader.GetValue(4)), Convert.ToDateTime(reader.GetValue(5)), Convert.ToBoolean(reader.GetValue(6)), Convert.ToInt32(reader.GetValue(7)), Convert.ToInt32(reader.GetValue(8)));
                arrayCounter++;
            }


        }
        con.Close();
        Panel1.Controls.Clear();
        Panel[] panelPost = new Panel[arraySize];
        con.Open();

        for (int i = 0; i < arraySize; i++)
        {
            String senderPic;
            String finalSenderPic;
            try
            {
                SqlCommand select = new SqlCommand();
                select.Connection = con;

                select.CommandText = "SELECT [ProfilePicture] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getGiverID();
                senderPic = (String)select.ExecuteScalar();
                finalSenderPic = "Images/" + senderPic;
            }
            catch (Exception)
            {
                finalSenderPic = "Images/admin.png";
            }
            String recieverPic;
            String finalRecieverPic;
            try
            {
                SqlCommand select = new SqlCommand();
                select.Connection = con;

                select.CommandText = "SELECT [ProfilePicture] FROM [dbo].[User] WHERE [UserID] = " + transaction[i].getReceiverID();
                recieverPic = (String)select.ExecuteScalar();
                finalRecieverPic = "Images/" + recieverPic;
            }
            catch (Exception)
            {
                finalRecieverPic = "Images/admin.png";
            }

            panelPost[i] = new Panel();
            panelPost[i].Controls.Add(new LiteralControl("<div class=\"col s12 m8 offset-m2 l6 offset-l3 card-panel grey lighten-5 z-depth-1 row valign-wrapper\"> "));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 10%\"> <img src = \"" + finalSenderPic + "\" alt = \"\" class=\"circle feed responsive-img\"> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 80%\"> <span style = \"display: inline; width:60%; font-size:200%;\" cssclass=\"black-text\"><strong>" + transaction[i].getGiverNickName(transaction[i].getGiverID()) + "</strong> rewarded <strong>" + transaction[i].getReceiverNickName(transaction[i].getReceiverID()) + "</strong> $" + transaction[i].getRewardValue() + "</span > " +
                " <br/><br/> <span style = \"display: inline; width:60%;\">" + transaction[i].getDescription() + "</span> <br/><br/> <span style = \"display: inline; width:60%;\"> Value: " + transaction[i].getValue() + "</span>" +
                " <br/> <span style = \"display: inline; width:60%;\"> Category: " + transaction[i].getCategory() + "</span> </div>"));
            panelPost[i].Controls.Add(new LiteralControl("<div style = \"float: left; width: 10%\"> <img src=\"" + finalRecieverPic + "\" alt=\"#\" class=\"circle feed responsive-img\"> </div>")); //<img src = \"" + getValueImageSrc(transaction[i].getValue()) + "\" alt = \"\" class=\"iconforvalue\" width = \"80%\">
            panelPost[i].Controls.Add(new LiteralControl("</div>"));

            Panel1.Controls.Add(panelPost[i]);

        }

        con.Close();
    }
    protected void btnGiveReward_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        Popup.Visible = true;
        Popup.Enabled = true;
        loadValueDropDown();
        fillSearchList();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        closePopup();


    }
    //protected void giverAndReceiver_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (giverAndReceiver.SelectedIndex == 0)
    //    {
    //        read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] ORDER BY [TransID] DESC", con);
    //        scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION]", con);
    //        loadNewsFeed();
    //    }
    //    else if (giverAndReceiver.SelectedIndex == 1)
    //    {
    //        read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] WHERE GiverID=" + (int)Session["UserID"] + " OR ReceiverID=" + (int)Session["UserID"] + " ORDER BY [TransID] DESC", con);
    //        scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION] WHERE GiverID=" + (int)Session["UserID"] + " OR ReceiverID=" + (int)Session["UserID"], con);
    //        loadNewsFeed();

    //    }
    //    else if (giverAndReceiver.SelectedIndex == 2)
    //    {
    //        read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] WHERE GiverID=" + (int)Session["UserID"] + " ORDER BY [TransID] DESC", con);
    //        scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION] WHERE GiverID=" + (int)Session["UserID"], con);
    //        loadNewsFeed();
    //    }
    //    else if (giverAndReceiver.SelectedIndex == 3)
    //    {
    //        read = new SqlCommand("SELECT * FROM [dbo].[TRANSACTION] WHERE ReceiverID=" + (int)Session["UserID"] + " ORDER BY [TransID] DESC", con);
    //        scaler = new SqlCommand("SELECT COUNT(TransID) FROM [dbo].[TRANSACTION] WHERE ReceiverID=" + (int)Session["UserID"], con);
    //        loadNewsFeed();
    //    }

    //}
    protected void closePopup()
    {
        Popup.Visible = false;
        Popup.Enabled = false;
        lblResult.Text = "";
    }
    protected bool validEntry()
    {
        if (lbResults.SelectedIndex == -1)
        {
            lblResult.Text = "Please Select A Reciever";
            return false;
        }
        else if (txtDescription.Text == "")
        {
            lblResult.Text = "Please Add A Description";
            return false;
        }
        return true;
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {

        if (validEntry())
        {


            Post post = new Post();
            post.setValue(ddlCompanyValue.SelectedValue);
            post.setCategory(ddlCategory.SelectedValue);
            post.setDescription(txtDescription.Text);
            post.setRewardValue(Convert.ToDouble(ddlRewardValue.SelectedValue));
            post.setPostDate(DateTime.Now);
            post.setGiverID((int)Session["UserID"]);

            if (Convert.ToByte(chkPrivate.Checked) == 0)
            {
                post.setIsPrivate(false);
            }
            else if (Convert.ToByte(chkPrivate.Checked) == 1)
            {
                post.setIsPrivate(true);
            }

            try
            {
                System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

                sc.Open();

                System.Data.SqlClient.SqlCommand cmdInsert = new System.Data.SqlClient.SqlCommand();
                cmdInsert.Connection = sc;


                if (checkTransactionDate(post.getGiverID()) == true)
                {

                    cmdInsert.CommandText = "INSERT INTO [dbo].[Transaction] (CompanyValue, Category, Description, RewardValue, TransactionDate,"
                        + " Private, GiverID, ReceiverID, EmployerID) VALUES (@CompanyValue, @Category, @Description, @RewardValue, @TransactionDate, @Private," +
                        " @GiverID, @ReceiverID," + Convert.ToString((int)Session["EmployerID"]) + ")";
                    cmdInsert.Parameters.AddWithValue("@CompanyValue", post.getValue());
                    cmdInsert.Parameters.AddWithValue("@Category", post.getCategory());
                    cmdInsert.Parameters.AddWithValue("@Description", post.getDescription());
                    cmdInsert.Parameters.AddWithValue("@RewardValue", post.getRewardValue());
                    cmdInsert.Parameters.AddWithValue("@TransactionDate", post.getPostDate());
                    cmdInsert.Parameters.AddWithValue("@Private", post.getIsPrivate());
                    cmdInsert.Parameters.AddWithValue("@GiverID", (int)Session["UserID"]);
                    cmdInsert.Parameters.AddWithValue("@ReceiverID", getRecieverID(lbResults.SelectedValue));

                    cmdInsert.ExecuteNonQuery();

                    System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = new System.Web.UI.HtmlControls.HtmlGenericControl();
                    NewDiv.Attributes["class"] = "dialog";
                    NewDiv.ID = "dialog";
                    NewDiv.Attributes["title"] = "Reward Sent!";
                    this.Controls.Add(NewDiv);

                    closePopup();
                    loadNewsFeed();

                    sc.Close();

                    try
                    {
                        sendNotification();
                        System.Data.SqlClient.SqlDataReader readerEmail;
                        SqlConnection checkemail = new SqlConnection();
                        checkemail.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
                        checkemail.Open();

                        SqlCommand reademail = new SqlCommand("SELECT TotalBalance FROM Employer WHERE CompanyName='ElkLogistics'"
                                , checkemail);
                        readerEmail = reademail.ExecuteReader();

                        Decimal totalBalance = 0;

                        while (readerEmail.Read())
                        {
                            totalBalance = readerEmail.GetDecimal(0);
                        }
                        checkemail.Close();

                        if (totalBalance < 500)
                        {
                            var fromAddress = new MailAddress("sdbasketball96@aol.com", "Elk Logistics");
                            var toAddress = new MailAddress("bennskych@gmail.com", "Administrator");
                            const string fromPassword = "Daisydoo#1pet";
                            const string subject = "Reward balance is below 500 dollars";
                            const string body = "Dear Administrator, It seems that"
                                + " the company account balance is below 500 dollars. Please consider adding additional"
                                + " money to the account some time today.";

                            var smtp = new SmtpClient
                            {
                                Host = "smtp.aol.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                            };
                            using (var message = new MailMessage(fromAddress, toAddress)
                            {
                                Subject = subject,
                                Body = body
                            })
                            {
                                smtp.Send(message);
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }

            catch
            {
                lblResult.Text = "Please Fill Out The Whole Form";
            }
        }

    }
    public void sendNotification()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT Email FROM [User] WHERE Username=@username", con);
        cmd.Parameters.AddWithValue("@username", this.Request.Form["txtTags"]);

        var fromAddress = new MailAddress("sdbasketball96@aol.com", "Elk Logistics Rewards");
        var toAddress = new MailAddress((String)cmd.ExecuteScalar(), "Test");
        const string fromPassword = "Daisydoo#1pet";
        const string subject = "You Received a Reward From a Co-Worker!";
        const string body = "Dear Team Member, You have received a reward from a fellow Team member. Login to find out who rewarded you!";


        var smtp = new SmtpClient
        {
            Host = "smtp.aol.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }
    public int getRecieverID(String nickName)
    {
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        sc.Open();

        System.Data.SqlClient.SqlCommand cmdInsert = new System.Data.SqlClient.SqlCommand();
        cmdInsert.Connection = sc;
        cmdInsert.CommandText = "SELECT UserID FROM [User] WHERE NickName = @NickName";

        cmdInsert.Parameters.AddWithValue("@NickName", nickName);

        int userID = (int)cmdInsert.ExecuteScalar();

        sc.Close();
        return userID;
    }
    public Boolean checkTransactionDate(int giverID)
    {

        Boolean valid = true;
        System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        sc.Open();

        System.Data.SqlClient.SqlCommand cmdInsert = new System.Data.SqlClient.SqlCommand();
        cmdInsert.Connection = sc;

        cmdInsert.CommandText = "SELECT TransactionDate FROM [Transaction] WHERE TransID = (SELECT MAX(TransID) FROM [Transaction] WHERE GiverID=@giverID)";
        cmdInsert.Parameters.AddWithValue("@giverID", giverID);
        DateTime transDate = Convert.ToDateTime(cmdInsert.ExecuteScalar());

        System.Diagnostics.Debug.WriteLine(transDate);
        DateTime today = DateTime.Today.Date;
        if (transDate.Date == today)
        {
            lblResult.Text = "Cannot make 2 transactions in one day.";
            valid = false;
        }



        sc.Close();


        return valid;
    }
    protected void connectDB()
    {
        con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
    }

    public static string getValueImageSrc(String value)
    {
        String imgSrc;
        //if statemets to select which value and image to show 
        if (value.Equals("Health, Well Being and Safety Of Team Members"))
        {
            imgSrc = "icons/grouphealth.png";
        }
        else if (value.Equals("Community Involvement"))
        {
            imgSrc = "icons/communityinv.png";
        }
        else if (value.Equals("Customer Service"))
        {
            imgSrc = "icons/customerservice.png";
        }
        else if (value.Equals("Retaining/Attracting New Customers"))
        {
            imgSrc = "icons/addcustomer.png";
        }
        else if (value.Equals("Leadership"))
        {
            imgSrc = "icons/leadership.png";
        }
        else if (value.Equals("Process Improvement Initiatives"))
        {
            imgSrc = "icons/process.png";
        }
        else if (value.Equals("Education"))
        {
            imgSrc = "icons/education.png";
        }
        else
        {
            imgSrc = "icons/teambuilding.png";
        }

        return imgSrc;
    }

    protected void fillSearchList()
    {
        //clear listbox
        lbResults.Items.Clear();

        //connect bd
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();
        String searchString = "SELECT[NickName] FROM[dbo].[User] WHERE [Admin] = 0 AND [ProviderID] IS NULL AND [EmployedStatus] = 1 AND [UserID] != " + Convert.ToString((int)Session["UserID"]) + " AND [SuperAdmin] = 0 AND [NickName] LIKE @Search AND EmployerID = " + Convert.ToString((int)Session["EmployerID"]);
        SqlDataAdapter search = new SqlDataAdapter(searchString, con);
        search.SelectCommand.Parameters.AddWithValue("@Search", txtSearch.Text + "%");
        DataTable dt = new DataTable();
        search.Fill(dt);

        lbResults.DataSource = dt;
        lbResults.DataValueField = "NickName";
        lbResults.DataTextField = "NickName";
        lbResults.DataBind();
        con.Close();
    }
    public static void SetSession(string value)
    {
        HttpContext.Current.Session["Value"] = value;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillSearchList();
    }

}

