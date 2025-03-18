USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Check_Seikyu_Change]    Script Date: 2024/05/29 13:21:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Check_Seikyu_Change]
GO


CREATE TABLE [dbo].[T_Check_Seikyu_Change](
	[Check_Seikyu_ID] [int] NOT NULL,
	[Uriage_Unchin_ID] [int] NOT NULL,
	[Qty] [float] NULL,
	[Unit] [int] NULL,
	[UnitPrice] [money] NULL,
	[CalcPrice] [money] NULL,
	[SeikyuUnchin] [money] NULL,
	[Tatekaekin] [money] NULL,
	[Warimashi1] [money] NULL,
	[Warimashi2] [money] NULL,
	[Warimashi3] [money] NULL,
	[Warimashi4] [money] NULL,
	[Warimashi5] [money] NULL,
	[SeikyuTotal] [money] NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Check_Seikyu_Change] PRIMARY KEY CLUSTERED 
(
	[Check_Seikyu_ID] ASC,
	[Uriage_Unchin_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Change] ADD  CONSTRAINT [DF_T_Check_Seikyu_Change_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Change] ADD  CONSTRAINT [DF_T_Check_Seikyu_Change_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Change] ADD  CONSTRAINT [DF_T_Check_Seikyu_Change_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Change] ADD  CONSTRAINT [DF_T_Check_Seikyu_Change_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO


