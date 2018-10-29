CREATE TABLE [dbo].[TB_CallLog]
(
[CallLogID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysPersonID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[PositionRecommendID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[CallTaskID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_TB_CallLog_CallTaskID] DEFAULT ((0)),
[CallTime] [datetime] NULL,
[IsConnect] [bit] NULL CONSTRAINT [DF_TB_CallLog_IsConnect] DEFAULT ((0)),
[IsSound] [bit] NULL CONSTRAINT [DF_TB_CallLog_IsSound] DEFAULT ((0)),
[JobStatusCode] [nvarchar] (max) COLLATE Chinese_PRC_CI_AS NULL,
[JobIndustryCode] [nvarchar] (max) COLLATE Chinese_PRC_CI_AS NULL,
[JobIndustryName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[JobPostCode] [nvarchar] (max) COLLATE Chinese_PRC_CI_AS NULL,
[JobPostName] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[JobAddressCode] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[JobAddressName] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[JobSalaryCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[NowCompanyInfo] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Remark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_CallLog_CreatedTime] DEFAULT (getdate()),
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsDel] [bit] NULL,
[IsSendEmail] [bit] NULL,
[IsSendMessage] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_CallLog] ADD CONSTRAINT [PK_TB_CallLog] PRIMARY KEY CLUSTERED  ([CallLogID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_CallLog] ADD CONSTRAINT [FK_TB_CallLog_TB_PositionRecommend] FOREIGN KEY ([PositionRecommendID]) REFERENCES [dbo].[TB_PositionRecommend] ([PositionRecommendID])
GO
ALTER TABLE [dbo].[TB_CallLog] ADD CONSTRAINT [FK_TB_CallLog_TB_CallLog] FOREIGN KEY ([SysPersonID]) REFERENCES [dbo].[SysPerson] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫记录表 主键', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'CallLogID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫计划任务ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'CallTaskID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'CallTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否接通', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'IsConnect'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否发送邮件', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'IsSendEmail'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否发送短信', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'IsSendMessage'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否录音', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'IsSound'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望工作地点', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'JobAddressCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望行业', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'JobIndustryCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望职位', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'JobPostCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望薪酬', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'JobSalaryCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'求职状态 ', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'JobStatusCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'现在公司', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'NowCompanyInfo'
GO
EXEC sp_addextendedproperty N'MS_Description', N'职位推荐关联表ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'PositionRecommendID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'备注', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'Remark'
GO
EXEC sp_addextendedproperty N'MS_Description', N'系统账号ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallLog', 'COLUMN', N'SysPersonID'
GO
