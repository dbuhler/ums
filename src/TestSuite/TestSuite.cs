using API;
using System;
using System.Collections.Generic;
/// <summary>
/// TestSuite.cs organizes and calls tests on different parts of the API. There are currently tests for
/// Members, Persons and Organizations, each partitioned to different files in the TestSuite. Each test
/// section does its own reporting on test run and failures, but TestSuite.cs aggregates the total number of 
/// failures.
/// 
/// It requires a connection string to a database, and a clean database with test data from Schemas/testdata.sql
/// 
/// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
/// December 01, 2014
/// </summary>
namespace TestSuite
{
    class TestSuite
    {
        public static string stars = "**********************************************************************";
        

        static int Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Invalid Parameters.  Must have serverName databaseName userName password");
                return 1;
            }

            string server = args[0];
            string database = args[1];
            string user = args[2];
            string pass = args[3];

            string connectionString = Repository.GetConnectionString(
                server, database, user, pass);

            Connection db;

            int totalErr = 0;

            db = Repository.OpenConnection(connectionString);
            totalErr += PersonTests.RunPersonTests(db);
            db.Close();

            db = Repository.OpenConnection(connectionString);
            totalErr += MemberTests.RunMemberTests(db);
            db.Close();

            db = Repository.OpenConnection(connectionString);
            totalErr += OrganizationTests.RunOrganizationTests(db);
            db.Close();

            db = Repository.OpenConnection(connectionString);
            printMembersAndOrgs(db);

            Console.WriteLine();
            Console.WriteLine("Tests Completed.  {0} errors encountered.", totalErr);

            db.Close();

            return totalErr;
        }

        static void printMembersAndOrgs(Connection db)
        {
            Console.WriteLine(stars);
            List<Member> members = db.GetMembers();
            for (int i = 0; i < members.Count; i++)
                Console.WriteLine("{0} Mid : {1} Pid {2}", members[i].Person.LastName, members[i].ID, members[i].PersonID);
            Console.WriteLine(stars);

            Console.WriteLine();

            Console.WriteLine(stars);
            List<Organization> orgs = db.GetOrganizations();
            for (int i = 0; i < orgs.Count; i++)
                Console.WriteLine(" Orgid {0}   name {1}", orgs[i].ID, orgs[i].Name);
            Console.WriteLine(stars);
        }
    }
}
