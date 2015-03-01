using API;
using System;
using System.Collections.Generic;
using System.Web.UI;


public partial class EditPerson : Page
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

            ScriptManager.RegisterStartupScript(
                this, typeof(Page), "datepicker", "datepicker_init();", true);

            Person person;
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                person = db.GetPerson(personID);
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

            dropDownMaritalStatus.DataSource = Application["MaritalStatuses"] as Dictionary<int, string>;
            dropDownMaritalStatus.DataBind();

            textBoxTitle.Text         = person.Title;
            textBoxLastName.Text      = person.LastName;
            textBoxFirstName.Text     = person.FirstName;
            textBoxInitial.Text       = Utils.ToString(person.Initial);
            textBoxBirthDate.Text     = Utils.ToString(person.BirthDate);
            textBoxDeathDate.Text     = Utils.ToString(person.DeathDate);
            textBoxSin.Text           = person.Sin;
            textBoxComments.Text      = person.Comments;
            textBoxAddress.Text       = person.Address;
            textBoxCity.Text          = person.City;
            textBoxPostalCode.Text    = person.PostalCode;
            textBoxProvinceState.Text = person.ProvinceState;
            textBoxCountry.Text       = person.Country;
            textBoxHomePhone.Text     = person.HomePhone;
            textBoxWorkPhone.Text     = person.WorkPhone;
            textBoxCellPhone.Text     = person.CellPhone;
            textBoxFax.Text           = person.Fax;
            textBoxEmail.Text         = person.Email;

            radioButtonsGender.SelectedValue    = Utils.ToString(person.Gender);
            dropDownMaritalStatus.SelectedValue = Utils.ToString(person.MaritalStatusID);
        }
    }


    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                Person person = db.GetPerson(personID);

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

                db.UpdatePerson(person);

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