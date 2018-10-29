CREATE TABLE [dbo].[SysDocumentSysPerson]
(
[SysPersonId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysSysDocumentId_Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysDocumentSysPerson] ADD CONSTRAINT [PK_SYSDOCUMENTSYSPERSON] PRIMARY KEY CLUSTERED  ([SysPersonId], [SysSysDocumentId_Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysDocumentSysPerson] ADD CONSTRAINT [FK_SYSDOCUM_REFERENCE_SYSPERSO] FOREIGN KEY ([SysPersonId]) REFERENCES [dbo].[SysPerson] ([Id])
GO
ALTER TABLE [dbo].[SysDocumentSysPerson] ADD CONSTRAINT [FK_SYSDOCUM_REFERENCE_SYSDOCUM3] FOREIGN KEY ([SysSysDocumentId_Id]) REFERENCES [dbo].[SysDocument] ([Id])
GO
