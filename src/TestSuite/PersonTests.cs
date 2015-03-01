using API;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
/// <summary>
/// PersonTests.cs is a part of the TestSuite for the API.  It runs tests on Person functions.
/// It tests for things that are expected to pass as well as things that are expected to fail.  It tests adding,
/// updating and finding Persons.
/// 
/// It also has PersonEquals method to see if two Person Objects are equal.
/// When finished, it reports on the number of tests run, passed and Failed.
/// 
/// It requires a valid Connection object to the database.
/// 
/// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
/// December 01, 2014
/// </summary>
static class PersonTests
{
    static int errCount = 0;
    static int testCount = 0;
    public static string stars = "**********************************************************************";

    public static int RunPersonTests(Connection db)
    {

        Console.WriteLine();
        Console.WriteLine("Now Running Person Tests");
        Console.Error.WriteLine("Now Running Person Tests\n");

        List<Person> testPersons = MakePersons();
        List<Person> persons = db.GetPersons();

        TestGetPersons(db, testPersons);

        TestAddPerson(db);

        TestGetPerson(db, testPersons);

        TestGetPersonID(db, persons);

        TestUpdatePerson(db, persons);

        WritePersonResults();

        return errCount;
    }

    static void WritePersonResults()
    {
        Console.WriteLine();
        Console.WriteLine("Completed Person Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
        Console.WriteLine();
        Console.WriteLine("{0}", stars);

        Console.Error.WriteLine("Completed Person Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
        Console.Error.WriteLine("\n{0}\n", stars);
    }

    static void TestUpdatePerson(Connection db, List<Person> persons)
    {
        bool caught = false;
        testCount++;
        Console.WriteLine();
        Console.WriteLine("Testing UpdatePerson() with valid data");

        Person temp = persons[1];
        temp.FirstName = "Wilma";
        temp.LastName = "Rubble";
        temp.Initial = 'X';
        temp.HomePhone = "6665554489";
        temp.DeathDate = new DateTime(2000, 02, 28);
        
        db.UpdatePerson(temp);
 
        if (PersonEquals(db.GetPerson(temp.ID), temp))
        {
            Console.WriteLine();
            Console.WriteLine("UpdatePerson() with valid data passed");
        }
        else
        {
            errCount++;
            Console.WriteLine();
            Console.WriteLine("UpdatePerson() with valid data FAILED");
        }

        testCount++;

        try
        {
            temp = new Person();
            temp.ID = 777;
            temp.FirstName = "harriet";
            temp.LastName = "Janus";
            temp.BirthDate = new DateTime(1988, 05, 05);
            db.UpdatePerson(temp);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using UpdatePerson() with invalid id. Test PASSED");
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using UpdatePerson() with invalid id. Test FAILED");
            errCount++;
        }

        return;

    }

    static void TestGetPerson(Connection db, List<Person> testPersons)
    {
        bool caught = false;

        testCount++;

        if (PersonEquals(testPersons[0], db.GetPerson(testPersons[0].ID)))
        {
            Console.WriteLine();
            Console.WriteLine("GetPerson(id) with valid id PASSED");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("GetPerson(id) with valid id FAILED");
            errCount++;
        }

        testCount++;

        try
        {
            db.GetPerson(200);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using GetPerson(id) with invalid id. Test PASSED");
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using GetPerson(id) with invalid id. Test FAILED");
            errCount++;
        }

        return;
    }

    static void TestGetPersonID(Connection db, List<Person> persons)
    {
        bool caught = false;

        testCount++;

        if ( db.GetPersonID(persons[0].LastName, persons[0].FirstName, (DateTime) persons[0].BirthDate) == persons[0].ID )
        {
            Console.WriteLine();
            Console.WriteLine("GetPersonID() with valid data PASSED");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("GetPersonID() with valid data FAILED");
            errCount++;
        }

        testCount++;

        try
        {
            db.GetPersonID("fdsafdsfsd", "fdsfds", (DateTime)persons[0].BirthDate);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using GetPersonID() with invalid data. Test PASSED");
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using GetPersonID() with invalid data. Test FAILED");
            errCount++;
        }

        return;
    }

    static void TestAddPerson(Connection db)
    {
        bool caught = false;

        Person p = new Person();
        p.LastName = "Wilfred";
        p.FirstName = "John";
        p.BirthDate = new DateTime(1988, 10, 22);

        testCount++;
        try
        {
            db.AddPerson(p);
        }
        catch (Exception)
        {
            caught = true;
            errCount++;
            Console.WriteLine();
            Console.WriteLine("AddPerson() with valid Person test FAILED");
        }

        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("AddPerson() with valid Person test Passed");
        }

        try
        {
            testCount++;
            db.AddPerson(p);
        }
        catch (DuplicateRecordException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when adding duplicate person, test PASSED");
            caught = true;
        }
  /*      catch (InvalidOperationException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when adding duplicate person, test PASSED");
            caught = true;
        } */
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("No exception thrown when adding duplicate person, test FAILED");
        }

        return;
    }

    static void TestGetPersons(Connection db, List<Person> pp)
    {
        Console.WriteLine("Verifying initial datbase data, as well as the GetPersons() Function...\n");
        Console.WriteLine();
        List<Person> persons = db.GetPersons();

        if (pp.Count != persons.Count)
        {
            Console.WriteLine("Person count {0} is and pp count is {1}.  They should match.  Test case Fail.\n", persons.Count, pp.Count);
            errCount++;
        }
        else
            Console.WriteLine("Person Count is correct, Test Passed\n");
        testCount++;

        Console.WriteLine();
        for (int i = 0; i < persons.Count && i < pp.Count; i++)
        {
            bool a = PersonEquals(persons[i], pp[i]);
            if (!a)
            {
                Console.WriteLine("Person in list number {0} is not equal. Test case failed\n", i);
                errCount++;
            }
            else
                Console.WriteLine("Person {0} equal, Test Passed\n", i);
            testCount++;
        }

        return;
    }

    static void PrintPersonList(List<Person> pp)
    {
        for (int i = 0; i < pp.Count; i++)
        {
            Console.WriteLine("Id {0} Name {1} {2} address {3}", pp[i].ID, pp[i].FirstName, pp[i].LastName, pp[i].Address);
        }
    }

    static List<Person> MakePersons()
    {
        List<Person> pp = new List<Person>();
        Person p1 = new Person();
        p1.ID = 1;
        p1.FirstName = "Bobby";
        p1.LastName = "Joejay";
        p1.Initial = 'A';
        p1.BirthDate = new DateTime(1903, 10, 15);
        p1.Title = "Mr.";
        p1.Gender = 'm';
        p1.Address = "123 Main St";
        p1.PostalCode = "v5c2e5";
        p1.City = "Vancouver";
        p1.ProvinceState = "BC";
        p1.HomePhone = "0123456789";

        Person p2 = new Person();
        p2.ID = 2;
        p2.FirstName = "Fred";
        p2.LastName = "Flintstone";
        p2.Initial = 'C';
        p2.BirthDate = new DateTime(1945, 02, 28);
        p2.Title = "Mr.";
        p2.Gender = 'm';
        p2.Address = "250 here St";
        p2.PostalCode = "v5c5t6";
        p2.City = "Surrey";
        p2.ProvinceState = "BC";
        p2.HomePhone = "6043334444";

        Person p3 = new Person();
        p3.ID = 3;
        p3.FirstName = "Ned";
        p3.LastName = "Stark";
        p3.Initial = 'B';
        p3.BirthDate = new DateTime(1990, 05, 05);
        p3.Title = "Mr.";
        p3.Gender = 'm';
        p3.Address = "123 Pleasant way";
        p3.PostalCode = "v5c255";
        p3.City = "Vancouver";
        p3.ProvinceState = "BC";
        p3.HomePhone = "2506665243";

        // now ordered alphabetical by last name
        pp.Add(p2);
        pp.Add(p1);
        pp.Add(p3);

        return pp;
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
        && (p1.Comments == p2.Comments || p1.Comments.ToLower().Equals(p2.Comments.ToLower()))
        && (p1.Email == p2.Email || p1.Email.ToLower().Equals(p2.Email.ToLower()))
        && (p1.MaritalStatusID == p2.MaritalStatusID);
    }
}
