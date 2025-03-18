USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Print_Seikyu]    Script Date: 2024/05/29 13:19:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Print_Seikyu]
GO

CREATE TABLE [dbo].[T_Print_Seikyu](
	[Print_Seikyu_ID] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Tantou_ID] [int] NOT NULL,
	[Shime_Date] [date] NOT NULL,
	[Check_Seikyu_ID] [int] NOT NULL,
	[Seikyu_ID] [int] NOT NULL,
	[Del_Datetime] [datetime] NULL,
	[Customer_ID] [int] NOT NULL,
	[Seikyu_Month] [date] NOT NULL,
	[Print_Pattern] [int] NOT NULL,
	[Mail_Title] [nvarchar](50) NULL,
	[Mail_Detail] [nvarchar](255) NULL,
	[Customer_Name] [nvarchar](40) NULL,
	[Customer_Name_Kana] [nvarchar](16) NULL,
	[PostCode] [nvarchar](7) NULL,
	[Mail_Address1] [nvarchar](50) NULL,
	[Mail_Address2] [nvarchar](50) NULL,
	[Address1] [nvarchar](40) NULL,
	[Address2] [nvarchar](40) NULL,
	[Address3] [nvarchar](40) NULL,
	[Phone1] [nvarchar](13) NULL,
	[Phone2] [nvarchar](13) NULL,
	[Fax1] [nvarchar](13) NULL,
	[Fax2] [nvarchar](13) NULL,
	[Seikyu_Date] [nvarchar](10) NULL,
	[‘OŒc] [decimal](18, 0) NULL,
	[“–Œ“ü‹àŠz] [decimal](18, 0) NULL,
	[ŒJ‰z‹àŠz] [decimal](18, 0) NULL,
	[‰ÛÅ”„ã‹àŠz] [decimal](18, 0) NULL,
	[”ñ‰ÛÅ”„ã‹àŠz] [decimal](18, 0) NULL,
	[¡‰ñ”„ã‹àŠz] [decimal](18, 0) NULL,
	[Á”ïÅŠz] [decimal](18, 0) NULL,
	[Å”„ã‹àŠz] [decimal](18, 0) NULL,
	[¡‰ñ¿‹Šz] [decimal](18, 0) NULL,
	[Œ»‹à“ü‹àŠz] [decimal](18, 0) NULL,
	[¬Øè“ü‹àŠz] [decimal](18, 0) NULL,
	[U“ü‹àŠz] [decimal](18, 0) NULL,
	[èŒ`“ü‹àŠz] [decimal](18, 0) NULL,
	[è”—¿‹àŠz] [decimal](18, 0) NULL,
	[‰^’À‘ŠE] [decimal](18, 0) NULL,
	[ˆê”Ê‘ŠE] [decimal](18, 0) NULL,
	[’²®‹àŠz] [decimal](18, 0) NULL,
	[”—ÊŒv] [decimal](18, 4) NULL,
	[Šî–{‰^’ÀŒv] [decimal](18, 4) NULL,
	[Š„‘‚PŒv] [decimal](18, 4) NULL,
	[Š„‘‚QŒv] [decimal](18, 4) NULL,
	[Š„‘‚RŒv] [decimal](18, 4) NULL,
	[Š„‘‚SŒv] [decimal](18, 4) NULL,
	[Š„‘‚TŒv] [decimal](18, 4) NULL,
	[Š„‘‚UŒv] [decimal](18, 4) NULL,
	[‰^’À‡ŒvŒv] [decimal](18, 0) NULL,
	[”„ã–¾×Œ”] [int] NULL,
	[–¢’è–¾×Œ”] [int] NULL,
	[“ü‹à–¾×Œ”] [int] NULL,
	[–¾×Œ”] [int] NULL,
	[FROM_DATE] [date] NULL,
	[TO_DATE] [date] NULL,
	[UP_DATE] [datetime] NULL,
 CONSTRAINT [PK_T_Print_Seikyu_1] PRIMARY KEY CLUSTERED 
(
	[Print_Seikyu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Print_Seikyu] ADD  CONSTRAINT [DF_T_Print_Seikyu_Print_Pattern]  DEFAULT ((0)) FOR [Print_Pattern]
GO

ALTER TABLE [dbo].[T_Print_Seikyu] ADD  CONSTRAINT [DF_T_Print_Seikyu_UP_DATE]  DEFAULT (getdate()) FOR [UP_DATE]
GO


