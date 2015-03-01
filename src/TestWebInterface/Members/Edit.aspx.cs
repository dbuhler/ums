using API;
using System;
using System.Collections.Generic;
using System.Web.UI;


public partial class EditMember : Page
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

            ScriptManager.RegisterStartupScript(
                this, typeof(Page), "datepicker", "datepicker_init();", true);

            Person person;
            Member member;
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                member = db.GetMember(memberID);
                person = member.Person;
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

            dropDownMaritalStatus.DataSource  = Application["MaritalStatuses"] as Dictionary<int, string>;
            dropDownMemberType.DataSource     = Application["MemberTypes"]     as Dictionary<int, string>;
            dropDownMemberStatus.DataSource   = Application["MemberStatuses"]  as Dictionary<int, string>;
            dropDownMemberStanding.DataSource = Application["MemberStandings"] as Dictionary<int, string>;
            dropDownMaritalStatus.DataBind();
            dropDownMemberType.DataBind();
            dropDownMemberStatus.DataBind();
            dropDownMemberStanding.DataBind();

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
            textBoxMemberNumber.Text  = member.MemberNumber;
            textBoxEffectiveDate.Text = Utils.ToString(member.EffectiveDate);

            radioButtonsGender.SelectedValue     = Utils.ToString(person.Gender);
            dropDownMaritalStatus.SelectedValue  = Utils.ToString(person.MaritalStatusID);
            dropDownMemberType.SelectedValue     = Utils.ToString(member.TypeID);
            dropDownMemberStatus.SelectedValue   = Utils.ToString(member.StatusID);
            dropDownMemberStanding.SelectedValue = Utils.ToString(member.StandingID);
        }
    }


    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            db = Repository.OpenConnection(Session["ConnectionString"] as string);

            try
            {
                Member member = db.GetMember(memberID);

                member.Person.Title           = Utils.GetString(textBoxTitle);
                member.Person.LastName        = Utils.GetString(textBoxLastName);
                member.Person.FirstName       = Utils.GetString(textBoxFirstName);
                member.Person.Initial         = Utils.GetChar(textBoxInitial);
                member.Person.Gender          = Utils.GetChar(radioButtonsGender);
                member.Person.BirthDate       = Utils.GetDate(textBoxBirthDate);
                member.Person.DeathDate       = Utils.GetDate(textBoxDeathDate);
                member.Person.Sin             = Utils.GetString(textBoxSin);
                member.Person.MaritalStatusID = Utils.GetInt(dropDownMaritalStatus);
                member.Person.Comments        = Utils.GetString(textBoxComments);
                member.Person.Address         = Utils.GetString(textBoxAddress);
                member.Person.City            = Utils.GetString(textBoxCity);
                member.Person.PostalCode      = Utils.GetString(textBoxPostalCode);
                member.Person.ProvinceState   = Utils.GetString(textBoxProvinceState);
                member.Person.Country         = Utils.GetString(textBoxCountry);
                member.Person.HomePhone       = Utils.GetString(textBoxHomePhone);
                member.Person.WorkPhone       = Utils.GetString(textBoxWorkPhone);
                member.Person.CellPhone       = Utils.GetString(textBoxCellPhone);
                member.Person.Fax             = Utils.GetString(textBoxFax);
                member.Person.Email           = Utils.GetString(textBoxEmail);

                member.MemberNumber           = Utils.GetString(textBoxMemberNumber);
                member.TypeID                 = Utils.GetInt(dropDownMemberType);
                member.StatusID               = Utils.GetInt(dropDownMemberStatus);
                member.EffectiveDate          = Utils.GetDate(textBoxEffectiveDate);
                member.StandingID             = Utils.GetInt(dropDownMemberStanding);

                db.UpdateMember(member);

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