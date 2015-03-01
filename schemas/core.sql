/*
 *	Core.sql implements the tables required for the core functionality of the SQL database.
 *
 *	Each table contains a short description, as well as the name of the table or attribute 
 *	that it most closely corresponds to in the client-supplied ER diagram of the original
 *	database, 'Visio-MEPS.pdf'.
 *
 *	Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
 *	December 01, 2014
 */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('dbo.MemberNumber_UniqueIndex',  'U') 	IS NOT NULL DROP INDEX [MemberNumber_UniqueIndex] 	ON [Member];
IF OBJECT_ID('dbo.PersonSin_UniqueIndex',  'U') 	IS NOT NULL DROP INDEX [PersonSin_UniqueIndex] 		ON [Person];

IF OBJECT_ID('dbo.ChangeLog',      'U') IS NOT NULL DROP TABLE [ChangeLog];
IF OBJECT_ID('dbo.Contribution',   'U')	IS NOT NULL DROP TABLE [Contribution];
IF OBJECT_ID('dbo.Organization',   'U')	IS NOT NULL DROP TABLE [Organization];
IF OBJECT_ID('dbo.PlanMember',     'U') IS NOT NULL DROP TABLE [PlanMember];
IF OBJECT_ID('dbo.Plan',           'U') IS NOT NULL DROP TABLE [Plan];
IF OBJECT_ID('dbo.Member',         'U') IS NOT NULL DROP TABLE [Member];
IF OBJECT_ID('dbo.MemberStatus',   'U')	IS NOT NULL DROP TABLE [MemberStatus];
IF OBJECT_ID('dbo.MemberStanding', 'U') IS NOT NULL DROP TABLE [MemberStanding];
IF OBJECT_ID('dbo.MemberType',     'U') IS NOT NULL DROP TABLE [MemberType];
IF OBJECT_ID('dbo.Person',         'U') IS NOT NULL DROP TABLE [Person];
IF OBJECT_ID('dbo.MaritalStatus',  'U') IS NOT NULL DROP TABLE [MaritalStatus];
GO

/*	
 *	Contains Marital Status of a Person.
 *
 *	See StaticData.sql for values.
 *
 *	Corresponds to the [Marital (Marital Status)] attribute in the
 *	'PERSONS (Persons Master)' Table.
 */
CREATE TABLE [MaritalStatus]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Description]               varchar(200)    NOT NULL,
    
    CONSTRAINT [PK_MaritalStatus]
        PRIMARY KEY ([ID])
);


/*	
 *	Contains Persons and all attributes that may be required for a Person.
 *	This table does not contain Member-specific information.
 *	
 *	Corresponds to the 'PERSONS (Persons Master)' Table.
 */
CREATE TABLE [Person]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Title]                     nvarchar(200)   NULL,
    [FirstName]                 nvarchar(200)   NOT NULL,
    [LastName]                  nvarchar(200)   NOT NULL,
    [Initial]                   nchar(1)        NULL,
    [BirthDate]                 date            NULL,
    [DeathDate]                 date            NULL,
    [Gender]                    char(1)         NULL,
    [Address]                   nvarchar(200)   NULL,
    [PostalCode]                varchar(20)     NULL,
    [City]                      nvarchar(200)   NULL,
    [ProvinceState]             char(2)         NULL,
    [Country]                   nvarchar(200)   NULL,
    [HomePhone]                 varchar(15)     NULL,
    [WorkPhone]                 varchar(15)     NULL,
    [CellPhone]                 varchar(15)     NULL,
    [Fax]                       varchar(15)     NULL,
    [Sin]                       char(9)         NULL,
    [Email]                     varchar(200)    NULL,
    [MaritalStatusID]           int             NULL,
    [Comments]                  varchar(64)     NULL

    CONSTRAINT [PK_Person]
        PRIMARY KEY ([ID]),
    
    CONSTRAINT [FK_Person_MaritalStatus]
        FOREIGN KEY ([MaritalStatusID])
        REFERENCES [MaritalStatus] ([ID]),

    CONSTRAINT [UQ_Person_NameAndBirthDate]
        UNIQUE ([LastName], [FirstName], [BirthDate]),

    CONSTRAINT [CK_Person_Gender]
        CHECK ([Gender] IN ('f', 'm'))		
	
);


/*	
 *	A filtered index on the Person Table for the SIN column.
 *	
 *	This index creates a constraint requiring the SIN to be unique, 
 *	but also allows multiple null values to appear in the column.
 *	Without this, the SIN column cannot:
 *		(a) have a unique constraint, and
 *		(b) allow multiple null values
 */
CREATE UNIQUE INDEX [PersonSin_UniqueIndex]
	ON [Person] ([Sin])
	WHERE [Sin] IS NOT NULL;


/*	
 *	Contains the Member Type of a Member.
 *
 *	Corresponds to the [Type] attribute in the 'PERSONS (Persons Master)' Table.
 *
 *	See StaticData.sql for values.
 */
CREATE TABLE [MemberType]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Description]               varchar(200)    NOT NULL,
    
    CONSTRAINT [PK_MemberType]
        PRIMARY KEY ([ID])
);


/*	
 *	Contains the Member Standing of a Member.
 *
 *	Does not correspond directly to any pre-existing table or attribute,
 *	though it does replace the [Retired (Retired)] attribute in the
 *	'PERSONS (Persons Master)' Table.
 *
 *	See StaticData.sql for values.
 */
CREATE TABLE [MemberStanding]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Description]               varchar(200)    NOT NULL,
    
    CONSTRAINT [PK_MemberStanding]
        PRIMARY KEY ([ID])
);


/*	
 *	Contains the Member Standing of a Member.
 *
 *	Does not correspond directly to any pre-existing table or attribute.
 *
 *	See StaticData.sql for values.
 */
CREATE TABLE [MemberStatus]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Description]               varchar(200)    NOT NULL,
    
    CONSTRAINT [PK_MemberStatus]
        PRIMARY KEY ([ID])
);


/*	
 *	Contains Members and all attributes that may be required for a Member.
 *	This table only contains Member-specific information. To get non-Member
 *	information, a JOIN must be performed between the Member and Person tables.
 *	
 *	Corresponds to the 'PERSONS (Persons Master)' Table.
 */
CREATE TABLE [Member]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [PersonID]                  int             NOT NULL,
    [TypeID]                    int             NULL,
    [StandingID]                int             NULL,
    [StatusID]                  int             NULL,
    [EffectiveDate]             date            NULL,
    [MemberNumber]              varchar(12)     NULL,

    CONSTRAINT [PK_Member]
        PRIMARY KEY ([ID]),

    CONSTRAINT [FK_Member_Person]
        FOREIGN KEY ([PersonID])
        REFERENCES [Person] ([ID])
        ON DELETE CASCADE,
        
    CONSTRAINT [UQ_Member_PersonID]
        UNIQUE ([PersonID]),

    CONSTRAINT [FK_Member_MemberType]
        FOREIGN KEY ([TypeID])
        REFERENCES [MemberType] ([ID]),

    CONSTRAINT [FK_Member_MemberStanding]
        FOREIGN KEY ([StandingID])
        REFERENCES [MemberStanding] ([ID]),

    CONSTRAINT [FK_Member_MemberStatus]
        FOREIGN KEY ([StatusID])
        REFERENCES [MemberStatus] ([ID])
);


/*	
 *	A filtered index on the Member Table for the MemberNumber column.
 *	
 *	This index creates a constraint requiring the MemberNumber to be unique, 
 *	but also allows multiple null values to appear in the column.
 *	Without this, the MemberNumber column cannot:
 *		(a) have a unique constraint, and
 *		(b) allow multiple null values
 */
CREATE UNIQUE INDEX [MemberNumber_UniqueIndex]
	ON [Member] ([MemberNumber])
	WHERE [MemberNumber] IS NOT NULL;


/*	
 *	Contains Benefit Plans and all attributes that may be required for a Benefit Plan.
 *
 *	Does not correspond directly to any pre-existing table or attribute.
 *
 *	See StaticData.sql for values.
 */
CREATE TABLE [Plan]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Name]                      nvarchar(200)   NOT NULL,
    [StandardContributionRate]  decimal(6, 4)   NOT NULL,
    [BusinessNumber]            char(15)        NOT NULL,

    CONSTRAINT [PK_Plan]
        PRIMARY KEY ([ID])
);


/*	
 *	Contains Benefit Plan Members and all attributes that may be required for
 *	a Benefit Plan Member. This table represents a logical relationship between
 *	a Member and a Benefit Plan.
 *	
 *	This assumes that non-Members cannot be Plan Members. If this assumption is false,
 *	then this table will need to be adjusted accordingly.
 *
 *	Corresponds indirectly to the 'PENMEM (Pension Member)' Table, since a pension is
 *	one type of plan.
 */
CREATE TABLE [PlanMember]
(
    [MemberID]                  int             NOT NULL,
    [PlanID]                    int             NOT NULL,
    [StatusID]                  int             NULL,
    [EffectiveDate]             date            NULL,

    CONSTRAINT [PK_PlanMember]
        PRIMARY KEY ([MemberID], [PlanID]),

    CONSTRAINT [FK_PlanMember_Member]
        FOREIGN KEY ([MemberID])
        REFERENCES [Member] ([ID])
        ON DELETE CASCADE,

    CONSTRAINT [FK_PlanMember_Plan]
        FOREIGN KEY ([PlanID])
        REFERENCES [Plan] ([ID]),

    CONSTRAINT [FK_PlanMember_MemberStatus]
        FOREIGN KEY ([StatusID])
        REFERENCES [MemberStatus] ([ID])
);


/*	
 *	Contains Organizations and all attributes that may be required for an Organization.
 *
 *	Corresponds directly to the 'ORGANS (Organizations Master)' Table.
 */
CREATE TABLE [Organization]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Name]                      nvarchar(200)   NOT NULL,
    [ContactID]                 int             NULL,

    CONSTRAINT [PK_Organization]
        PRIMARY KEY ([ID]),
        
    CONSTRAINT [UQ_Organization_Name]
        UNIQUE ([Name]),

    CONSTRAINT [FK_Organization_Person]
        FOREIGN KEY ([ContactID])
        REFERENCES [Person] ([ID])
        ON DELETE SET NULL
);


/*	
 *	Contains Contributions and all attributes that may be required for a Contribution.
 *
 *	Corresponds directly to the 'CONTRIBS (Contributions)' Table.
 */
CREATE TABLE [Contribution]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [MemberID]                  int             NOT NULL,
    [PlanID]                    int             NOT NULL,
    [OrganizationID]            int             NOT NULL,
    [EntryDate]                 date            NOT NULL,
    [EffectiveDate]             date            NOT NULL,
    [BatchID]                   varchar(12)     NOT NULL,
    [Units]                     int             NOT NULL,
    [Rate]                      int             NOT NULL,

    CONSTRAINT [PK_Contribution]
        PRIMARY KEY ([ID]),

    CONSTRAINT [FK_Contribution_PlanMember]
        FOREIGN KEY ([MemberID], [PlanID])
        REFERENCES [PlanMember] ([MemberID], [PlanID])
        ON DELETE CASCADE,

    CONSTRAINT [FK_Contribution_Organization]
        FOREIGN KEY ([OrganizationID])
        REFERENCES [Organization] ([ID])
        ON DELETE CASCADE
);


/*
 *	Contains history events and all attributes required for a history event.
 *	A new history event will be created whenever there is a change to an attribute,
 *	with a new event for every attribute (if multiple attributes are changed at once).
 *	History events will be recorded whenever there is a change to an attribute in
 *	any of the following tables:
 *		(a) Person
 *		(b) Member
 *		(c) Organization
 *		(d) PlanMember
 *
 *	Corresponds to the 'PERSHIST (Persons History)' Table, but contains history events
 *	for more than just the Persons Table (see above).
 */
CREATE TABLE [ChangeLog]
(
    [ID]                        int             NOT NULL IDENTITY(1, 1),
    [Time]                      datetime        NOT NULL,
    [Table]                     nvarchar(50)    NOT NULL,
    [RecordID]                  int             NOT NULL,
    [Field]                     nvarchar(50)    NOT NULL,
    [OldValue]                  nvarchar(200)   NULL,
    [NewValue]                  nvarchar(200)   NULL,
    [User]                      nvarchar(200)   NOT NULL

    CONSTRAINT [PK_ChangeLog]
        PRIMARY KEY ([ID]),
);


GO