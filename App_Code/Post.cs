using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class Post
{
    private int PID;
    private String value;
    private String category;
    private String description;
    private double rewardValue;
    private DateTime postDate;
    private bool isPrivate;
    private int giverID;
    private int receiverID;

    public Post()
    {

    }
    public Post(int pID, String value, String category, String description, double rewardValue, DateTime postDate, bool isPrivate, int giverID, int receiverID)
    {
        setPID(pID);
        setValue(value);
        setCategory(category);
        setDescription(description);
        setRewardValue(rewardValue);
        setPostDate(postDate);
        setIsPrivate(isPrivate);
        setGiverID(giverID);
        setReceiverID(receiverID);
    }

    // Setters
    public void setPID(int PID)
    {
        this.PID = PID;
    }

    public void setValue(String value)
    {
        this.value = value;
    }

    public void setCategory(String category)
    {
        this.category = category;
    }

    
    public void setDescription(String description)
    {
        this.description = description;
    }

    public void setRewardValue(double rewardValue)
    {
        this.rewardValue = rewardValue;
    }

    public void setPostDate(DateTime postDate)
    {
        this.postDate = postDate;
    }

    public void setIsPrivate(bool isPrivate)
    {
        this.isPrivate = isPrivate;
    }

    public void setGiverID(int giverID)
    {
        this.giverID = giverID;
    }
    public void setReceiverID(int receiverID)
    {
        this.receiverID = receiverID;
    }

    // Getters
    public int getPID()
    {
        return this.PID;
    }

    public String getValue()
    {
        return this.value;
    }

    public String getCategory()
    {
        return this.category;
    }


    public String getDescription()
    {
        return this.description;
    }

    public double getRewardValue()
    {
        return this.rewardValue;
    }

    public DateTime getPostDate()
    {
        return this.postDate;
    }

    public bool getIsPrivate()
    {
        return this.isPrivate;
    }

    public int getGiverID()
    {
        return this.giverID;
    }
    
    public int getReceiverID()
    {
        return this.receiverID;
    }

    public string getGiverNickName(int giverID)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;

        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT NickName FROM [User] WHERE UserID = @userID", con);
        cmd.Parameters.AddWithValue("@userID", giverID);
        
        string NickName = (String)cmd.ExecuteScalar();
        con.Close();
        return NickName;
        
    }

    public string getReceiverNickName(int receiverID)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["lab4ConnectionString"].ConnectionString;
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT NickName FROM [User] WHERE UserID = @userID", con);
        cmd.Parameters.AddWithValue("@userID", receiverID);
        
        string NickName = (String)cmd.ExecuteScalar();
        con.Close();
        return NickName;
        
    }

    
}