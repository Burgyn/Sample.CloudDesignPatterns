CREATE TABLE [dbo].Photos (
    [Id] [int] NOT NULL,
    [Description] [nvarchar](255),
	[Name] [nvarchar](100),
	[Image] [varbinary],
	[Thumbnail] [varbinary]
CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO