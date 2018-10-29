CREATE TABLE [dbo].[SysEmailTemp]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Mail_name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Subject] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Content] [ntext] COLLATE Chinese_PRC_CI_AS NULL,
[Reply_email] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[IsDefault] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Mail_type] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysEmailTemp] ADD CONSTRAINT [PK_mail_info2] PRIMARY KEY NONCLUSTERED  ([Id]) WITH (FILLFACTOR=90) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysEmailTemp', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'RadioButton', 'SCHEMA', N'dbo', 'TABLE', N'SysEmailTemp', 'COLUMN', N'IsDefault'
GO
