using API;
using System;
using System.Web.UI;


public partial class DeleteMember : Page
{
    private Connection    db;
    private static int    memberID;
    private static string previousPage;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            previousPage = this.GetPreviousPage();

            if (!int.TryParse(Request.QueryString["id"], out memberID))
            {
                Response.Redirect(previousPage ?? "~/");
                return;
            }

            Member member;

            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                member = db.GetMember(memberID);
                labelName.Text = string.Format("{0} {1}", member.Person.FirstName, member.Person.LastName);
            }
            catch (RecordNotFoundException)
            {
                // Record not found.
                Response.Redirect(previousPage ?? "~/");
                return;
            }
            finally
            {
                db.Close();
            }
        }
    }


    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        db = Repository.OpenConnection(Session["ConnectionString"] as string);

        try
        {
            db.DeleteMember(memberID);
        }
        catch (RecordNotFoundException)
        {
            // Record not found.
        }
        finally
        {
            db.Close();
        }

        Response.Redirect(previousPage ?? "~/");
    }


    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(previousPage ?? "~/");
    }
}