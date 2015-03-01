<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        API.Connection db = API.Repository.OpenConnection(
            "localhost", "UnionDB", "root", "");

        Application["MaritalStatuses"] = db.GetMaritalStatuses();
        Application["MemberStandings"] = db.GetMemberStandings();
        Application["MemberStatuses"]  = db.GetMemberStatuses();
        Application["MemberTypes"]     = db.GetMemberTypes();
        
        db.Close();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
    }

    void Session_Start(object sender, EventArgs e) 
    {
        Session["ConnectionString"] = API.Repository.GetConnectionString(
            "localhost", "UnionDB", "root", "");
    }

    void Session_End(object sender, EventArgs e) 
    {
    }
       
</script>