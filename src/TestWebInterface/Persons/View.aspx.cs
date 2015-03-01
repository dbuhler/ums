using API;
using System;
using System.Collections.Generic;
using System.Web.UI;


public partial class ViewPerson : Page
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
                Response.Redirect(previousPage);
                return;
            }

            Person person;
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                person = db.GetPerson(personID);
            }
            catch (RecordNotFoundException)
            {
                // Record not found.
                Response.Redirect(previousPage);
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


    protected void buttonEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Persons/Edit.aspx?id=" + personID);
    }


    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Persons/Delete.aspx?id=" + personID);
    }
}