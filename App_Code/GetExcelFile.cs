using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ClosedXML.Excel;
namespace MailScheduler
{
    public class GetExcelFile
    {
        public void getData(int index)
        {
            DataSet ds = null;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Lab4ConnectionString"].ConnectionString))
            {
                try
                {
                    if(index == 0)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT [Employer].TotalBalance, SUM([Transaction].RewardValue) " +
                            "FROM [Employer], [Transaction] " +
                            "WHERE [Employer].EmployerID = 1 " +
                            "GROUP BY [Employer].TotalBalance", con);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    else if(index == 1)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT [User].NickName, [Transaction].CompanyValue, SUM([Transaction].RewardValue) AS [Total Spent], [Transaction].TransactionDate " +
                            "FROM [User], [Transaction] " +
                            "WHERE [User].UserID = [Transaction].ReceiverID " +
                            "GROUP BY [User].NickName, [Transaction].CompanyValue, [Transaction].TransactionDate", con);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                    else if(index == 2)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT [Transaction].CompanyValue, SUM([Transaction].RewardValue) " +
                            "FROM [Transaction] " +
                            "GROUP BY [Transaction].CompanyValue", con);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                        ExportDataSetToExcel(ds);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ds.Dispose();
                }
            }
        }
        private void ExportDataSetToExcel(DataSet ds)
        {
            string AppLocation = "";
            AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            AppLocation = AppLocation.Replace("file:\\", "");
            string file = AppLocation + "\\ExcelFiles\\DataFile.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0]);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                wb.SaveAs(file);
            }
        }
    }
}