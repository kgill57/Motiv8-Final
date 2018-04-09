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
using System.Web.UI.DataVisualization.Charting;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using MailScheduler;

public partial class AnalyticsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }



        loadProfilePicture();

        try
        {

        }
        catch (Exception)
        {

        }

        panel1.Visible = true;
        panel2.Visible = false;
        panel3.Visible = false;


    }

    protected void loadProfilePicture()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand balance = new SqlCommand("SELECT TotalBalance FROM Employer WHERE EmployerID =" + Convert.ToString((int)Session["EmployerID"]), con);
        double totalBalance = Convert.ToDouble(balance.ExecuteScalar());

        lblBalance.Text = totalBalance.ToString("$#.00");

        try
        {

            SqlCommand select = new SqlCommand();
            select.Connection = con;

            select.CommandText = "SELECT ProfilePicture FROM [dbo].[User] WHERE UserID =" + Convert.ToString((int)Session["UserID"]);
            string currentPicture = (String)select.ExecuteScalar();

            profilePicture.ImageUrl = "~/Images/" + currentPicture;
            lblUser.Text = (String)Session["FName"] + " " + (String)Session["LName"];

        }
        catch (Exception)
        {

        }
        con.Close();
    }


    protected void btnDownload_Click(object sender, EventArgs e)
    {
        
        if(drpReportOptions.SelectedIndex == 0)
        {
            GetExcelFile GF = new GetExcelFile();
            GF.getData(0);
        }
        else if(drpReportOptions.SelectedIndex == 1)
        {
            GetExcelFile GF = new GetExcelFile();
            GF.getData(1);
        }
        else
        {
            GetExcelFile GF = new GetExcelFile();
            GF.getData(2);
        }

        SendMail sm = new SendMail();
        sm.SendEmail();

    }





    protected void tableauDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(tableauDropDown.SelectedIndex == 0)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }
        else if(tableauDropDown.SelectedIndex == 1)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
        }
        else if(tableauDropDown.SelectedIndex == 2)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
        }
    }
}