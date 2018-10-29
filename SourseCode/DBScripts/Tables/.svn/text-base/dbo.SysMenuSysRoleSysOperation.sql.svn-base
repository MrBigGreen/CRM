CREATE TABLE [dbo].[SysMenuSysRoleSysOperation]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysMenuId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[SysOperationId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[SysRoleId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenuSysRoleSysOperation] ADD CONSTRAINT [PK_SYSMENUSYSROLESYSOPERATION] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenuSysRoleSysOperation] ADD CONSTRAINT [FK_SYSMENUS_REFERENCE_SYSMENU2] FOREIGN KEY ([SysMenuId]) REFERENCES [dbo].[SysMenu] ([Id])
GO
ALTER TABLE [dbo].[SysMenuSysRoleSysOperation] ADD CONSTRAINT [FK_SYSMENUS_REFERENCE_SYSOPERA2] FOREIGN KEY ([SysOperationId]) REFERENCES [dbo].[SysOperation] ([Id])
GO
ALTER TABLE [dbo].[SysMenuSysRoleSysOperation] ADD CONSTRAINT [FK_SYSMENUS_REFERENCE_SYSROLE] FOREIGN KEY ([SysRoleId]) REFERENCES [dbo].[SysRole] ([Id])
GO
