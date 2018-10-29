CREATE TABLE [dbo].[SysDocumentTalk]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Content] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[SysDocumentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Bad] [int] NULL,
[Good] [int] NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysDocumentTalk] ADD CONSTRAINT [PK_SYSDOCUMENTTALK] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysDocumentTalk] ADD CONSTRAINT [FK_SYSDOCUM_REFERENCE_SYSDOCUM] FOREIGN KEY ([SysDocumentId]) REFERENCES [dbo].[SysDocument] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysDocumentTalk', 'COLUMN', N'Content'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysDocumentTalk', 'COLUMN', N'CreateTime'
GO
