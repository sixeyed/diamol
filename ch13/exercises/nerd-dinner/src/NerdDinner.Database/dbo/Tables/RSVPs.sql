CREATE TABLE [dbo].[RSVPs] (
    [RsvpID]       INT            IDENTITY (1, 1) NOT NULL,
    [DinnerID]     INT            NOT NULL,
    [AttendeeName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.RSVPs] PRIMARY KEY CLUSTERED ([RsvpID] ASC),
    CONSTRAINT [FK_dbo.RSVPs_dbo.Dinners_DinnerID] FOREIGN KEY ([DinnerID]) REFERENCES [dbo].[Dinners] ([DinnerID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DinnerID]
    ON [dbo].[RSVPs]([DinnerID] ASC);

