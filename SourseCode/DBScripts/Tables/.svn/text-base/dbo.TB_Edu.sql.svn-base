CREATE TABLE [dbo].[TB_Edu]
(
[EduID] [int] NOT NULL IDENTITY(1, 1),
[FromYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[FromMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToYear] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[ToMonth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL,
[IsOverSea] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[ShoolName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Country] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[MajorCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Degree] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[MajorDescription] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[IsDelete] [bit] NULL CONSTRAINT [DF_TB_Edu_IsDelete] DEFAULT ((0)),
[VersionNo] [int] NULL,
[TransactionID] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_Edu_CreatedTime] DEFAULT (getdate()),
[LastUpdatedBy] [nvarchar] (256) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[MajorName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_Edu] ADD CONSTRAINT [PK_TB_Edu] PRIMARY KEY CLUSTERED  ([EduID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_Edu] ADD CONSTRAINT [FK_TB_Edu_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'国家，作废字段', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'Country'
GO
EXEC sp_addextendedproperty N'MS_Description', N'学历代码', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'Degree'
GO
EXEC sp_addextendedproperty N'MS_Description', N'KEY 自增长', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'EduID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始月份', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'FromMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'开始年份', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'FromYear'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否海外工作经验代码,CodeTable', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'IsOverSea'
GO
EXEC sp_addextendedproperty N'MS_Description', N'专业代码', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'MajorCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'专业描述', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'MajorDescription'
GO
EXEC sp_addextendedproperty N'MS_Description', N'学校名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'ShoolName'
GO
EXEC sp_addextendedproperty N'MS_Description', N'结束月份', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'ToMonth'
GO
EXEC sp_addextendedproperty N'MS_Description', N'结束年份', 'SCHEMA', N'dbo', 'TABLE', N'TB_Edu', 'COLUMN', N'ToYear'
GO
