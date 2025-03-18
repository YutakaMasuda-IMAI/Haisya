USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Check_Shitabarai_Detail]    Script Date: 2024/05/30 18:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Check_Shitabarai_Detail]
GO

CREATE TABLE [dbo].[T_Check_Shitabarai_Detail](
	[Check_Shitabarai_ID] [int] NOT NULL,
	[Uriage_Shiharai_ID] [int] NOT NULL,
	[Anken_ID] [int] NOT NULL,
	[Uriage_ID] [int] NOT NULL,
	[Nippou_ID] [int] NOT NULL,
	[Approval_Datetime] [datetime] NULL,
	[Approval_User] [int] NOT NULL,
	[Qty] [float] NULL,
	[Unit] [int] NULL,
	[UnitPrice] [money] NULL,
	[CalcPrice] [money] NULL,
	[ShiharaiUnchin] [money] NULL,
	[Tatekaekin] [money] NULL,
	[Warimashi1] [money] NULL,
	[Warimashi2] [money] NULL,
	[Warimashi3] [money] NULL,
	[Warimashi4] [money] NULL,
	[Warimashi5] [money] NULL,
	[ShiharaiTotal] [money] NULL,
 CONSTRAINT [PK_T_Check_Shitabarai_Detail] PRIMARY KEY CLUSTERED 
(
	[Check_Shitabarai_ID] ASC,
	[Uriage_Shiharai_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai_Detail] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Detail_Approval_User]  DEFAULT ((0)) FOR [Approval_User]
GO


