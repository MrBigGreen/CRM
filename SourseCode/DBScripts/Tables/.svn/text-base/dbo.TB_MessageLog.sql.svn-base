CREATE TABLE [dbo].[TB_MessageLog]
(
[MsgID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MsgType] [smallint] NOT NULL,
[MsgSender] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MsgRecipient] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL,
[MsgTitle] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[MsgContent] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL,
[IsDel] [bit] NULL CONSTRAINT [DF_TB_MessageLog_IsDel] DEFAULT ((0)),
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_MessageLog_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_MessageLog] ADD CONSTRAINT [PK_TB_MessageLog] PRIMARY KEY CLUSTERED  ([MsgID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'是否删除', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'IsDel'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'信息内容', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgContent'
GO
EXEC sp_addextendedproperty N'MS_Description', N'发送信息记录表主键', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'收件人', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgRecipient'
GO
EXEC sp_addextendedproperty N'MS_Description', N'发送人', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgSender'
GO
EXEC sp_addextendedproperty N'MS_Description', N'标题', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgTitle'
GO
EXEC sp_addextendedproperty N'MS_Description', N'消息类型
1 短信 2 邮件 3 其它
', 'SCHEMA', N'dbo', 'TABLE', N'TB_MessageLog', 'COLUMN', N'MsgType'
GO
