CREATE TABLE [dbo].[SysMessage]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Content] [nvarchar] (400) COLLATE Chinese_PRC_CI_AS NULL,
[SysMessageTempId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[MessageType] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[ReadTime] [datetime] NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMessage] ADD CONSTRAINT [PK_SYSMESSAGE] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMessage] ADD CONSTRAINT [FK_SYSMESSA_REFERENCE_SYSMESSA] FOREIGN KEY ([SysMessageTempId]) REFERENCES [dbo].[SysMessageTemp] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysMessage', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysMessage', 'COLUMN', N'ReadTime'
GO
