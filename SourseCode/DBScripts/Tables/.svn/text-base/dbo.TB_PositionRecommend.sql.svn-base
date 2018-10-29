CREATE TABLE [dbo].[TB_PositionRecommend]
(
[PositionRecommendID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CompanyInterviewJobID] [int] NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyID] [int] NULL,
[MsgID] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[JobTitle] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyAddressName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[RecommendType] [int] NULL,
[SalaryCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsPush] [bit] NULL CONSTRAINT [DF_TB_PositionRecommend_IsPush] DEFAULT ((0)),
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_PositionRecommend_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsDel] [bit] NULL CONSTRAINT [DF_TB_PositionRecommend_IsDel] DEFAULT ((0)),
[SendEmailTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_PositionRecommend] ADD CONSTRAINT [PK_TB_PositionRecommend] PRIMARY KEY CLUSTERED  ([PositionRecommendID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_PositionRecommend] ADD CONSTRAINT [CK_TB_PositionRecommend] UNIQUE NONCLUSTERED  ([ImportResumeID], [CompanyInterviewJobID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_PositionRecommend] ADD CONSTRAINT [FK_TB_PositionRecommend_TB_PositionRecommend] FOREIGN KEY ([PositionRecommendID]) REFERENCES [dbo].[TB_PositionRecommend] ([PositionRecommendID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'企业编号对应Company表中CompanyID', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'CompanyID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'企业职位信息ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'CompanyInterviewJobID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'企业名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'CompanyName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入简历关联ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否推送', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'IsPush'
GO
EXEC sp_addextendedproperty N'MS_Description', N'职位名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'JobTitle'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'主键 自增', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'PositionRecommendID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'关联类型1 猎头推荐  2 系统匹配', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'RecommendType'
GO
EXEC sp_addextendedproperty N'MS_Description', N'职位月薪', 'SCHEMA', N'dbo', 'TABLE', N'TB_PositionRecommend', 'COLUMN', N'SalaryCode'
GO
