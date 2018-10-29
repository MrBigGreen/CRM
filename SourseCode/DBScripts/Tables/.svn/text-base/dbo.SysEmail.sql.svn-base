CREATE TABLE [dbo].[SysEmail]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysMailId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Subject] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Content] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[Reply_email] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Mail_type] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[ReadTime] [datetime] NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysEmail] ADD CONSTRAINT [PK_SYSEMAIL] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysEmail] ADD CONSTRAINT [FK_SYSEMAIL_REFERENCE_SYSEMAIL] FOREIGN KEY ([SysMailId]) REFERENCES [dbo].[SysEmailTemp] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysEmail', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysEmail', 'COLUMN', N'ReadTime'
GO
