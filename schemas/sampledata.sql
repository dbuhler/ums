SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

DELETE FROM [Organization];
DELETE FROM [Member];
DELETE FROM [Person];

INSERT INTO [Person] ([FirstName], [LastName], [Initial], [BirthDate], [Title], [Gender], [Address], [PostalCode], [City], [ProvinceState], [HomePhone], [MaritalStatusID], [Email] ) VALUES
    ( 'Bobby', 'Joejay', 'B', '1903-10-15', 'Mr.', 'm', '123 Main St', 'v3t2b5', 'Vancouver', 'BC', '0123456789', 2, 'Bobby@hotmail.com' ),
    ( 'Fred', 'Flintstone', 'F', '1945-02-28', 'Mr.', 'm', '250 here st', 'v3t5t6', 'Surrey', 'BC', '6043334444', 2, 'Fred@gmail.com' ),
	( 'William', 'Brown', 'W', '1967-10-15', 'Miss', 'f', '78 Var St', 'v3c7e1', 'Burnaby', 'BC', '6046678987', 2, 'William123@gmail.com' ),
	( 'Jessica', 'Wang', 'J', '1945-02-28', 'Miss', 'f', '5 Bay st', 'v3i5t6', 'Surrey', 'BC', '6043334121', 2, 'Jessica5@hotmail.com' ),
    ( 'Lucy', 'Archer', 'L', '2000-10-15', 'Miss', 'f', '12 Main St', 'v6d3e9', 'Richmand', 'BC', '7183456789', 1, 'Lucy@gmail.com' ),
	( 'Nathan', 'Archer', 'N', '1970-07-05', 'Mr.', 'm', '12 Main St', 'v6d3e9', 'Richmand', 'BC', '7183456789', 2, 'Nathan8@hotmail.com' ),
	( 'Amanda', 'Archer', 'A', '1970-09-05', 'Mrs.', 'f', '12 Main St', 'v6d3e9', 'Richmand', 'BC', '7183456789', 2, 'Amanda@hotmail.com' ),
    ( 'Charles', 'Drew', 'D', '1975-11-08', 'Mr.', 'm', '8 Int St', 'v1g2k3', 'Burnaby', 'BC', '6045656787', 2, 'Charles@hotmail.com' ),
	( 'Carter', 'Woodson', 'C', '1990-02-07', 'Mr.', 'm', '36 Binary St', 'v8d2u7', 'Surrey', 'BC', '604456789', 2, 'Carter@hotmail.com' ),
	( 'linda', 'Drew', 'L', '2003-12-08', 'Miss', 'f', '8 Int St', 'v1g2k3', 'Burnaby', 'BC', '6045656787', 1, 'Charles@hotmail.com' ),
	( 'John', 'Luther', 'J', '1900-10-15', 'Mr.', 'm', '333 Double St', 'v7t9b6', 'Vancouver', 'BC', '718889898', 1, 'John@hotmail.com' ),
	( 'Alice', 'Luther', 'A', '1900-10-15', 'Miss', 'f', '333 Double St', 'v2t9d6', 'Vancouver', 'BC', '718877898', 1, 'Alice@gmail.com' ),
	( 'Tyrion', 'Lannister', 'T', '1989-06-28', 'Mr.', 'm', '256 Long St', 'v2r9r6', 'Vancouver', 'BC', '604877898', 1, 'Tyrion@gmail.com' ),
	( 'Justin', 'Brown', 'J', '1902-05-17', 'Mr.', 'm', '1024 String St', 'v1t9b8', 'Vancouver', 'BC', '718089808', 1, 'Justin@hotmail.com' );

INSERT INTO [Member] ([PersonID], [TypeID], [StandingID], [StatusID], [EffectiveDate], [MemberNumber]) VALUES
    ( 1, 2, 1, 2, '1980-09-16', 'UA11' ),
	( 2, 1, 1, 1, '2012-10-15', 'UA23' ),
	( 3, 2, 1, 1, '1985-08-20', 'UA12' ),
	( 4, 2, 1, 1, '1990-09-15', 'UA50' ),
	( 6, 2, 1, 1, '1990-10-15', 'UA47' ),
	( 8, 2, 1, 1, '2001-08-15', 'UA67' ),
	( 9, 2, 2, 3, '1999-03-22', 'UB33' );

INSERT INTO [Organization] ([Name], [ContactID]) VALUES
    ( 'Ama', 1 ),
	( 'Bna', 3 ),
	( 'Cla', 4 ),
	( 'dsu', 6 );
	

