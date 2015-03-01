using API;
using System;
using System.Web.UI;


public partial class FindPerson : Page
{
    private Connection db;

    
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        db = Repository.OpenConnection(Session["ConnectionString"] as string);

        string lastName  = Utils.GetString(textBoxLastName);
        string firstName = Utils.GetString(textBoxFirstName);

        if (firstName == null)
        {
            gridViewData.DataSource = db.GetPersons(lastName);
        }
        else
        {
            gridViewData.DataSource = db.GetPersons(lastName, firstName);
        }

        gridViewData.DataBind();
        db.Close();
    }
}