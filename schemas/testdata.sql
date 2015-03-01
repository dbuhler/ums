SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

DELETE FROM [Organization];
DELETE FROM [Member];
DELETE FROM [Person];

INSERT INTO [Person] ([FirstName], [LastName], [Initial], [BirthDate], [Title], [Gender], [Address], [PostalCode], [City], [ProvinceState], [HomePhone] ) VALUES
    ( 'Bobby', 'Joejay', 'A', '1903-10-15', 'Mr.', 'm', '123 Main St', 'v5c2e5', 'Vancouver', 'BC', '0123456789' ),
    ( 'Fred', 'Flintstone', 'C', '1945-02-28', 'Mr.', 'm', '250 here st', 'v5c5t6', 'Surrey', 'BC', '6043334444'),
    ( 'Ned', 'Stark', 'B', '1990-05-05', 'Mr.', 'm', '123 Pleasant way', 'v5c255', 'Vancouver', 'BC', '2506665243' );


INSERT INTO [Member] ([PersonID]) VALUES
    ( (SELECT [ID] FROM [Person] WHERE [FirstName] = 'Bobby') ),
    ( (SELECT [ID] FROM [Person] WHERE [FirstName] = 'Fred') ),
	( (SELECT [ID] FROM [Person] WHERE [FirstName] = 'Ned') );
