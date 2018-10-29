CREATE TABLE [dbo].[TB_Address]
(
[AddressID] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[ProvinceCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CityCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[DistrictCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[AddressDetails] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[VersionNo] [int] NULL,
[TransactionID] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[CreatedTime] [datetime] NULL CONSTRAINT [DF_TB_Address_CreatedTime] DEFAULT (getdate()),
[CreatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[LastUpdatedTime] [datetime] NULL,
[LastUpdatedBy] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL,
[Lng] [decimal] (18, 12) NULL,
[Lat] [decimal] (18, 12) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_Address] ADD CONSTRAINT [PK_TB_Address] PRIMARY KEY CLUSTERED  ([AddressID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'详细地址信息', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'AddressDetails'
GO
EXEC sp_addextendedproperty N'MS_Description', N'KEY 自增长', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'AddressID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'城市（CodeTable）', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'CityCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建者', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'CreatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'创建时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'CreatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'区域(CodeTable)', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'DistrictCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改者', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'LastUpdatedBy'
GO
EXEC sp_addextendedproperty N'MS_Description', N'最后修改时间', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'LastUpdatedTime'
GO
EXEC sp_addextendedproperty N'MS_Description', N'纬度', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'Lat'
GO
EXEC sp_addextendedproperty N'MS_Description', N'经度', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'Lng'
GO
EXEC sp_addextendedproperty N'MS_Description', N'省份（CodeTable）', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'ProvinceCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'事务', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'TransactionID'
GO
EXEC sp_addextendedproperty N'MS_Description', N'版本', 'SCHEMA', N'dbo', 'TABLE', N'TB_Address', 'COLUMN', N'VersionNo'
GO
