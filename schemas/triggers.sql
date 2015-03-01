/*
 *	Triggers.sql implements triggers for performing actions upon changes in the database.
 *
 *	This currently only contains triggers related to storing information for the ChangeLog
 *	whenever there is a change to one of the specified tables (see ChangeLog in Core.sql).
 *
 *	Designed and Written by: Dan Buhler, Jeff Bayntun, Tyler Hlynsky, Sarah Wu (BCIT)
 *	December 01, 2014
 */

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('dbo.tr_MemberDelete', 'TR') IS NOT NULL DROP TRIGGER   [tr_MemberDelete];
IF OBJECT_ID('dbo.tr_MemberUpdate', 'TR') IS NOT NULL DROP TRIGGER   [tr_MemberUpdate];
IF OBJECT_ID('dbo.tr_MemberInsert', 'TR') IS NOT NULL DROP TRIGGER   [tr_MemberInsert];
IF OBJECT_ID('dbo.tr_PersonDelete', 'TR') IS NOT NULL DROP TRIGGER   [tr_PersonDelete];
IF OBJECT_ID('dbo.tr_PersonUpdate', 'TR') IS NOT NULL DROP TRIGGER   [tr_PersonUpdate];
IF OBJECT_ID('dbo.tr_PersonInsert', 'TR') IS NOT NULL DROP TRIGGER   [tr_PersonInsert];
IF OBJECT_ID('dbo.sp_AddLogEntries', 'P') IS NOT NULL DROP PROCEDURE [sp_AddLogEntries];
IF OBJECT_ID('dbo.sp_GetValue',      'P') IS NOT NULL DROP PROCEDURE [sp_GetValue];   
GO


/*
 *	Gets a value from a table, specified by table name, column name, and record ID.
 *
 *	The value is returned through the parameter @Value.
 */
CREATE PROCEDURE [sp_GetValue]
(
    @Table  nvarchar(50),
    @ID     int,
    @Column nvarchar(50),
    @Value  nvarchar(200) OUT
)
AS BEGIN
    DECLARE @Query nvarchar(200) = CONCAT(
        N'SELECT @Value = [' + @Column + N'] ' +
        N'FROM [' + @Table + N'] '             +
        N'WHERE [ID] = ', @ID);

    EXECUTE sp_executesql
        @Query,
        N'@Value nvarchar(200) OUT',
        @Value OUT;
END;
GO


/*	
 *	Loops through all affected rows of a table, using two temporary tables
 *	that store the old values and the new values, and checks for each column
 *	whether the value has changed. 
 *
 *	If the value has changed, an entry to the ChangeLog table is added.
 */
CREATE PROCEDURE [sp_AddLogEntries]
(
    @Table     nvarchar(50),
    @OldValues nvarchar(50),
    @NewValues nvarchar(50)
)
AS BEGIN
    DECLARE @Date     datetime = GETDATE();
    DECLARE @ID       int;
    DECLARE @Column   nvarchar(50);
    DECLARE @OldValue nvarchar(200);
    DECLARE @NewValue nvarchar(200);
    DECLARE @User     nvarchar(200) = SUSER_SNAME();
    DECLARE @Query    nvarchar(200);
    DECLARE @Result   TABLE ([ID] int);

    IF @NewValues IS NOT NULL
        -- This is the case for INSERT and UPDATE.
        INSERT INTO @Result EXECUTE (N'SELECT [ID] FROM [' + @NewValues + N']');
    ELSE
        -- This is the case for DELETE.
        INSERT INTO @Result EXECUTE (N'SELECT [ID] FROM [' + @OldValues + N']');

    DECLARE [IDs]     CURSOR READ_ONLY FOR SELECT * FROM @Result;
    DECLARE [Columns] CURSOR READ_ONLY FOR 
        SELECT [COLUMN_NAME]
        FROM   [INFORMATION_SCHEMA].[COLUMNS]
        WHERE  [TABLE_NAME] = @Table;

    -- Loop through all affected rows.
    OPEN [IDs];
    FETCH NEXT FROM [IDs] INTO @ID;
    WHILE @@FETCH_STATUS = 0 BEGIN

        -- Loop through all columns.
        OPEN [Columns];
        FETCH NEXT FROM [Columns] INTO @Column;
        WHILE @@FETCH_STATUS = 0 BEGIN

            -- Get the old value for this row and column or use
            -- NULL if no old value exists (in case of INSERT).
            IF @OldValues IS NULL SET @OldValue = NULL
            ELSE EXECUTE [sp_GetValue] @OldValues, @ID, @Column, @OldValue OUT;
            
            -- Get the new value for this row and column or use
            -- NULL if no new value exists (in case of DELETE).
            IF @NewValues IS NULL SET @NewValue = NULL
            ELSE EXECUTE [sp_GetValue] @NewValues, @ID, @Column, @NewValue OUT;

            -- Write a log entry if the value has changed.
            IF (@OldValue IS NULL AND @NewValue IS NOT NULL) OR
               (@NewValue IS NULL AND @OldValue IS NOT NULL) OR
               (@OldValue != @NewValue)

                INSERT INTO [ChangeLog]
                (
                    [Time],
                    [Table],
                    [RecordID],
                    [Field],
                    [OldValue],
                    [NewValue],
                    [User]
                )
                VALUES
                (
                    @Date,
                    @Table,
                    @ID,
                    @Column,
                    @OldValue,
                    @NewValue,
                    @User
                );

            FETCH NEXT FROM [Columns] INTO @Column;
        END;
        CLOSE [Columns];

        FETCH NEXT FROM [IDs] INTO @ID;
    END;
    CLOSE [IDs];

    DEALLOCATE [IDs];
    DEALLOCATE [Columns];
END;
GO


/*
 *	Creates a new ChangeLog entry when information is inserted into the Person Table.
 */
CREATE TRIGGER [tr_PersonInsert]
ON [Person] FOR INSERT
AS BEGIN
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'Person', NULL, N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is updated in the Person Table.
 */
CREATE TRIGGER [tr_PersonUpdate]
ON [Person] FOR UPDATE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'Person', N'#OldValues', N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is deleted from the Person Table.
 */
CREATE TRIGGER [tr_PersonDelete]
ON [Person] FOR DELETE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    EXECUTE [sp_AddLogEntries] N'Person', N'#OldValues', NULL;
END;
GO


/*
 *	Creates a new ChangeLog entry when information is inserted into the Member Table.
 */
CREATE TRIGGER [tr_MemberInsert]
ON [Member] FOR INSERT
AS BEGIN
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'Member', NULL, N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is updated in the Member Table.
 */
CREATE TRIGGER [tr_MemberUpdate]
ON [Member] FOR UPDATE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'Member', N'#OldValues', N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is deleted from the Member Table.
 */
CREATE TRIGGER [tr_MemberDelete]
ON [Member] FOR DELETE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    EXECUTE [sp_AddLogEntries] N'Member', N'#OldValues', NULL;
END;
GO


/*
 *	Creates a new ChangeLog entry when information is inserted into the PlanMember Table.
 */
CREATE TRIGGER [tr_PlanMemberInsert]
ON [PlanMember] FOR INSERT
AS BEGIN
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'PlanMember', NULL, N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is updated in the PlanMember Table.
 */
CREATE TRIGGER [tr_PlanMemberUpdate]
ON [PlanMember] FOR UPDATE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    SELECT * INTO [#NewValues] FROM [inserted];
    EXECUTE [sp_AddLogEntries] N'PlanMember', N'#OldValues', N'#NewValues';
END;
GO


/*
 *	Creates a new ChangeLog entry when information is deleted from the PlanMember Table.
 */
CREATE TRIGGER [tr_PlanMemberDelete]
ON [PlanMember] FOR DELETE
AS BEGIN
    SELECT * INTO [#OldValues] FROM [deleted];
    EXECUTE [sp_AddLogEntries] N'PlanMember', N'#OldValues', NULL;
END;
GO