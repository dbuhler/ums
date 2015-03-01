using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;

namespace TestConsoleInterface
{
    static class MemberPersonTests
    {

        public static void doMPtests(Repository db, List<Person> pp)
        {

            Console.WriteLine();
            Console.WriteLine("Now Testing Member and MemberPerson methods\n");
            Console.WriteLine();

            List<MemberPerson> MPerson = new List<MemberPerson>();
            List<MemberPerson> MPTest = new List<MemberPerson>();

            MPerson = db.GetMembers();
            SetMPList(pp, MPTest);

            for (int i = 0; i < MPTest.Count; i++)
            {
                bool a = MPEquals(MPTest[i], MPerson[i]);
                if (!a)
                {
                    Console.WriteLine("MemberPerson in list number {0} is not equal. Test case failed\n", i);
                    Program.errCount++;
                }
                else
                    Console.WriteLine("MemberPerson {0} equal, Test Passed\n", i);
                Program.testCount++;
            }
            Console.WriteLine();
            Console.WriteLine("Now Testing AddMember(Person, Member), AddMember(Member), GetMemberId() and GetMember()\n");
            Member m1 = new Member();
            m1.ID = 4;
            m1.PersonID = 4;
            db.AddMember(m1);

            Person p2 = new Person();
            p2.FirstName = "Harry";
            p2.LastName = "Smith";
            p2.BirthDate = new DateTime(1967, 05, 20);
            p2.ID = 5;
            Member m2 = new Member();
            m2.PersonID = 5;
            m2.ID = 5;
            db.AddMember(p2, m2);

            Program.testCount++;
            int harryID = db.GetMemberID("Smith", "Harry", new DateTime(1967, 05, 20));
            if (harryID == 5)
            {
                Console.WriteLine("AddMember(Person, Member) and GetMemberId() Passed\n");
            }
            else
            {
                Console.WriteLine("AddMember(Person, Member) and GetMemberId() Failed\n");
                Program.errCount++;
            } Console.WriteLine();

            MPerson = db.GetMembers();

            Program.testCount++;
            if (!MPEquals(MPerson[4], db.GetMemberPerson(5)))
            {
                Console.WriteLine("AddMember() and GetMemberId() Failed\n");
                Program.errCount++;
            }
            else
            {
                Console.WriteLine("AddMember() and GetMemberId() Passed\n");
            }
        
            MemberPerson test = MPerson[0];
            test.IsActive = false;
            test.Initial = 'z';
            test.HomePhone = null;
            test.FirstName = "lucky luciano";
            db.UpdateMember(test);

            Program.testCount++;
            if (!MPEquals(test, db.GetMemberPerson(test.ID)))
            {
                Console.WriteLine("UpdateMember() Failed\n");
                Program.errCount++;
            }
            else
            {
                Console.WriteLine("UpdateMember() Passed\n");
            }
        }
        public static void SetMPList(List<Person> pp, List<MemberPerson> mp)
        {
            mp.Clear();
            for (int i = 0; i < pp.Count; i++)
            {
                MemberPerson temp = new MemberPerson();
                temp.FirstName = pp[i].FirstName;
                temp.LastName = pp[i].LastName;
                temp.Title = pp[i].Title;
                temp.BirthDate = pp[i].BirthDate;
                temp.Address = pp[i].Address;
                temp.City = pp[i].City;
                temp.ProvinceState = pp[i].ProvinceState;
                temp.HomePhone = pp[i].HomePhone;
                temp.Gender = pp[i].Gender;
                temp.Initial = pp[i].Initial;
                temp.Country = pp[i].Country;
                temp.PostalCode = pp[i].PostalCode;
                temp.ID = i + 1;
                temp.PersonID = temp.ID;

                mp.Add(temp);
            }

            mp[0].IsActive = true;
            mp[1].IsActive = false;
            mp[2].IsActive = true;
        }

        public static bool MPEquals(MemberPerson mp1, MemberPerson mp2)
        {
            return mp1.ID == mp2.ID
                 && mp1.PersonID == mp2.PersonID
                 && mp1.TypeID == mp2.TypeID
                 && mp1.StandingID == mp2.StandingID
                 && mp1.IsActive == mp2.IsActive
                 && (mp1.Title == mp2.Title || mp1.Title.ToLower().Equals(mp2.Title.ToLower()))
                 && mp1.Gender == mp2.Gender
                 && (mp1.BirthDate == mp2.BirthDate || mp1.BirthDate.Equals(mp2.BirthDate))
                 && (mp1.Fax == mp2.Fax || mp1.Fax.ToLower().Equals(mp2.Fax.ToLower()))
                 && (mp1.FirstName == mp2.FirstName || mp1.FirstName.ToLower().Equals(mp2.FirstName.ToLower()))
                 && (mp1.LastName == mp2.LastName || mp1.LastName.ToLower().Equals(mp2.LastName.ToLower()))
                 && (mp1.Initial == mp2.Initial || Char.ToLower((char)(mp1.Initial ?? ' ')) == Char.ToLower((char)(mp2.Initial ?? ' ')))
                 && (mp1.DeathDate == mp2.DeathDate || mp1.DeathDate.Equals(mp2.DeathDate))
                 && (mp1.Address == mp2.Address || mp1.Address.ToLower().Equals(mp2.Address.ToLower()))
                 && (mp1.PostalCode == mp2.PostalCode || mp1.PostalCode.ToLower().Equals(mp2.PostalCode.ToLower()))
                 && (mp1.City == mp2.City || mp1.City.ToLower().Equals(mp2.City.ToLower()))
                 && (mp1.ProvinceState == mp2.ProvinceState || mp1.ProvinceState.ToLower().Equals(mp2.ProvinceState.ToLower()))
                 && (mp1.Country == mp2.Country || mp1.Country.ToLower().Equals(mp2.Country.ToLower()))
                 && (mp1.HomePhone == mp2.HomePhone || mp1.HomePhone.ToLower().Equals(mp2.HomePhone.ToLower()))
                 && (mp1.WorkPhone == mp2.WorkPhone || mp1.WorkPhone.ToLower().Equals(mp2.WorkPhone.ToLower()))
                 && (mp1.CellPhone == mp2.CellPhone || mp1.CellPhone.ToLower().Equals(mp2.CellPhone.ToLower()))
                 && (mp1.Sin == mp2.Sin || mp1.Sin.ToLower().Equals(mp2.Sin.ToLower()))
                 && (mp1.Email == mp2.Email || mp1.Email.ToLower().Equals(mp2.Email.ToLower()))
                 && (mp1.MaritalStatus == mp2.MaritalStatus || mp1.MaritalStatus.ToLower().Equals(mp2.MaritalStatus.ToLower()));
        }

        static void PrintMemberPersonList(List<MemberPerson> mp)
        {
            for (int i = 0; i < mp.Count; i++)
            {
                Console.WriteLine("MemberId {0} Name {1} {2} address {3}", mp[i].ID, mp[i].FirstName, mp[i].LastName, mp[i].Address);
            }
        }

    }
}
