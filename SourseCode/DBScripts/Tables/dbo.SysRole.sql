CREATE TABLE [dbo].[SysRole]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Description] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[UpdateTime] [datetime] NULL,
[UpdatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysRole] ADD CONSTRAINT [PK_SYSROLE] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysRole', 'COLUMN', N'Name'
GO
