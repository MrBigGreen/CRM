CREATE TABLE [dbo].[SysAnnouncement]
(
[Id] [nvarchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL,
[Title] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL,
[Message] [nvarchar] (4000) COLLATE Chinese_PRC_CI_AS NULL,
[State] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
[CreateTime] [datetime] NULL,
[CreatePerson] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SysAnnouncement] ADD CONSTRAINT [PK_SYSANNOUNCEMENT] PRIMARY KEY NONCLUSTERED  ([Id]) ON [PRIMARY]
GO
