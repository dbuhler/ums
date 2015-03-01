/*
 *	Views.sql implements commonly used views for the SQL database.
 *
 *	Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
 *	December 01, 2014
 */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('dbo.MemberPerson', 'V') IS NOT NULL DROP VIEW [MemberPerson];
GO

/*
 *	Merges the Person and Member Tables into one view table.
 */	
CREATE VIEW [MemberPerson] AS
    SELECT
        [Member].[ID],
        [Member].[PersonID],
        [Member].[TypeID],
        [Member].[StandingID],
        [Member].[StatusID],
        [Member].[EffectiveDate],
        [Member].[MemberNumber],
        [Person].[Title],
        [Person].[FirstName],
        [Person].[LastName],
        [Person].[Initial],
        [Person].[BirthDate],
	    [Person].[DeathDate],
	    [Person].[Gender],
        [Person].[Address],
        [Person].[PostalCode],
        [Person].[City],
        [Person].[ProvinceState],
        [Person].[Country],
        [Person].[HomePhone],
	    [Person].[WorkPhone],
        [Person].[CellPhone],
	    [Person].[Fax],
        [Person].[Sin],
        [Person].[Email],
	    [Person].[MaritalStatusID],
        [Person].[Comments]
    FROM [Member] INNER JOIN [Person]
    ON [Member].[PersonID] = [Person].[ID];

GO