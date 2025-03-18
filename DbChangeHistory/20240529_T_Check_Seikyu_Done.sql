USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Check_Seikyu_Done]    Script Date: 2024/05/29 13:23:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Check_Seikyu_Done]
GO

CREATE TABLE [dbo].[T_Check_Seikyu_Done](
	[Check_Seikyu_ID] [int] NOT NULL,
	[Check_Datetime] [datetime] NOT NULL,
	[Check_User] [int] NOT NULL,
	[Check_Reault] [int] NOT NULL,
	[Change_Flg] [int] NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Check_Seikyu_Done] PRIMARY KEY CLUSTERED 
(
	[Check_Seikyu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Check_Datetime]  DEFAULT (getdate()) FOR [Check_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Check_User]  DEFAULT ((0)) FOR [Check_User]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Check_Reault]  DEFAULT ((0)) FOR [Check_Reault]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Change_Flg]  DEFAULT ((0)) FOR [Change_Flg]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Done] ADD  CONSTRAINT [DF_T_Check_Seikyu_Done_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：金額変更無し、1：金額変更あり' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Check_Seikyu_Done', @level2type=N'COLUMN',@level2name=N'Change_Flg'
GO


