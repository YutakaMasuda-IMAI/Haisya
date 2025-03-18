USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Print_Shitabarai_Detail]    Script Date: 2024/05/30 18:20:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Print_Shitabarai_Detail]
GO

CREATE TABLE [dbo].[T_Print_Shitabarai_Detail](
	[Print_Shitabarai_ID] [int] NOT NULL,
	[Data_Kubun] [int] NOT NULL,
	[Data_Sort] [int] NOT NULL,
	[Uriage_Shiharai_ID] [int] NOT NULL,
	[Anken_ID] [int] NOT NULL,
	[Anken_ID_Detail] [int] NOT NULL,
	[Display_Date] [smalldatetime] NULL,
	[Syaban] [nvarchar](5) NULL,
	[SyasyuKataName] [nvarchar](16) NULL,
	[Tsumi] [nvarchar](20) NULL,
	[Oroshi] [nvarchar](20) NULL,
	[Luggage] [nvarchar](20) NULL,
	[Work_Name] [nvarchar](100) NULL,
	[Qty] [float] NOT NULL,
	[Unit] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[CalcPrice] [money] NOT NULL,
	[ShiharaiUnchin] [money] NOT NULL,
	[Tatekaekin] [money] NOT NULL,
	[Warimashi1] [money] NOT NULL,
	[Warimashi2] [money] NOT NULL,
	[Warimashi3] [money] NOT NULL,
	[Warimashi4] [money] NOT NULL,
	[Warimashi5] [money] NOT NULL,
	[ShiharaiTotal] [money] NOT NULL,
	[Zei_Kubun] [int] NULL,
	[YosyaDriver_ID] [int] NOT NULL,
	[Yosya_ID] [int] NOT NULL,
	[Yosya_Name] [nvarchar](50) NOT NULL,
	[Yosya_Driver_Name] [nvarchar](50) NOT NULL,
	[Remarks_ID] [int] NULL,
	[Remaks] [nvarchar](20) NULL,
	[FROM_DATE] [date] NULL,
	[TO_DATE] [date] NULL,
 CONSTRAINT [PK_T_Print_Shitabarai_Detail_1] PRIMARY KEY CLUSTERED 
(
	[Print_Shitabarai_ID] ASC,
	[Data_Kubun] ASC,
	[Data_Sort] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Print_Shitabarai_Detail] ADD  CONSTRAINT [DF_T_Print_Shitabarai_Detail_Uriage_Shiharai_ID]  DEFAULT ((0)) FOR [Uriage_Shiharai_ID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：ヘッダー、2：入金、３：案件明細' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Print_Shitabarai_Detail', @level2type=N'COLUMN',@level2name=N'Data_Kubun'
GO


