CREATE TABLE [dbo].[Dinners] (
    [DinnerID]     INT               IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (50)     NOT NULL,
    [EventDate]    DATETIME          NOT NULL,
    [Description]  NVARCHAR (256)    NOT NULL,
    [HostedBy]     NVARCHAR (20)     NULL,
    [ContactPhone] NVARCHAR (20)     NOT NULL,
    [Address]      NVARCHAR (50)     NOT NULL,
    [Country]      NVARCHAR (MAX)    NULL,
    [Location]     [sys].[geography] NULL,
    CONSTRAINT [PK_dbo.Dinners] PRIMARY KEY CLUSTERED ([DinnerID] ASC)
);

