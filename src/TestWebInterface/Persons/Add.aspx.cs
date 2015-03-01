using API;
using System;
using System.Collections.Generic;
using System.Web.UI;


public partial class AddPerson : Page
{
    private Connection    db;
    private static string previousPage;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            previousPage = this.GetPreviousPage();
            
            ScriptManager.RegisterStartupScript(
                this, typeof(Page), "datepicker", "datepicker_init();", true);

            dropDownMaritalStatus.DataSource = Application["MaritalStatuses"] as Dictionary<int, string>;
            dropDownMaritalStatus.DataBind();
        }
    }


    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Person person = new Person();
            
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            person.Title           = Utils.GetString(textBoxTitle);
            person.LastName        = Utils.GetString(textBoxLastName);
            person.FirstName       = Utils.GetString(textBoxFirstName);
            person.Initial         = Utils.GetChar(textBoxInitial);
            person.Gender          = Utils.GetChar(radioButtonsGender);
            person.BirthDate       = Utils.GetDate(textBoxBirthDate);
            person.DeathDate       = Utils.GetDate(textBoxDeathDate);
            person.Sin             = Utils.GetString(textBoxSin);
            person.MaritalStatusID = Utils.GetInt(dropDownMaritalStatus);
            person.Comments        = Utils.GetString(textBoxComments);
            person.Address         = Utils.GetString(textBoxAddress);
            person.City            = Utils.GetString(textBoxCity);
            person.PostalCode      = Utils.GetString(textBoxPostalCode);
            person.ProvinceState   = Utils.GetString(textBoxProvinceState);
            person.Country         = Utils.GetString(textBoxCountry);
            person.HomePhone       = Utils.GetString(textBoxHomePhone);
            person.WorkPhone       = Utils.GetString(textBoxWorkPhone);
            person.CellPhone       = Utils.GetString(textBoxCellPhone);
            person.Fax             = Utils.GetString(textBoxFax);
            person.Email           = Utils.GetString(textBoxEmail);

            try
            {
                db.AddPerson(person);
                Response.Redirect(previousPage ?? "~/");
            }
            catch (Exception ex)
            {
                labelMessage.Text = ex.Message;
            }
            finally
            {
                db.Close();
            }
        }
    }


    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(previousPage ?? "~/");
    }
}