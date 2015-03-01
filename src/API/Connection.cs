using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;


namespace API
{   
    /// <summary>
    /// This class provides a Connection Object that connects to the database and performs 
    /// the addition, retrieval, modification, and removal of objects and attributes
    /// within the database.
    /// 
    /// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
    /// December 01, 2014
    /// </summary>
    public class Connection
    {
        private DataClassesDataContext db;

        /// <summary>
        /// Creates a new connection to the database.
        /// Only one connection should be open at a time.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public Connection(string connectionString)
        {
            db = new DataClassesDataContext(connectionString);
        }


        /// <summary>
        /// Closes the current connection to the database.
        /// </summary>
        public void Close()
        {
            db.Dispose();
        }


        /// <summary>
        /// Retrieve the marital status that is defined in the Persons table.
        /// 
        /// These are currently defined in Schemas/staticdata.sql
        /// </summary>
        /// <returns>Dictionary of marital status</returns>
        public Dictionary<int, string> GetMaritalStatuses()
        {
            var result = from   maritalStatus in db.MaritalStatus
                         select maritalStatus;

            return result.ToDictionary(x => x.ID, x => x.Description);
        }

        
        /// <summary>
        /// Retrieve the member standings that are defined in the MemberStanding table.
        /// 
        /// These are currently defined in Schemas/staticdata.sql
        /// </summary>
        /// <returns>Dictionary of IDs and descriptions</returns>
        public Dictionary<int, string> GetMemberStandings()
        {
            var result = from   memberStanding in db.MemberStandings
                         select memberStanding;

            return result.ToDictionary(x => x.ID, x => x.Description);
        }


        /// <summary>
        /// Retrieve the Member Statuses that are defined in the Member table.
        /// 
        /// These are currently defined in Schemas/staticdata.sql
        /// </summary>
        /// <returns>Dictionary of member status</returns>
        public Dictionary<int, string> GetMemberStatuses()
        {
            var result = from   memberStatus in db.MemberStatus
                         select memberStatus;

            return result.ToDictionary(x => x.ID, x => x.Description);
        }


        /// <summary>
        /// Retrieve the Member types that are defined in the MemberTypes table.
        /// 
        /// These are currently defined in Schemas/staticdata.sql
        /// </summary>
        /// <returns>Dictionary of Member type</returns>
        public Dictionary<int, string> GetMemberTypes()
        {
            var result = from   memberType in db.MemberTypes
                         select memberType;

            return result.ToDictionary(x => x.ID, x => x.Description);
        }


        /// <summary>
        /// Get the person's ID by specifying three parameters: last name, 
        /// first name and birthdate. Currently, these three parameters should be 
        /// sufficient to identify a person, but this may need to be modified in the 
        /// future if this is deemed insufficient.
        /// </summary>
        /// <param name="lastName">Last name of the target</param>
        /// <param name="firstName">First name the target</param>
        /// <param name="birthDate">Birth date the target</param>
        /// <returns>PersonID found in the database</returns>
        /// <exception cref="ArgumentNullException">Thrown if last name or 
        /// first name is null</exception>
        /// <exception cref="RecordNotFoundException">Thrown when there is
        /// no matching Person found</exception>
        public int GetPersonID(string lastName, string firstName, DateTime? birthDate)
        {
            if (lastName == null || firstName == null)
            {
                throw new ArgumentNullException();
            }

            IQueryable<int> result;

            if (birthDate == null)
            {

                result = from   person in db.Persons
                         where  person.LastName == lastName &&
                                person.FirstName == firstName &&
                                person.BirthDate == null
                         select person.ID;
            }
            else
            {
                result = from   person in db.Persons
                         where  person.LastName == lastName &&
                                person.FirstName == firstName &&
                                person.BirthDate == birthDate
                         select person.ID;
            }

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Gets a Person Object by specifying the ID of that Person.
        /// </summary>
        /// <param name="id">ID of the Person</param>
        /// <returns>Person</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is 
        /// no Person with that ID</exception>
        public Person GetPerson(int id)
        {
            var result = from   person in db.Persons
                         where  person.ID == id
                         select person;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Retrieve a list of all Person Objects from the database.
        /// </summary>
        /// <returns>List of Persons</returns>
        public List<Person> GetPersons()
        {
            var result = from    person in db.Persons
                         orderby person.LastName,
                                 person.FirstName,
                                 person.BirthDate
                         select  person;

            return result.ToList();
        }


        /// <summary>
        /// Retrieve a list of Persons from the database based on last name.
        /// </summary>
        /// <param name="lastName">Target last name</param>
        /// <returns>People with target last name</returns>
        public List<Person> GetPersons(string lastName)
        {
            var result = from    person in db.Persons
                         where   person.LastName.StartsWith(lastName)
                         orderby person.LastName,
                                 person.FirstName,
                                 person.BirthDate
                         select  person;

            return result.ToList();
        }


        /// <summary>
        /// Returns a list of Persons based on first and last name.
        /// </summary>
        /// <param name="lastName">Target last name</param>
        /// <param name="firstName">Target first Name</param>
        /// <returns>List of Persons which have both target names</returns>
        public List<Person> GetPersons(string lastName, string firstName)
        {
            var result = from    person in db.Persons
                         where   person.LastName.StartsWith(lastName) &&
                                 person.FirstName.StartsWith(firstName)
                         orderby person.LastName,
                                 person.FirstName,
                                 person.BirthDate
                         select  person;

            return result.ToList();
        }


        /// <summary>
        /// Adds the specified Person Object to the database.
        /// The Person ID is automatically generated by the database.
        /// </summary>
        /// <param name="person">The Person Object to be added</param>
        /// <exception cref="ArgumentNullException">Thrown if Person is null</exception>
        /// <exception cref="SqlException">Thrown if constraint is violated (unique, not null, etc)</exception>
        /// <exception cref="DuplicateRecordException">Thrown if this person already exists in database</exception>
        /// <exception cref="InvalidNameException">Thrown if first or last name is invalid</exception>
        /// <exception cref="InvalidDateException">Thrown if birth date or death date is invalid</exception>
        /// <exception cref="InvalidSinException">Thrown if social insurance number is invalid and not null</exception>
        public void AddPerson(Person person)
        {
            Validator.ValidatePerson(person);
            
            try
            {
                db.Persons.InsertOnSubmit(person);
                db.SubmitChanges();
            }
            catch (InvalidOperationException)
            {
                throw new DuplicateRecordException();
            }
            
            return;
        }


        /// <summary>
        /// Updates the attributes of the parameter Person Object,
        /// based on ID of this parameter Object.
        /// </summary>
        /// <param name="person">Person Object to update</param>
        /// <exception cref="ArgumentNullException">Thrown if Person is null</exception>
        /// <exception cref="InvalidNameException">Thrown if first or last name is invalid</exception>
        /// <exception cref="InvalidDateException">Thrown if birth date or death date is invalid</exception>
        /// <exception cref="SqlException">Thrown if constraint is violated (unique, not null, etc)</exception>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Person with that ID</exception>
        /// <exception cref="InvalidSinException">Thrown if social insurance number is invalid and not null</exception>
        public void UpdatePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException();
            }

            Person toUpdate = GetPerson(person.ID);

            Validator.ValidatePerson(person);

            toUpdate.Title           = person.Title;
            toUpdate.FirstName       = person.FirstName;
            toUpdate.LastName        = person.LastName;
            toUpdate.Initial         = person.Initial;
            toUpdate.BirthDate       = person.BirthDate;
            toUpdate.DeathDate       = person.DeathDate;
            toUpdate.Gender          = person.Gender;
            toUpdate.Address         = person.Address;
            toUpdate.PostalCode      = person.PostalCode;
            toUpdate.City            = person.City;
            toUpdate.ProvinceState   = person.ProvinceState;
            toUpdate.Country         = person.Country;
            toUpdate.HomePhone       = person.HomePhone;
            toUpdate.WorkPhone       = person.WorkPhone;
            toUpdate.CellPhone       = person.CellPhone;
            toUpdate.Fax             = person.Fax;
            toUpdate.Sin             = person.Sin;
            toUpdate.Email           = person.Email;
            toUpdate.MaritalStatusID = person.MaritalStatusID;
            toUpdate.Comments        = person.Comments;

            db.SubmitChanges();
        }


        /// <summary>
        /// Deletes a Person Object from the database, based on the parameter ID.
        /// </summary>
        /// <param name="id">Person ID</param>
        public void DeletePerson(int id)
        {
            Person person = GetPerson(id);
            
            if (person == null)
            {
                throw new RecordNotFoundException();
            }

            db.Persons.DeleteOnSubmit(person);
            db.SubmitChanges();
        }


        /// <summary>
        /// Returns whether a given Person is a Member.
        /// </summary>
        /// <param name="person">Target Person</param>
        /// <returns>whether person is member</returns>
        public bool IsMember(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException();
            }

            var result = from   member in db.Members
                         where  member.PersonID == person.ID
                         select member.ID;

            return result.Count() != 0;
        }


        /// <summary>
        /// Retrieve Member ID by specifying three parameters: last name, first name and birthdate.
        /// These three parameters are guaranteed to be unique by the database.
        /// </summary>
        /// <param name="lastName">Last name of the target</param>
        /// <param name="firstName">First name the target</param>
        /// <param name="birthDate">Birth date the target</param>
        /// <returns>Member ID found in the database</returns>
        /// <exception cref="ArgumentNullException">Thrown if the last name or first name is null</exception>
        /// <exception cref="RecordNotFoundException">Thrown when no matching person/member is found</exception>
        public int GetMemberID(string lastName, string firstName, DateTime? birthDate)
        {
            int personID = GetPersonID(lastName, firstName, birthDate);

            var result = from   member in db.Members
                         where  member.PersonID == personID
                         select member.ID;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Retrieve Member ID from a Person
        /// </summary>
        /// <param name="personID">Target Person ID</param>
        /// <returns>Member ID found in the database</returns>
        /// <exception cref="RecordNotFoundException">Thrown when no matching member is found</exception>
        public int GetMemberID(int personID)
        {
            var result = from   member in db.Members
                         where  member.PersonID == personID
                         select member.ID;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Retrieves a Member based on the provided Member ID.
        /// </summary>
        /// <param name="id">ID of target Member</param>
        /// <returns>Member Object with the specified ID</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Member with that ID</exception>
        public Member GetMember(int id)
        {
            var result = from   member in db.Members
                         where  member.ID == id
                         select member;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Gets a MemberPerson Object based on member.ID provided.
        /// </summary>
        /// <param name="id">Member ID of target</param>
        /// <returns>MemberPerson with the specified ID</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Member with that ID</exception>
        public MemberPerson GetMemberPerson(int id)
        {
            var result = from   member in db.MemberPersons
                         where  member.ID == id
                         select member;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Returns a Member based on social insurance number. 
        /// Assumes social insurance number is unique.
        /// </summary>
        /// <param name="sin">Target social insurance number</param>
        /// <returns>Target Member</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Member with the 
        /// social insurance number</exception>
        public Member GetMemberBySIN(string sin)
        {
            var result = from   member in db.Members
                         where  member.Person.Sin == sin
                         select member;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Returns a Member based on MemberNumber. Assumes MemberNumber is unique.
        /// </summary>
        /// <param name="memberNumber">Target MemberNumber</param>
        /// <returns>Target Member</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Member with that MemberNumber</exception>
        public Member GetMemberByMemberNumber(string memberNumber)
        {
            var result = from   member in db.Members
                         where  member.MemberNumber == memberNumber
                         select member;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Returns a List of all Members in the database.
        /// </summary>
        /// <returns>List of all Members</returns>
        public List<Member> GetMembers()
        {
            var result = from    member in db.Members
                         orderby member.Person.LastName,
                                 member.Person.FirstName,
                                 member.Person.BirthDate
                         select  member;

            return result.ToList();
        }


        /// <summary>
        /// Retrieve a list of Members based on last name.
        /// </summary>
        /// <param name="lastName">Target last name</param>
        /// <returns>Members with target last name</returns>
        public List<Member> GetMembers(string lastName)
        {
            var result = from    member in db.Members
                         where   member.Person.LastName.StartsWith(lastName)
                         orderby member.Person.LastName,
                                 member.Person.FirstName,
                                 member.Person.BirthDate
                         select  member;

            return result.ToList();
        }


        /// <summary>
        /// Retrieve a list of Members based on first and last name.
        /// </summary>
        /// <param name="lastName">Target last name</param>
        /// <param name="firstName">Target first name</param>
        /// <returns>List of Members with given names</returns>
        public List<Member> GetMembers(string lastName, string firstName)
        {
            var result = from    member in db.Members
                         where   member.Person.LastName.StartsWith(lastName) &&
                                 member.Person.FirstName.StartsWith(firstName)
                         orderby member.Person.LastName,
                                 member.Person.FirstName,
                                 member.Person.BirthDate
                         select  member;

            return result.ToList();
        }


        /// <summary>
        /// Adds a Member based on the parameter Person Object.
        /// ID is auto generated by the database. Must have a valid Person ID.
        /// </summary>
        /// <param name="member">Person to add as a Member</param>
        /// <exception cref="ArgumentNullException">Thrown if Member is null</exception>
        /// <exception cref="SqlException">Thrown if constraint violated(unique, not null, etc)</exception>
        public void AddMember(Member member)
        {
            Validator.ValidateMember();

            db.Members.InsertOnSubmit(member);

            db.SubmitChanges();
        }


        /// <summary>
        /// Adds both a Person and a Member to the database based on the parameter Objects.
        /// Both person.ID and member.ID will be autogenerated by the database
        /// </summary>
        /// <param name="person">Person Object to add</param>
        /// <param name="member">Member Object to add</param>
        /// <exception cref="ArgumentNullException">Thrown if Person or Member is null</exception>
        /// <exception cref="SqlException">Thrown if constraint violated (unique, not null, etc)</exception>
        /// <exception cref="DuplicateRecordException">Thrown if Person already exists in database</exception>
        /// <exception cref="InvalidNameException">Thrown if first or last name is invalid</exception>
        /// <exception cref="InvalidDateException">Thrown if birth date or death date is invalid</exception>
        /// <exception cref="InvalidSinException">Thrown if social insurance number is invalid</exception>
        public void AddMember(Person person, Member member)
        {
            AddPerson(person);
            member.PersonID = GetPersonID(person.LastName, person.FirstName, person.BirthDate);
            AddMember(member);
        }


        /// <summary>
        /// Updates Member values of given Member.
        /// Updates the Member with the same ID as parameter Object.
        /// </summary>
        /// <param name="member">Member Object to update</param>
        /// <exception cref="ArgumentNullException">Thrown if Member is null</exception>
        /// <exception cref="SqlException">Thrown if constraint violated (unique, not null, etc)</exception>
        /// <exception cref="RecordNotFoundException">Thrown if there is no Member with that ID</exception>
        public void UpdateMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException();
            }

            UpdatePerson(member.Person);

            Validator.ValidateMember();

            Member toUpdate = GetMember(member.ID);

            toUpdate.Person        = member.Person;
            toUpdate.TypeID        = member.TypeID;
            toUpdate.StandingID    = member.StandingID;
            toUpdate.StatusID      = member.StatusID;
            toUpdate.EffectiveDate = member.EffectiveDate;
            toUpdate.MemberNumber  = member.MemberNumber;
            
            db.SubmitChanges();
        }


        /// <summary>
        /// Deletes a Member Object from the database, based on the parameter ID.
        /// </summary>
        /// <param name="id">Member ID</param>
        public void DeleteMember(int id)
        {
            Member member = GetMember(id);

            if (member == null)
            {
                throw new RecordNotFoundException();
            }

            db.Members.DeleteOnSubmit(member);
            db.SubmitChanges();
        }


        /// <summary>
        /// Retrieve the Organization ID of the parameter Organization name.
        /// Currently assumes that Organization names are unique. 
        /// May need to be modified if this assumption proves false.
        /// </summary>
        /// <param name="name">Organization name</param>
        /// <exception cref="ArgumentNullException">Thrown if name is null</exception>
        /// <exception cref="RecordNotFoundException">Thrown if no Organization is found</exception>
        /// <returns>Organization ID</returns>
        public int GetOrganizationID(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            
            var result = from   organization in db.Organizations
                         where  organization.Name == name
                         select organization.ID;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }
            
            return result.Single();
        }


        /// <summary>
        /// Retrieve the Organization Object with the specified ID.
        /// </summary>
        /// <param name="id">ID of target Organization</param>
        /// <returns>Target Organization</returns>
        /// <exception cref="RecordNotFoundException">Thrown if there is 
        /// no Organization with that ID</exception>
        public Organization GetOrganization(int id)
        {
            var result = from   organization in db.Organizations
                         where  organization.ID == id
                         select organization;

            if (result.Count() == 0)
            {
                throw new RecordNotFoundException();
            }

            return result.Single();
        }


        /// <summary>
        /// Returns a list of all Organizations stored in the database.
        /// </summary>
        /// <returns>List of Organizations Objects</returns>
        public List<Organization> GetOrganizations()
        {
            var result = from   organization in db.Organizations
                         select organization;

            return result.ToList();
        }


        /// <summary>
        /// Adds an Organization to the Database. 
        /// Organization ID is auto generated by the database.
        /// </summary>
        /// <param name="organization">Organization Object to add</param>
        /// <exception cref="ArgumentNullException">Thrown if organization is null</exception>
        /// <exception cref="SqlException">Thrown if constraint violated (unique, not null, etc)</exception>
        public void AddOrganization(Organization organization)
        {
            Validator.ValidateOrganization();

            db.Organizations.InsertOnSubmit(organization);
            db.SubmitChanges();
        }


        /// <summary>
        /// Updates an Organization based on the ID of the parameter Organization Object.
        /// </summary>
        /// <param name="organization">Modified Organization Object with valid ID</param>
        /// <exception cref="ArgumentNullException">Thrown if Organization Object is null</exception>
        /// <exception cref="SqlException">Thrown if constraint violated (unique, not null, etc)</exception>
        /// <exception cref="RecordNotFoundException">Thrown if there is 
        /// no Organization with that ID</exception>
        public void UpdateOrganization(Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException();
            }

            Validator.ValidateOrganization();
            
            Organization toUpdate = GetOrganization(organization.ID);

            toUpdate.Name      = organization.Name;
            toUpdate.ContactID = organization.ContactID;

            db.SubmitChanges();
        }


        /// <summary>
        /// Deletes an Organization based on the parameter ID.
        /// </summary>
        /// <param name="id">Organization ID</param>
        public void DeleteOrganization(int id)
        {
            Organization organization = GetOrganization(id);

            if (organization == null)
            {
                throw new RecordNotFoundException();
            }

            db.Organizations.DeleteOnSubmit(organization);
            db.SubmitChanges();
        }


        /// <summary>
        /// Retrieves a list of all changes logged in the ChangeLog Table.
        /// </summary>
        /// <returns>List of all changes logged</returns>
        public List<ChangeLog> GetChangeLog()
        {
            var result = from   changeLog in db.ChangeLogs
                         select changeLog;

            return result.ToList();
        }


        /// <summary>
        /// ***DO NOT USE!***
        /// (For testing purposes only!)
        /// 
        /// Completely erases all entries in the ChangeLog Table.
        /// </summary>
        public void ClearChangeLog()
        {
            db.ChangeLogs.DeleteAllOnSubmit(db.ChangeLogs);
            db.SubmitChanges();
        }
    }
}