CREATE TABLE [dbo].[Player] (
    [Id]       NVARCHAR (128) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [Email]    NVARCHAR (256) NOT NULL,
    [BirthDay] DATETIME2 (7)  NOT NULL,
    [State]    NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Players_To_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

