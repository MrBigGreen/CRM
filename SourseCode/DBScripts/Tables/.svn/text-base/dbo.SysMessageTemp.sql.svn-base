CREATE TABLE [dbo].[SysMessageTemp]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MessageName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Content] [nvarchar] (400) COLLATE Chinese_PRC_CI_AS NULL,
[IsDefault] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[MessageType] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMessageTemp] ADD CONSTRAINT [PK_mail_info] PRIMARY KEY NONCLUSTERED  ([Id]) WITH (FILLFACTOR=90) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysMessageTemp', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'RadioButton', 'SCHEMA', N'dbo', 'TABLE', N'SysMessageTemp', 'COLUMN', N'IsDefault'
GO
