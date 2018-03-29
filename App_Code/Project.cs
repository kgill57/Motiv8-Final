// Johnathon Hoyns I have neither given nor received help on this assignmentusing System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Project
/// </summary>
public class Project
{
    // Skill class properties
    private string projectName;
    private string projectDescription;
    private string lastUpdatedBy;
    private string lastUpdated;

    // Skill Constructor
    public Project(string projectName, string projectDescription,
        string lastUpdatedBy, string lastUpdated)
    {
        setprojectName(projectName);
        setprojectDescription(projectDescription);
        setLastUpdatedBy(lastUpdatedBy);
        setLastUpdated(lastUpdated);
    }

    // Set skill name method
    public void setprojectName(string projectName)
    {
        this.projectName = projectName;
    }
    // Get Skill name method
    public string getprojectName()
    {
        return this.projectName;
    }
    // Set skill description method
    public void setprojectDescription(string projectDescription)
    {
        this.projectDescription = projectDescription;
    }
    // Get skill description method
    public string getprojectDescription()
    {
        return this.projectDescription;
    }
    // Set lastupdatedby method
    public void setLastUpdatedBy(string lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }
    // get lastupdatedby method
    public string getLastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }
    // setlastupdated method
    public void setLastUpdated(string lastUpdated)
    {
        this.lastUpdated = lastUpdated;
    }
    // getlastupdated method
    public string getLastUpdated()
    {
        return this.lastUpdated;
    }
}
