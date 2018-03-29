
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class RewardProvider
{
    private int providerID;
    private String name;
    private String email;

    // Setters
    public void setProviderID(int providerID)
    {
        this.providerID = providerID;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public void setEmail(String email)
    {
        this.email = email;
    }

    // Getters
    public int getProviderID(int userID)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT ProviderID FROM [User] WHERE UserID = " + userID;
        cmd.Connection = con;

        int providerID = (int)cmd.ExecuteScalar();

        con.Close();

        return providerID;
    }

    public String getName()
    {
        return this.name;
    }

    public String getEmail()
    {
        return this.email;
    }

    public RewardProvider(int providerID, String name, String email)
    {
        setProviderID(providerID);
        setName(name);
        setEmail(email);
    }

    public RewardProvider()
    {

    }

}