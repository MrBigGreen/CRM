CREATE TABLE [dbo].[TB_JobNature]
(
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobNatureCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobNatureName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_JobNature_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobNature] ADD CONSTRAINT [PK_TB_JobNature] PRIMARY KEY CLUSTERED  ([ImportResumeID], [JobNatureCode]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobNature] ADD CONSTRAINT [FK_TB_JobNature_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入的简历ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望工作性质 Code ', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'JobNatureCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望工作性质名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'JobNatureName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobNature', 'COLUMN', N'LastUpdatedTime'
GO
