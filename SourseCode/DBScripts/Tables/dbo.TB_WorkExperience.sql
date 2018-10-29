CREATE TABLE [dbo].[TB_WorkExperience]
(
[WorkID] [int] NOT NULL IDENTITY(1, 1),
[FromYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[FromMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[WorkAddress] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CompanySize] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Industry] [varchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[PostType] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PostName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Salary] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LeaveReson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[WorkDescription] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[MexIsHas] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[MexFromYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[MexFromMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[MexToYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[MexToMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ReportObject] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[PersonNum] [varchar] (10) COLLATE Chinese_PRC_CI_AS NULL,
[Underling] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[JXDescription] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[Department] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsDeleted] [bit] NULL CONSTRAINT [DF_TB_WorkExperience_IsDeleted] DEFAULT ((0)),
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[VersionNo] [int] NULL,
[TransactionID] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_WorkExperience_CreatedTime] DEFAULT (getdate()),
[LastUpdatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_WorkExperience] ADD CONSTRAINT [PK_TB_WorkExperience] PRIMARY KEY CLUSTERED  ([WorkID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_WorkExperience] ADD CONSTRAINT [FK_TB_WorkExperience_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'公司名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'CompanyName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始月', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'FromMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始年', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'FromYear'
GO
EXEC sp_addextendedproperty N'MS_Description', N'结束月', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'ToMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'结束年', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'ToYear'
GO
EXEC sp_addextendedproperty N'MS_Description', N'工作地点,市代码', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'WorkAddress'
GO
EXEC sp_addextendedproperty N'MS_Description', N'工作描述', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'WorkDescription'
GO
EXEC sp_addextendedproperty N'MS_Description', N'KEY 自增长', 'SCHEMA', N'dbo', 'TABLE', N'TB_WorkExperience', 'COLUMN', N'WorkID'
GO
