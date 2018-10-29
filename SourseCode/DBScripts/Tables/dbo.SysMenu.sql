CREATE TABLE [dbo].[SysMenu]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ParentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Url] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Iconic] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Sort] [int] NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[UpdateTime] [datetime] NULL,
[UpdatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[IsLeaf] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenu] ADD CONSTRAINT [PK_SYSMENU] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenu] ADD CONSTRAINT [FK_SYSMENU_REFERENCE_SYSMENU] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[SysMenu] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', '导航栏', 'SCHEMA', N'dbo', 'TABLE', N'SysMenu', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysMenu', 'COLUMN', N'Name'
GO
EXEC sp_addextendedproperty N'MS_Description', 'RadioButtonResearch', 'SCHEMA', N'dbo', 'TABLE', N'SysMenu', 'COLUMN', N'State'
GO
