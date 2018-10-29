CREATE TABLE [dbo].[TB_ProjectExperience]
(
[ProjectID] [int] NOT NULL IDENTITY(1, 1) NOT FOR REPLICATION,
[ProjectName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[FromYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[FromMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ProjectDescription] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[MainResponsibility] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [bit] NULL CONSTRAINT [DF_TB_ProjectExperience_IsDelete] DEFAULT ((0)),
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[VersionNo] [int] NOT NULL CONSTRAINT [DF_TB_ProjectExperience_VersionNo] DEFAULT ((1)),
[TransactionID] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_TB_ProjectExperience_TransactionID] DEFAULT ('?'),
[CreatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_TB_ProjectExperience_CreatedBy] DEFAULT (N'?'),
[CreatedTime] [datetime] NOT NULL CONSTRAINT [DF_TB_ProjectExperience_CreatedTime] DEFAULT (getdate()),
[LastUpdatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_TB_ProjectExperience_LastUpdatedBy] DEFAULT (N'?'),
[LastUpdatedTime] [datetime] NOT NULL CONSTRAINT [DF_TB_ProjectExperience_LastUpdatedTime] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_ProjectExperience] ADD CONSTRAINT [PK_TB_ProjectExperience] PRIMARY KEY CLUSTERED  ([ProjectID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_ProjectExperience] ADD CONSTRAINT [FK_TB_ProjectExperience_TB_ProjectExperience] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'个人简历-项目经验表', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始月', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'FromMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始年', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'FromYear'
GO
EXEC sp_addextendedproperty N'MS_Description', N'简历Id,与表Resume关联', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否删除', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'IsDelete'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后更新者', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后更新时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'主要责任', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'MainResponsibility'
GO
EXEC sp_addextendedproperty N'MS_Description', N'项目描述', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ProjectDescription'
GO
EXEC sp_addextendedproperty N'MS_Description', N'KEY 自增长', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ProjectID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'项目名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ProjectName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'到达月', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ToMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'到达年', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'ToYear'
GO
EXEC sp_addextendedproperty N'MS_Description', N'事务', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'TransactionID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'版本', 'SCHEMA', N'dbo', 'TABLE', N'TB_ProjectExperience', 'COLUMN', N'VersionNo'
GO
