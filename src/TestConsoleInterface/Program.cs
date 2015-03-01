using API;
using System;
using System.Collections.Generic;
using System.Threading;


/// <summary>
/// This is a basic console interface for testing on the fly.
/// 
/// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
/// December 01, 2014
/// </summary>
namespace TestConsoleInterface
{
        
    class Program
    {

        static int Main(string[] args)
        {
            string server = "localhost";
            string database = "UnionDB";
            string user = "root";
            string pass = "";

            Connection db = Repository.OpenConnection(
                server,
                database,
                user,
                pass);

            List<Person> persons = db.GetPersons();
            printPersons(persons);

            db.Close();

            Console.WriteLine("DONE!");
            Console.ReadLine();
            

            return 0;
        }


        public static void printPersons(List<Person> persons)
        {
            Console.WriteLine();
            for (int i = 0; i < persons.Count; i++)
                Console.WriteLine("{0} {1}  id: {2}",
                    persons[i].FirstName, persons[i].LastName, persons[i].ID);
        }


        public static void printMembers(List<Member> members)
        {
            Console.WriteLine();
            for (int i = 0; i < members.Count; i++)
                Console.WriteLine("{0} {1}  id: {2} member number: {3}",
                    members[i].Person.FirstName, members[i].Person.LastName, members[i].ID, members[i].MemberNumber);
        }

        public static bool PersonEquals(Person p1, Person p2)
        {
            return p1.ID == p2.ID
            && (p1.Title == p2.Title || p1.Title.ToLower().Equals(p2.Title.ToLower()))
            && p1.Gender == p2.Gender
            && (p1.BirthDate == p2.BirthDate || p1.BirthDate.Equals(p2.BirthDate))
            && (p1.Fax == p2.Fax || p1.Fax.ToLower().Equals(p2.Fax.ToLower()))
            && (p1.FirstName == p2.FirstName || p1.FirstName.ToLower().Equals(p2.FirstName.ToLower()))
            && (p1.LastName == p2.LastName || p1.LastName.ToLower().Equals(p2.LastName.ToLower()))
            && (p1.Initial == p2.Initial || Char.ToLower((char)(p1.Initial ?? ' ')) == Char.ToLower((char)(p2.Initial ?? ' ')))
            && (p1.DeathDate == p2.DeathDate || p1.DeathDate.Equals(p2.DeathDate))
            && (p1.Address == p2.Address || p1.Address.ToLower().Equals(p2.Address.ToLower()))
            && (p1.PostalCode == p2.PostalCode || p1.PostalCode.ToLower().Equals(p2.PostalCode.ToLower()))
            && (p1.City == p2.City || p1.City.ToLower().Equals(p2.City.ToLower()))
            && (p1.ProvinceState == p2.ProvinceState || p1.ProvinceState.ToLower().Equals(p2.ProvinceState.ToLower()))
            && (p1.Country == p2.Country || p1.Country.ToLower().Equals(p2.Country.ToLower()))
            && (p1.HomePhone == p2.HomePhone || p1.HomePhone.ToLower().Equals(p2.HomePhone.ToLower()))
            && (p1.WorkPhone == p2.WorkPhone || p1.WorkPhone.ToLower().Equals(p2.WorkPhone.ToLower()))
            && (p1.CellPhone == p2.CellPhone || p1.CellPhone.ToLower().Equals(p2.CellPhone.ToLower()))
            && (p1.Sin == p2.Sin || p1.Sin.ToLower().Equals(p2.Sin.ToLower()))
            && (p1.Email == p2.Email || p1.Email.ToLower().Equals(p2.Email.ToLower()))
            && (p1.MaritalStatusID == p2.MaritalStatusID);
        }
    }
}
