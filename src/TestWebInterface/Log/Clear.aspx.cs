using API;
using System;
using System.Web.UI;


public partial class ClearLog : Page
{
    private Connection    db;
    private static string previousPage;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            previousPage = this.GetPreviousPage();
        }
    }


    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        db = Repository.OpenConnection(Session["ConnectionString"] as string);
        db.ClearChangeLog();
        db.Close();

        Response.Redirect(previousPage);
    }


    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(previousPage);
    }
}