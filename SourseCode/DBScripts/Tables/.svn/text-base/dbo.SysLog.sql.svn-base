CREATE TABLE [dbo].[SysLog]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[PersonId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Message] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[Result] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[MenuId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Ip] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysLog] ADD CONSTRAINT [PK_SYSLOG] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysLog', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysLog', 'COLUMN', N'Ip'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysLog', 'COLUMN', N'Message'
GO
EXEC sp_addextendedproperty N'MS_Description', 'ResearchDropDown', 'SCHEMA', N'dbo', 'TABLE', N'SysLog', 'COLUMN', N'State'
GO
