CREATE TABLE [dbo].[TB_ImportResume]
(
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysPersonID] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[OfferUserID] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ResumeName] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL,
[GenderCode] [int] NULL,
[BOB] [datetime] NULL,
[AddressID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[PhoneNumber] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL,
[EmailAddress] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[QQID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[EducationCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[JobSalaryCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[JobStatusCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[SpecialtyCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PoliticalCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CredentialsCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[MarrigeCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[WorkYearCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ResumeSource] [int] NULL,
[ResumeContent] [text] COLLATE Chinese_PRC_CI_AS NULL,
[CVLevel] [int] NULL,
[CallCount] [int] NULL CONSTRAINT [DF_TB_ImportResume_CallCount] DEFAULT ((0)),
[IsDel] [bit] NULL CONSTRAINT [DF_TB_ImportResume_IsDel] DEFAULT ((0)),
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_ImportResume_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_ImportResume] ADD CONSTRAINT [PK_TB_ImportResume] PRIMARY KEY CLUSTERED  ([ImportResumeID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_ImportResume] ADD CONSTRAINT [CK_TB_ImportResume] UNIQUE NONCLUSTERED  ([PhoneNumber]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_ImportResume] ADD CONSTRAINT [FK_TB_ImportResume_TB_ImportResume] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[TB_Address] ([AddressID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'现居住地址Id,与表Address关联', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'AddressID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'出生年月', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'BOB'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫次数', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'CallCount'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'证件号码', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'CredentialsCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'简历层次 1白领 2 黑领 3 蓝领 4 灰领', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'CVLevel'
GO
EXEC sp_addextendedproperty N'MS_Description', N'教育程度', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'EducationCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'邮箱', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'EmailAddress'
GO
EXEC sp_addextendedproperty N'MS_Description', N'性别1 男 2 女', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'GenderCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入的简历ID 自动增长主键', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否删除', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'IsDel'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望税前月薪代码', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'JobSalaryCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'目前求职状态代码', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'JobStatusCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改人', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'9191Offer用户，对应表中UserID ', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'OfferUserID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'手机号码', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'PhoneNumber'
GO
EXEC sp_addextendedproperty N'MS_Description', N'政治面貌', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'PoliticalCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'原简历内容', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'ResumeContent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'姓名', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'ResumeName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'简历来源1 猎头推荐 2 内部匹配', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'ResumeSource'
GO
EXEC sp_addextendedproperty N'MS_Description', N'专业', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'SpecialtyCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'归属人(运营中心系统账号)', 'SCHEMA', N'dbo', 'TABLE', N'TB_ImportResume', 'COLUMN', N'SysPersonID'
GO
