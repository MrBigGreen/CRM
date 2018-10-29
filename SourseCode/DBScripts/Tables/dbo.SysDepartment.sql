CREATE TABLE [dbo].[SysDepartment]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ParentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[PositionLevel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Address] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Sort] [int] NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[UpdateTime] [datetime] NULL,
[UpdatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[SysDepartment] ADD CONSTRAINT [PK_SYSDEPARTMENT] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]

GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysDepartment', 'COLUMN', N'Name'
GO

ALTER TABLE [dbo].[SysDepartment] ADD
CONSTRAINT [FK_SYSDEPAR_REFERENCE_SYSDEPAR] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[SysDepartment] ([Id])
GO
