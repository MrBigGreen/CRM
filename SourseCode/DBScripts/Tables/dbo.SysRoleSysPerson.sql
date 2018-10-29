CREATE TABLE [dbo].[SysRoleSysPerson]
(
[SysPersonId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysRoleId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysRoleSysPerson] ADD CONSTRAINT [PK_SYSROLESYSPERSON] PRIMARY KEY CLUSTERED  ([SysPersonId], [SysRoleId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysRoleSysPerson] ADD CONSTRAINT [FK_SYSROLES_REFERENCE_SYSPERSO] FOREIGN KEY ([SysPersonId]) REFERENCES [dbo].[SysPerson] ([Id])
GO
ALTER TABLE [dbo].[SysRoleSysPerson] ADD CONSTRAINT [FK_SYSROLES_REFERENCE_SYSROLE] FOREIGN KEY ([SysRoleId]) REFERENCES [dbo].[SysRole] ([Id])
GO
