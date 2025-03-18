/****** Object:  Table [dbo].[M_Customer]    Script Date: 2024/05/15 10:31:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[M_Customer]
GO

CREATE TABLE [dbo].[M_Customer](
	[Customer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Customer_ID_Oya] [int] NOT NULL,
	[Customer_Code] [nvarchar](20) NULL,
	[Customer_Code_Oya] [nvarchar](20) NULL,
	[Customer_Name] [nvarchar](60) NOT NULL,
	[Customer_Name_Kana] [nvarchar](40) NULL,
	[Customer_Name_Abbr] [nvarchar](40) NULL,
	[PostCode] [nvarchar](8) NULL,
	[Address1] [nvarchar](80) NULL,
	[Address2] [nvarchar](40) NULL,
	[Address3] [nvarchar](40) NULL,
	[Phone1] [nvarchar](13) NULL,
	[Phone2] [nvarchar](13) NULL,
	[Fax1] [nvarchar](13) NULL,
	[Fax2] [nvarchar](13) NULL,
	[Seikyu_Kubun] [int] NOT NULL,
	[SeikyuDate_Kubun] [int] NOT NULL,
	[Toll_Kubun] [int] NOT NULL,
	[Shime_Day] [int] NOT NULL,
	[AnkenRemarks] [nvarchar](255) NULL,
	[SeikyuRemarks] [nvarchar](255) NULL,
	[SeikyuTantouID] [int] NULL,
	[Del_Flg] [bit] NOT NULL,
	[Insert_Datetime] [datetime] NULL,
	[Insert_User] [int] NULL,
	[Update_Datetime] [datetime] NULL,
	[Update_User] [int] NULL,
	[Seikyu_Address] [nvarchar](50) NULL,
	[Seikuy_PostCode] [nvarchar](8) NULL,
	[Seikyu_Customer_ID] [int] NOT NULL,
 CONSTRAINT [PK_M_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Company_ID]  DEFAULT ((0)) FOR [Company_ID]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Customer_ID_Oya]  DEFAULT ((0)) FOR [Customer_ID_Oya]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Seikyu_Kubun]  DEFAULT ((0)) FOR [Seikyu_Kubun]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_SeikyuDate_Kubun]  DEFAULT ((0)) FOR [SeikyuDate_Kubun]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Toll_Kubun]  DEFAULT ((0)) FOR [Toll_Kubun]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Shime_Day]  DEFAULT ((0)) FOR [Shime_Day]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_De_lFlg]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

ALTER TABLE [dbo].[M_Customer] ADD  CONSTRAINT [DF_M_Customer_Seikyu_Customer_ID]  DEFAULT ((0)) FOR [Seikyu_Customer_ID]
GO


