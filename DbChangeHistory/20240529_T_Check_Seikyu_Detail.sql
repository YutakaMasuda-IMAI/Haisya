USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Check_Seikyu_Detail]    Script Date: 2024/05/29 13:22:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Check_Seikyu_Detail]
GO

CREATE TABLE [dbo].[T_Check_Seikyu_Detail](
	[Check_Seikyu_ID] [int] NOT NULL,
	[Uriage_Unchin_ID] [int] NOT NULL,
	[Anken_ID] [int] NOT NULL,
	[Uriage_ID] [int] NOT NULL,
	[Nippou_ID] [int] NOT NULL,
	[Approval_Datetime] [datetime] NULL,
	[Approval_User] [int] NOT NULL,
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
 CONSTRAINT [PK_T_Check_Seikyu_Detail] PRIMARY KEY CLUSTERED 
(
	[Check_Seikyu_ID] ASC,
	[Uriage_Unchin_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Check_Seikyu_Detail] ADD  CONSTRAINT [DF_T_Check_Seikyu_Detail_Approval_User]  DEFAULT ((0)) FOR [Approval_User]
GO


