CREATE TABLE [dbo].[TB_JobPost]
(
[JobPostID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobPostCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL,
[JobPostName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_JobPost_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobPost] ADD CONSTRAINT [PK_TB_JobPost_1] PRIMARY KEY CLUSTERED  ([JobPostID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobPost] ADD CONSTRAINT [FK_TB_JobPost_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入的简历ID ', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望从事职位', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'JobPostCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望从事职位名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'JobPostName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobPost', 'COLUMN', N'LastUpdatedTime'
GO
