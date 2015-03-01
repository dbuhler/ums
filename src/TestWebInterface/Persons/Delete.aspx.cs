using API;
using System;
using System.Web.UI;


public partial class DeletePerson : Page
{
    private Connection    db;
    private static int    personID;
    private static string previousPage;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            previousPage = this.GetPreviousPage();
            
            if (!int.TryParse(Request.QueryString["id"], out personID))
            {
                Response.Redirect(previousPage ?? "~/");
                return;
            }

            Person person;

            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                person = db.GetPerson(personID);
                labelName.Text = string.Format("{0} {1}", person.FirstName, person.LastName);
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
            db.DeletePerson(personID);
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