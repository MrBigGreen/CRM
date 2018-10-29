CREATE TABLE [dbo].[SysDocumentSysDepartment]
(
[SysDepartmentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Sys_Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[SysDocumentSysDepartment] ADD
CONSTRAINT [FK_SYSDOCUM_REFERENCE_SYSDEPAR] FOREIGN KEY ([SysDepartmentId]) REFERENCES [dbo].[SysDepartment] ([Id])
GO
ALTER TABLE [dbo].[SysDocumentSysDepartment] ADD CONSTRAINT [PK_SYSDOCUMENTSYSDEPARTMENT] PRIMARY KEY CLUSTERED  ([SysDepartmentId], [Sys_Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysDocumentSysDepartment] ADD CONSTRAINT [FK_SYSDOCUM_REFERENCE_SYSDOCUM2] FOREIGN KEY ([Sys_Id]) REFERENCES [dbo].[SysDocument] ([Id])
GO
