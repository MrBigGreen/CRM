CREATE TABLE [dbo].[SysMenuSysOperation]
(
[SysMenuId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysOperationId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenuSysOperation] ADD CONSTRAINT [PK_SYSMENUSYSOPERATION] PRIMARY KEY CLUSTERED  ([SysMenuId], [SysOperationId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysMenuSysOperation] ADD CONSTRAINT [FK_SYSMENUS_REFERENCE_SYSMENU] FOREIGN KEY ([SysMenuId]) REFERENCES [dbo].[SysMenu] ([Id])
GO
ALTER TABLE [dbo].[SysMenuSysOperation] ADD CONSTRAINT [FK_SYSMENUS_REFERENCE_SYSOPERA] FOREIGN KEY ([SysOperationId]) REFERENCES [dbo].[SysOperation] ([Id])
GO
