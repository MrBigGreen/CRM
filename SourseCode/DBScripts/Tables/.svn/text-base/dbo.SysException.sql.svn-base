CREATE TABLE [dbo].[SysException]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[LeiXing] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Message] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[Result] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysException] ADD CONSTRAINT [PK_SYSEXCEPTION] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysException', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysException', 'COLUMN', N'Message'
GO
