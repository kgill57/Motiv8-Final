using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class EventPost
{
    private int eventPostID;
    private String eventTitle;
    private String eventDesc;
    private String postedBy;
    private DateTime datePosted;

    //Event Post Constructor

    public EventPost(int eventPostID, String eventTitle, String eventDesc,
        DateTime datePosted, String postedBy)
    {
        setEventPostID(eventPostID);
        setEventTitle(eventTitle);
        setEventDesc(eventDesc);
        setPostedBy(postedBy);
        setDatePosted(datePosted);
    }



    // Setters
    public void setEventPostID(int eventPostID)
    {
        this.eventPostID = eventPostID;
    }

    public void setEventTitle(String eventTitle)
    {
        this.eventTitle = eventTitle;
    }

    public void setEventDesc(String eventDesc)
    {
        this.eventDesc = eventDesc;
    }

    public void setPostedBy(String postedBy)
    {
        this.postedBy = postedBy;
    }

    public void setDatePosted(DateTime datePosted)
    {
        this.datePosted = datePosted;
    }

    // Getters 
    public int getEventPostID()
    {
        return this.eventPostID;
    }

    public String getEventTitle()
    {
        return this.eventTitle;
    }

    public String getEventDesc()
    {
        return this.eventDesc;
    }

    public String getPostedBy()
    {
        return this.postedBy;
    }

    public DateTime getDatePosted()
    {
        return this.datePosted;
    }

    public EventPost()
    {

    }
}