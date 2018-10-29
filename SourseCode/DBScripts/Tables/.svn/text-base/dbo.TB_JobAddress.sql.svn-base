CREATE TABLE [dbo].[TB_JobAddress]
(
[JobAddressID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CallLogID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ProvinceCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CityCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CityName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[DistrictCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_JobAddress_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobAddress] ADD CONSTRAINT [PK_TB_JobAddress_1] PRIMARY KEY CLUSTERED  ([JobAddressID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_JobAddress] ADD CONSTRAINT [FK_TB_JobAddress_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望工作地点名称', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'CityCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'导入的简历ID', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'期望工作地点', 'SCHEMA', N'dbo', 'TABLE', N'TB_JobAddress', 'COLUMN', N'ProvinceCode'
GO
