USE [Haisya]
GO

/****** Object:  Table [dbo].[M_Yosya]    Script Date: 2024/05/31 9:51:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[M_Yosya]
GO

CREATE TABLE [dbo].[M_Yosya](
	[Yosya_ID] [int] IDENTITY(1,1) NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Yosya_Code] [nvarchar](50) NULL,
	[Yosya_Name] [nvarchar](40) NOT NULL,
	[Yosya_Name_Kana] [nvarchar](16) NULL,
	[Yosya_Name_Abbr] [nvarchar](20) NULL,
	[Post_Code] [nvarchar](10) NULL,
	[Mail_Title] [nvarchar](50) NULL,
	[Mail_Address1] [nvarchar](50) NULL,
	[Mail_Address2] [nvarchar](50) NULL,
	[Address1] [nvarchar](40) NULL,
	[Address2] [nvarchar](40) NULL,
	[Phone1] [nvarchar](13) NULL,
	[Phone2] [nvarchar](13) NULL,
	[Fax1] [nvarchar](13) NULL,
	[Fax2] [nvarchar](13) NULL,
	[ShiharaiTantouID] [int] NOT NULL,
	[ClosingDate1] [int] NULL,
	[Del_Flg] [bit] NOT NULL,
	[UP_DATE] [datetime] NOT NULL,
	[Shime_Day] [int] NOT NULL,
	[ShiharaiRemarks] [nvarchar](255) NULL,
	[Shiharai_Yosya_ID] [int] NOT NULL,
 CONSTRAINT [PK_M_Yosya] PRIMARY KEY CLUSTERED 
(
	[Yosya_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[M_Yosya] ADD  CONSTRAINT [DF_M_Yosya_ShiharaiTantouID]  DEFAULT ((0)) FOR [ShiharaiTantouID]
GO

ALTER TABLE [dbo].[M_Yosya] ADD  CONSTRAINT [DF__M_Yosya__Del_Flg__1446FBA6]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[M_Yosya] ADD  CONSTRAINT [DF_M_Yosya_UP_DATE]  DEFAULT (getdate()) FOR [UP_DATE]
GO

ALTER TABLE [dbo].[M_Yosya] ADD  CONSTRAINT [DF_M_Yosya_Shime_Day]  DEFAULT ((0)) FOR [Shime_Day]
GO

ALTER TABLE [dbo].[M_Yosya] ADD  CONSTRAINT [DF_M_Yosya_Seikyu_Customer_ID]  DEFAULT ((0)) FOR [Shiharai_Yosya_ID]
GO


