CREATE TABLE [dbo].[Online] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [IdPlayer]   NVARCHAR (128) NOT NULL,
    [DateOnline] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Online_To_Player] FOREIGN KEY ([IdPlayer]) REFERENCES [dbo].[Player] ([Id])
);

