CREATE TABLE [dbo].[TB_CallTask]
(
[CallTaskID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[SysPersonID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CallTime] [datetime] NOT NULL,
[CallStatus] [int] NULL CONSTRAINT [DF_TB_CallTask_CallStatus] DEFAULT ((1)),
[Remark] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_CallTask_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[IsDel] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_CallTask] ADD CONSTRAINT [PK_TB_CallTask] PRIMARY KEY CLUSTERED  ([CallTaskID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_CallTask] ADD CONSTRAINT [FK_TB_CallTask_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
ALTER TABLE [dbo].[TB_CallTask] ADD CONSTRAINT [FK_TB_CallTask_SysPerson] FOREIGN KEY ([SysPersonID]) REFERENCES [dbo].[SysPerson] ([Id])
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼出状态  1 未呼 2 已呼
', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'CallStatus'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫计划任务表主键', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'CallTaskID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'CallTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'被呼人（简历ID）', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'ImportResumeID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'备注', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'Remark'
GO
EXEC sp_addextendedproperty N'MS_Description', N'呼叫人（系统账号ID）', 'SCHEMA', N'dbo', 'TABLE', N'TB_CallTask', 'COLUMN', N'SysPersonID'
GO
