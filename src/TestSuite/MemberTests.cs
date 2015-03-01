using API;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// MemberTests.cs is a part of the TestSuite for the API. It runs tests on Member functions.
/// It tests for things that are expected to pass as well as things that are expected to fail. It tests adding,
/// updating and finding Members.
/// 
/// It also has an MemberEquals method to see if two Member Objects are equal.
/// When finished, it reports on the number of tests run, passed and Failed.
/// 
/// It requires a valid Connection object to the database.
/// 
/// Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
/// December 01, 2014
/// </summary>
static class MemberTests
{
    static int errCount = 0;
    static int testCount = 0;
    public static string stars = "**********************************************************************";

    public static int RunMemberTests(Connection db)
    {
        List<Member> members = db.GetMembers();

        Console.WriteLine();
        Console.WriteLine("Now Running Member Tests");
        Console.Error.WriteLine("Now Running Member Tests\n");

        TestAddMember(db);

        TestAddMember2(db);

        TestGetMembers(db);

        TestGetMember(db);

        TestGetMemberID(db);

        TestUpdateMember(db);

        WriteMemberResults();

        return errCount;
    }


    static void WriteMemberResults()
    {
        Console.WriteLine();
        Console.WriteLine("Completed Member Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
        Console.WriteLine();
        Console.WriteLine("{0}", stars);

        Console.Error.WriteLine("Completed Member Tests.  PASSED: {0}\t FAILED: {1}\t TESTS RUN: {2}", testCount - errCount, errCount, testCount);
        Console.Error.WriteLine("\n{0}\n", stars);
    }


    static void TestUpdateMember(Connection db)
    {
        bool caught = false;
        List<Member> members = db.GetMembers();

        testCount++;
        Console.WriteLine();

        Member temp = members[1];
        temp.Person.FirstName = "Wilma";
        temp.Person.LastName = "Rubble";
        temp.Person.Initial = 'X';
        temp.Person.HomePhone = "6665554489";
        temp.Person.DeathDate = new DateTime(2000, 02, 28);

        db.UpdateMember(temp);
        if (MemberEquals(db.GetMember(temp.ID), temp))
        {
            Console.WriteLine();
            Console.WriteLine("UpdateMember() with valid data passed");
        }
        else
        {
            errCount++;
            Console.WriteLine();
            Console.WriteLine("UpdateMember() with valid data FAILED");
        }

        testCount++;
        try
        {
            temp.Person = new Person(); 
            db.UpdateMember(temp);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using UpdateMember() with an invalid PersonID. Test PASSED");
        }
       /* catch (System.InvalidOperationException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using UpdateMember() with an invalid PersonID. Test PASSED");
        } */
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using UpdateMember() with an invalid PersonID. Test FAILED");
            errCount++;
        }

        return;

    }


    static void TestGetMember(Connection db)
    {
        List<Member> members = db.GetMembers();
        bool caught = false;

        testCount++;

        if (MemberEquals( members[0], db.GetMember(members[0].ID)) )
        {
            Console.WriteLine();
            Console.WriteLine("GetMember(id) with valid data PASSED");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("GetMember(id) with valid data FAILED");
            errCount++;
        }

        testCount++;

        try
        {
            db.GetMember(200);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using GetMember(id) with invalid id. Test PASSED");
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using GetMember(id) with invalid id. Test FAILED");
            errCount++;
        }

        return;
    }


    static void TestGetMemberID(Connection db)
    {
        bool caught = false;
        List<Member> members = db.GetMembers();

        testCount++;
        if (db.GetPersonID(members[0].Person.LastName, members[0].Person.FirstName, (DateTime)members[0].Person.BirthDate) == members[0].ID)
        {
            Console.WriteLine();
            Console.WriteLine("GetMemberID() with valid data PASSED");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("GetMemberID() with valid data FAILED");
            errCount++;
        }

        testCount++;
        try
        {
            db.GetPersonID("fdsafdsfsd", "fdsfds", (DateTime)members[0].Person.BirthDate);
        }
        catch (RecordNotFoundException)
        {
            caught = true;
            Console.WriteLine();
            Console.WriteLine("Exception thrown when using GetMemberID() with invalid data. Test PASSED");
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("Exception not thrown when using GetMemberID() with invalid data. Test FAILED");
            errCount++;
        }

        return;
    }


    static void TestAddMember(Connection db)
    {
        bool caught = false;

        List<Person> persons = db.GetPersons();

        Member m = new Member();
        m.PersonID = persons[ persons.Count -1 ].ID;

        testCount++;
        try
        {
            db.AddMember(m);
        }
        catch (Exception)
        {
            caught = true;
            errCount++;
            Console.WriteLine();
            Console.WriteLine("AddMember() with a valid member and person test FAILED");
        }

        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("AddMember() with a valid member and person test Passed");
        }

        try
        {
            testCount++;
            db.AddMember(m);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when adding duplicate member, test PASSED");
            caught = true;
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("No exception thrown when adding duplicate member, test FAILED");
        }

        return;
    }


    static void TestAddMember2(Connection db)
    {
        bool caught = false;
        Person p1 = new Person();


        p1.FirstName = "Arnold";
        p1.LastName = "Schwarzenagger";
        p1.Initial = 'A';
        p1.BirthDate = new DateTime(1943, 10, 15);
        p1.Title = "Mr.";
        p1.Gender = 'm';
        p1.Address = "123 Main St";
        p1.PostalCode = "v5c2e5";
        p1.City = "Vancouver";
        p1.ProvinceState = "BC";
        p1.HomePhone = "0123456789";

        Person p2 = new Person();

        p2.FirstName = "Arnold";
        p2.LastName = "Toynbee";
        p2.Initial = 'v';
        p2.BirthDate = new DateTime(1977, 10, 15);
        p2.Title = "Mr.";
        p2.Gender = 'm';
        p2.Address = "123 Main St";
        p2.PostalCode = "v5c2e5";
        p2.City = "Vancouver";
        p2.ProvinceState = "BC";
        p2.HomePhone = "0123456789";

        testCount++;
        try
        {
            Console.WriteLine();
            Console.WriteLine("Testing AddMember(Member, Person) with valid data");
            db.AddMember(p1, new Member());
        }
        catch (Exception)
        {
            caught = true;
            errCount++;
            Console.WriteLine();
            Console.WriteLine("AddMember(Member, Person) test FAILED");
        }

        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("AddMember(Member, Person) test Passed");
        }

        try
        {
            testCount++;
            Console.WriteLine();
            Console.WriteLine("Testing AddMember(Member, Person) when person is already a member");
            db.AddMember(p1, new Member());
        }
        catch (DuplicateRecordException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when person is already member, test PASSED");
            caught = true;
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when person is already member, test PASSED");
            caught = true;
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("No exception thrown when person is already member, test FAILED");
        }

        try
        {
            testCount++;
            Console.WriteLine();
            Console.WriteLine("Testing AddMember(Member, Person) when person exists already");
            db.AddMember(p2, new Member());
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine();
            Console.WriteLine("Exception thrown when person exists already, test PASSED");
            caught = true;
        }
        if (!caught)
        {
            Console.WriteLine();
            Console.WriteLine("No exception thrown when person exists already, test FAILED");
        }

        return;
    }


    static void TestGetMembers(Connection db)
    {
        Console.WriteLine("Testing GetMembers...");
        Console.WriteLine();

        testCount++;
        List<Member> list = db.GetMembers();
        if (list.Count == 0)
        {
            Console.WriteLine("Testing GetMembers Failed");
            Console.WriteLine();
            errCount++;
        }
        else
        {
            Console.WriteLine("Testing GetMembers Passed");
            Console.WriteLine();
        }

        return;
    }


    public static bool MemberEquals(Member m1, Member m2)
    {
        return m1.ID == m2.ID
             && PersonTests.PersonEquals(m1.Person, m2.Person)
             && m1.MemberNumber == m2.MemberNumber || m1.MemberNumber.ToLower().Equals(m2.MemberNumber)
             && m1.StandingID == m2.StandingID
             && m1.StatusID == m2.StatusID
             && m1.TypeID == m2.TypeID
             && m1.EffectiveDate == m2.EffectiveDate;
    }


    static void PrintMemberList(List<Member> m)
    {
        for (int i = 0; i < m.Count; i++)
        {
            Console.WriteLine("MemberId {0} Name {1} {2} address {3}",
                m[i].ID, m[i].Person.FirstName, m[i].Person.LastName, m[i].Person.Address);
        }
    
    }


    static Member MakeMemberFromPerson( Person p)
    {
        Member m = new Member();
        m.PersonID = p.ID;
        return m;
    }
}