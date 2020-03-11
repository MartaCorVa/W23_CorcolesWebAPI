CREATE TABLE [dbo].[Message] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [IdPlayer]    NVARCHAR (128) NOT NULL,
    [Content]     NVARCHAR (128) NOT NULL,
    [DateMessage] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_To_Player] FOREIGN KEY ([IdPlayer]) REFERENCES [dbo].[Player] ([Id])
);

