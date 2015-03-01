/*
 *	StaticData.sql implements pre-defined values for any attributes that require them.
 *
 *	Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
 *	December 01, 2014
 */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

DELETE FROM [MaritalStatus];
DELETE FROM [MemberStanding];
DELETE FROM [MemberStatus];
DELETE FROM [MemberType];
GO

/*
 *	Pre-defined values for Marital Status in the Marital Status Table. 
 */
INSERT INTO [MaritalStatus] ([Description]) VALUES
    ( 'Single' ),
    ( 'Married' ),
    ( 'Divorced' ),
    ( 'Widowed' );


/*
 *	Pre-defined values for Member Standing in the MemberStanding Table. 
 */
INSERT INTO [MemberStanding] ([Description]) VALUES
    ( 'Current' ),
    ( 'Delinquent' ),
	( 'Suspended' ),
	( 'Prospect' ),
	( 'Resigned' ),
	( 'Expelled' );


/*
 *	Pre-defined values for Member Status in the MemberStatus Table. 
 */
INSERT INTO [MemberStatus] ([Description]) VALUES
    ( 'Active' ),
	( 'Deceased' ),
    ( 'Transferred' );


/*
 *	Pre-defined values for Member Type in the MemberType Table. 
 */
INSERT INTO [MemberType] ([Description]) VALUES
    ( 'Associate' ),
    ( 'Regular' ),
	( 'Prospective' ),
	( 'Executive' );
GO