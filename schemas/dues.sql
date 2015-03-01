SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF OBJECT_ID('dbo.Charge', 'U') IS NOT NULL DROP TABLE [Charge];
IF OBJECT_ID('dbo.Payment','U') IS NOT NULL DROP TABLE [Payment];


CREATE TABLE [Payment]
(
    [id]                int             NOT NULL IDENTITY(1, 1),
    [member_id]         int             NOT NULL,
    [entry_date]        date            NOT NULL,
    [payment_type]      varchar(10)     NOT NULL,
    -- CHECK (payment_type IN('Cash', 'Cheque', 'Employer', 'Manager')),
    [reference_number]  varchar(12)     NOT NULL,
    [amount]            money           NOT NULL,

    PRIMARY KEY ([id]),
    FOREIGN KEY ([member_id]) REFERENCES [Member] ([id])
);


CREATE TABLE [Charge]
(
    [id]                int             NOT NULL IDENTITY(1, 1),
    [member_id]         int             NOT NULL,
    [entry_date]        date            NOT NULL,
    [charge_type]       varchar(10)     NOT NULL,
    -- CHECK (charge_type IN('initiation', 'dues', 'death assessment')),
    [amount]            money           NOT NULL,

    PRIMARY KEY ([id]),
    FOREIGN KEY ([member_id]) REFERENCES [Member] ([id])
);