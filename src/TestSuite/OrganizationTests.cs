using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;
using System.Data.SqlClient;
/// <summary>
/// OrganizationTests.cs is a part of the TestSuite for the API.  It runs tests on Organization functions.
/// It tests for things that are expected to pass as well as things that are expected to fail.  It tests adding,
/// updating and finding Organizations.
/// 
/// It also has equals method to see if two Organization Objects are equal.
/// When finished, it reports on the number of tests run, passed and Failed.
/// 
/// It requires a valid Connection object to the database.
/// 
/// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
/// December 01, 2014
/// </summary>
   static class OrganizationTests
    {
        static int errCount = 0;
        static int testCount = 0;
        public static string stars = "**********************************************************************";

        public static int RunOrganizationTests(Connection db)
        {
            Console.WriteLine();
            Console.WriteLine("Now Running Organization Tests");
            Console.Error.WriteLine("Now Running Organization Tests\n");

            List<Organization> testOrgs = makeAndAddOrgs(db);

            testGetOrganization(db);

            testGetOrganizationID(db);

            testGetOrganizations(db, testOrgs);

            testUpdateOrganization(db, testOrgs);

            testAdditionalAdds(db, testOrgs);

            testInvalidUpdateOrganization(db);

            WriteOrganizationResults();
            return errCount;
        }

        static bool equals(Organization o1, Organization o2)
        {
            return o1.ID == o2.ID
                && o1.Name.Equals(o2.Name)
                && o1.ContactID == o2.ContactID;
        }

        static void WriteOrganizationResults()
        {
            Console.WriteLine();
            Console.WriteLine("Completed Organization Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
            Console.WriteLine();
            Console.WriteLine("{0}", stars);

            Console.Error.WriteLine("Completed Organization Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
            Console.Error.WriteLine("\n{0}\n", stars);
        }

        static List<Organization> makeAndAddOrgs(Connection db)
        {
            bool caught = false;
            Organization org1 = new Organization();
            org1.ID = 1;
            org1.Name = "The Best Company";
            org1.ContactID = 1;

            Organization org2 = new Organization();
            org2.ID = 2;
            org2.Name = "The Other Company";
            org2.ContactID = 2;

            List<Organization> testOrgs = new List<Organization>();


            testOrgs.Add(org1);
            testOrgs.Add(org2);

            testCount++;

            try
            {
                db.AddOrganization(org1);
                db.AddOrganization(org2);
            }
            catch (Exception)
            {
                caught = true;
                errCount++;
                Console.WriteLine();
                Console.WriteLine("AddOrganization() test with valid data FAILED");
            }

            if (!caught)
            {
                Console.WriteLine();
                Console.WriteLine("AddOrganization() test with valid data test Passed");
            }

            return testOrgs;
        }

        static void testGetOrganization(Connection db)
        {
            List<Organization> testOrgs = db.GetOrganizations();

            if (!equals(db.GetOrganization(testOrgs[0].ID), testOrgs[0]))
            {
                errCount++;
                Console.WriteLine("Test GetOrganization does not match organization added, test FAILS.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Test GetOrganization matches organization added, test Passes.");
                Console.WriteLine("");
            }
            testCount++;
        }

        static void testGetOrganizationID(Connection db)
        {
            int id = db.GetOrganizationID("The Other Company");

            if (id != 2)
            {
                errCount++;
                Console.WriteLine("Test GetOrganizationID does not match organization added, test FAILS.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Test GetOrganizationID matches organization added, test Passes.");
                Console.WriteLine("");
            }
            testCount++;
        }

        static void testGetOrganizations(Connection db, List<Organization> testOrgs)
        {
            List<Organization> OO = db.GetOrganizations();

            for (int j = 0; j < OO.Count && j < testOrgs.Count; j++)
            {
                if (!equals(OO[j], testOrgs[j]))
                {
                    errCount++;
                    Console.WriteLine("Test GetOrganizations Object does not match organization added, test FAILS.");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Test GetOrganizations Object matches organization added, test Passes.");
                    Console.WriteLine("");
                }
                testCount++;
            }
        }

        static void testUpdateOrganization(Connection db, List<Organization> testOrgs)
        {
            Organization test = testOrgs[0];
            test.ContactID = 3;
            test.Name = "Hinkledinkle";
            db.UpdateOrganization(test);

            if (!equals(db.GetOrganization(test.ID), test))
            {
                errCount++;
                Console.WriteLine("UpdateOrganization() Object does not match, test FAILS.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("UpdateOrganization() Object matches, test Passes.");
                Console.WriteLine("");
            }
            testCount++;
        }

        static void testAdditionalAdds(Connection db, List<Organization> testOrgs)
        {
            testCount++;
            bool caught = false;
            try
            {// add person that alreadyexists
                db.AddOrganization(testOrgs[0]);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Threw exception of duplicate entry, test Passes.");
                Console.WriteLine("");
                caught = true;
            }
            if (!caught)
            {
                errCount++;
                Console.WriteLine("Allowed addition of duplicate entry, test FAILS.");
                Console.WriteLine("");
            }

            Organization testO = new Organization();
            testO.Name = "Flagship";
            testO.ContactID = 99;

            testCount++;
            caught = false;
            try
            {// use invalid contactID
                db.AddOrganization(testO);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Console.WriteLine("Threw exception for adding with an invalid ContactID, test passes.");
                Console.WriteLine("");
                caught = true;
            }
            if (!caught)
            {
                errCount++;
                Console.WriteLine("Allowed adding with an invalid ContactID, test FAILS.");
                Console.WriteLine("");
                List<Organization> ff = db.GetOrganizations();
                PrintOrganization(ff[ff.Count - 1]);
            }
        }

        public static void testInvalidUpdateOrganization(Connection db)
        {
            // test faliled case: update an organization that doesn't exist
            List<Organization> testList = db.GetOrganizations();
            testCount++;
            bool caught = false;
            Organization testUpdateNotExisted = new Organization();
            testUpdateNotExisted.ID = 250;
            testUpdateNotExisted.Name = "Empty";
            try
            {
                db.UpdateOrganization(testUpdateNotExisted);
            }
            catch (RecordNotFoundException)
            {
                Console.WriteLine("Threw exception for updating a organaization that does not exist (invalid organization ID), test passes.");
                Console.WriteLine("");
                caught = true;
            }
            if (!caught)
            {
                errCount++;
                Console.WriteLine("Allowed for updating a organaization that does not exist (invalid organization ID), test FAILS.");
                Console.WriteLine("");
            }

            testCount++;
            caught = false;
            Organization testUpdateInvalidContactID = new Organization();
            testUpdateInvalidContactID.Name = "testInvalidContactID";
            testUpdateInvalidContactID.ContactID = 99;
            try
            {// use invalid contactID
                db.AddOrganization(testUpdateInvalidContactID);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                Console.WriteLine("Threw exception for updating with invalid ContactID, test passes.");
                Console.WriteLine("");
                caught = true;
            }
            if (!caught)
            {
                errCount++;
                Console.WriteLine("Allowed addition for updating with invalid ContactID, test FAILS.");
                Console.WriteLine("");
            }
        }

        public static void PrintOrganization(Organization o)
        {
            Console.WriteLine("Name {0}, ID {1}, ContactID {2}", o.Name, o.ID, o.ContactID);
        }

    }

