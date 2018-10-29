CREATE TABLE [dbo].[SysOperation]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Function] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Iconic] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Sort] [int] NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysOperation] ADD CONSTRAINT [PK_SYSOPERATION] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', '操作', 'SCHEMA', N'dbo', 'TABLE', N'SysOperation', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysOperation', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Equal', 'SCHEMA', N'dbo', 'TABLE', N'SysOperation', 'COLUMN', N'Iconic'
GO
EXEC sp_addextendedproperty N'MS_Description', 'ResearchDropDown', 'SCHEMA', N'dbo', 'TABLE', N'SysOperation', 'COLUMN', N'State'
GO
