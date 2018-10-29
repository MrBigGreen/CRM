CREATE TABLE [dbo].[SysField]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MyTexts] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ParentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[MyTables] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[MyColums] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Sort] [int] NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[UpdateTime] [datetime] NULL,
[UpdatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysField] ADD CONSTRAINT [PK_SYSFIELD] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[SysField] ([MyTables], [MyColums]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysField] ADD CONSTRAINT [FK_SYSFIELD_REFERENCE_SYSFIELD] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[SysField] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysField', 'COLUMN', N'MyTexts'
GO
