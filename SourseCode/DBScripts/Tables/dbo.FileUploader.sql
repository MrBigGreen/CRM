CREATE TABLE [dbo].[FileUploader]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Path] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[FullPath] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL,
[Suffix] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[Size] [int] NULL,
[Remark] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FileUploader] ADD CONSTRAINT [PK_FILEUPLOADER] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'FileUploader', 'COLUMN', N'CreateTime'
GO
EXEC sp_addextendedproperty N'MS_Description', 'Research', 'SCHEMA', N'dbo', 'TABLE', N'FileUploader', 'COLUMN', N'Name'
GO
