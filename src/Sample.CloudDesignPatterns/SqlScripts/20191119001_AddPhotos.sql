﻿CREATE TABLE [dbo].Photos (
    [Id] [int] NOT NULL IDENTITY(1,1),
    [Description] [nvarchar](255),
	[Name] [nvarchar](100),
	[Image] [varbinary](max),
	[Thumbnail] [varbinary](max)
CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO