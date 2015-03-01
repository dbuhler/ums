# Union Management System
*By Jeff Bayntun, Dan Buhler, Tyler Hlynsky, and Sarah Wu*

This is a basic user management system created for our client, *The Operative Plasterers’ and Cement Masons’ International Association Local 919*, as our COMP 3900 term project.

The project consists of a database design, an API for using the database, a test suite, and a basic web interface. It is only partially completed at present. The current iteration includes the core components of the database for union members. The API provies basic CRUD functionality and is meant to be extended in the future to define the business rules surrounding the use of the database.

The database has been deployed and tested on Microsoft SQL Server 2012. The API and test suite are written in C#. The API is using LINQ to communicate with the database. The web interface is an ASP.NET web application demonstrating the usage of the API.

* [/schemas](/schemas) - SQL schemas for the database
* [/src](/src) - Visual Studio 2012 project solution
