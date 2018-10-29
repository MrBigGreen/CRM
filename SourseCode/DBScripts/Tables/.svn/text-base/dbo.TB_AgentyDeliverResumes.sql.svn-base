CREATE TABLE [dbo].[TB_AgentyDeliverResumes]
(
[ADRID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[CompanyInterviewJobID] [int] NULL,
[ImportResumeID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyID] [int] NULL,
[MsgID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NULL,
[JobTitle] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CompanyAddressName] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[SalaryCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[RecommendType] [int] NULL,
[IsPush] [bit] NULL CONSTRAINT [DF_TB_AgentyDeliverResumes_IsPush] DEFAULT ((0)),
[IsCall] [bit] NULL CONSTRAINT [DF_TB_AgentyDeliverResumes_IsCall] DEFAULT ((0)),
[IsAgenty] [bit] NULL CONSTRAINT [DF_TB_AgentyDeliverResumes_IsAgenty] DEFAULT ((0)),
[Agent] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[DeliverTime] [datetime] NULL,
[AgentyStartTime] [datetime] NULL,
[AgentyEndTime] [datetime] NULL,
[Record] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Pic] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[IsDel] [bit] NULL CONSTRAINT [DF_TB_AgentyDeliverResumes_IsDel] DEFAULT ((0)),
[CreatedTime] [datetime] NULL,
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_AgentyDeliverResumes] ADD CONSTRAINT [PK_TB_AgentyDeliverResumes] PRIMARY KEY CLUSTERED  ([ADRID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_AgentyDeliverResumes] ADD CONSTRAINT [FK_TB_AgentyDeliverResumes_TB_AgentyDeliverResumes] FOREIGN KEY ([ADRID]) REFERENCES [dbo].[TB_AgentyDeliverResumes] ([ADRID])
GO
ALTER TABLE [dbo].[TB_AgentyDeliverResumes] ADD CONSTRAINT [FK_TB_AgentyDeliverResumes_TB_ImportResume] FOREIGN KEY ([ImportResumeID]) REFERENCES [dbo].[TB_ImportResume] ([ImportResumeID])
GO
ALTER TABLE [dbo].[TB_AgentyDeliverResumes] ADD CONSTRAINT [FK_TB_AgentyDeliverResumes_TB_MessageLog] FOREIGN KEY ([MsgID]) REFERENCES [dbo].[TB_MessageLog] ([MsgID])
GO
