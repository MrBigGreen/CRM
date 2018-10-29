CREATE TABLE [dbo].[TB_JobIndustry]
(
[JobIndustryID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobIndustryCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobIndustryName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_JobIndustry_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobIndustry] ADD CONSTRAINT [PK_TB_JobIndustry_1] PRIMARY KEY CLUSTERED  ([JobIndustryID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobIndustry] ADD CONSTRAINT [FK_TB_JobIndustry_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入的简历ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望从事行业', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'JobIndustryCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望从事行业名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'JobIndustryName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobIndustry', 'COLUMN', N'LastUpdatedTime'
GO
