using API;
using System;
using System.Collections.Generic;
using System.Web.UI;


public partial class AddMember : Page
{
    private Connection    db;
    private static int    personID;
    private static string previousPage;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            previousPage = this.GetPreviousPage();

            personID = 0;

            if (Request.QueryString["id"] != null &&
                !int.TryParse(Request.QueryString["id"], out personID))
            {
                Response.Redirect(previousPage ?? "~/");
                return;
            }

            ScriptManager.RegisterStartupScript(
                this, typeof(Page), "datepicker", "datepicker_init();", true);

            dropDownMaritalStatus.DataSource  = Application["MaritalStatuses"] as Dictionary<int, string>;
            dropDownMemberType.DataSource     = Application["MemberTypes"]     as Dictionary<int, string>;
            dropDownMemberStatus.DataSource   = Application["MemberStatuses"]  as Dictionary<int, string>;
            dropDownMemberStanding.DataSource = Application["MemberStandings"] as Dictionary<int, string>;
            dropDownMaritalStatus.DataBind();
            dropDownMemberType.DataBind();
            dropDownMemberStatus.DataBind();
            dropDownMemberStanding.DataBind();

            if (personID != 0)
            {
                Person person;
                db = Repository.OpenConnection(Session["ConnectionString"] as string);
                
                try
                {
                    person = db.GetPerson(personID);

                    if (db.IsMember(person))
                    {
                        formAdd.Visible = false;
                        labelMessage.Text = string.Format("Error: {0} {1} is already a member.",
                            person.FirstName, person.LastName);
                    }
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

                textBoxTitle.ReadOnly         = true;
                textBoxLastName.ReadOnly      = true;
                textBoxFirstName.ReadOnly     = true;
                textBoxInitial.ReadOnly       = true;
                textBoxBirthDate.ReadOnly     = true;
                textBoxDeathDate.ReadOnly     = true;
                textBoxSin.ReadOnly           = true;
                textBoxComments.ReadOnly      = true;
                textBoxAddress.ReadOnly       = true;
                textBoxCity.ReadOnly          = true;
                textBoxPostalCode.ReadOnly    = true;
                textBoxProvinceState.ReadOnly = true;
                textBoxCountry.ReadOnly       = true;
                textBoxHomePhone.ReadOnly     = true;
                textBoxWorkPhone.ReadOnly     = true;
                textBoxCellPhone.ReadOnly     = true;
                textBoxFax.ReadOnly           = true;
                textBoxEmail.ReadOnly         = true;
            }
        }
    }


    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Person person = new Person();
            Member member = new Member();
            
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

            member.MemberNumber    = Utils.GetString(textBoxMemberNumber);
            member.TypeID          = Utils.GetInt(dropDownMemberType);
            member.StatusID        = Utils.GetInt(dropDownMemberStatus);
            member.EffectiveDate   = Utils.GetDate(textBoxEffectiveDate);
            member.StandingID      = Utils.GetInt(dropDownMemberStanding);

            try
            {
                if (personID == 0)
                {
                    db.AddMember(person, member);
                }
                else
                {
                    member.PersonID = personID;
                    db.AddMember(member);
                }

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