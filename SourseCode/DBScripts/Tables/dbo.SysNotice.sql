CREATE TABLE [dbo].[SysNotice]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Message] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[LostTime] [datetime] NULL,
[PersonId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysNotice] ADD CONSTRAINT [PK_SYSNOTICE] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysNotice', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysNotice', 'COLUMN', N'LostTime'
GO
