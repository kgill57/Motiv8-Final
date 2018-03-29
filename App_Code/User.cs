using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class User
{
    private int userID;
    private String fName;
    private char MI;
    private String lName;
    private String email;
    private String userName;
    private String profilePicture;
    private Boolean admin;
    private String lastUpdatedBy;
    private DateTime lastUpdated;


    public User()
    {



    }
    // Setters
    public void setUserID(int userID)
    {
        this.userID = userID;
    }
    
    public void setFName(String fName)
    {
        this.fName = fName;
    }

    public void setMI(char MI)
    {
        this.MI = MI;
    }

    public void setLName(String lName)
    {
        this.lName = lName;
    }

    public void setEmail(String email)
    {
        this.email = email;
    }

    public void setUserName(String userName)
    {
        this.userName = userName;
    }

    public void setProfilePicture(String profilePicture)
    {
        this.profilePicture = profilePicture;
    }

    public void setAdmin(Boolean admin)
    {
        this.admin = admin;
    }

    public void setLastUpdatedBy(String lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }

    public void setLastUpdated(DateTime lastUpdated)
    {
        this.lastUpdated = lastUpdated;
    }

    // Getters
    public int getUserID()
    {
        return this.userID;
    }

    public String getFName()
    {
        return this.fName;
    }

    public char getMI()
    {
        return this.MI;
    }

    public String getLName()
    {
        return this.lName;
    }

    public String getEmail()
    {
        return this.email;
    }

    public String getUserName()
    {
        return this.userName;
    }

    public String getProfilePicture()
    {
        return this.profilePicture;
    }

    public Boolean getAdmin()
    {
        return this.admin;
    }

    public String getLastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }

    public DateTime getLastUpdated()
    {
        return this.lastUpdated;
    }

    
}