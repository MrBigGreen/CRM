CREATE TABLE [dbo].[SysPerson]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MyName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Password] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SurePassword] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Sex] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[SysDepartmentId] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[Position] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[MobilePhoneNumber] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyQQ] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Province] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[City] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Village] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[EnglishName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[EmailAddress] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[UpdateTime] [datetime] NULL,
[LogonNum] [int] NULL,
[LogonTime] [datetime] NULL,
[LogonIP] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[LastLogonTime] [datetime] NULL,
[LastLogonIP] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[PageStyle] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[UpdatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Version] [timestamp] NULL,
[HDpic] [varchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[SysPerson] ADD
CONSTRAINT [FK_SYSPERSO_REFERENCE_SYSDEPAR] FOREIGN KEY ([SysDepartmentId]) REFERENCES [dbo].[SysDepartment] ([Id])
GO
ALTER TABLE [dbo].[SysPerson] ADD CONSTRAINT [PK_SYSPERSON] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Index_1] ON [dbo].[SysPerson] ([Name]) ON [PRIMARY]
GO

EXEC sp_addextendedproperty N'MS_Description', 'ProvinceCascade', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'City'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'MyName'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'Name'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Display', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'PageStyle'
GO
EXEC sp_addextendedproperty N'MS_Description', 'DropDown', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'Province'
GO
EXEC sp_addextendedproperty N'MS_Description', 'DropDown', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'Sex'
GO
EXEC sp_addextendedproperty N'MS_Description', 'RadioButtonResearch', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'State'
GO
EXEC sp_addextendedproperty N'MS_Description', 'CityCascade', 'SCHEMA', N'dbo', 'TABLE', N'SysPerson', 'COLUMN', N'Village'
GO
