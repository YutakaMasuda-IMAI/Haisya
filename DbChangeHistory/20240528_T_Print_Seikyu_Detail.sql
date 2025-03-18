USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Print_Seikyu_Detail]    Script Date: 2024/05/28 13:52:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Print_Seikyu_Detail](
	[Print_Seikyu_ID] [int] NOT NULL,
	[データ区分] [int] NOT NULL,
	[Anken_ID] [int] NOT NULL,
	[Anken_ID_Detail] [int] NOT NULL,
	[Uriage_Unchin_ID] [int] NOT NULL,
	[Oroshi_Date] [smalldatetime] NULL,
	[Syaban] [nvarchar](5) NULL,
	[SyasyuKataName] [nvarchar](16) NULL,
	[DriverName] [nvarchar](16) NULL,
	[Tsumi] [nvarchar](20) NULL,
	[Oroshi] [nvarchar](20) NULL,
	[Luggage] [nvarchar](20) NULL,
	[Work_Name] [nvarchar](100) NULL,
	[Qty] [float] NOT NULL,
	[Unit] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[CalcPrice] [money] NOT NULL,
	[SeikyuUnchin] [money] NOT NULL,
	[Tatekaekin] [money] NOT NULL,
	[Warimashi1] [money] NOT NULL,
	[Warimashi2] [money] NOT NULL,
	[Warimashi3] [money] NOT NULL,
	[Warimashi4] [money] NOT NULL,
	[Warimashi5] [money] NOT NULL,
	[SeikyuTotal] [money] NOT NULL,
	[Zei_Kubun] [int] NULL,
	[YosyaDriver_ID] [int] NOT NULL,
	[Yosya_ID] [int] NOT NULL,
	[Yosya_Name] [nvarchar](50) NOT NULL,
	[Yosya_Driver_Name] [nvarchar](50) NOT NULL,
	[Remarks_ID] [int] NULL,
	[Remaks] [nvarchar](20) NULL,
	[FROM_DATE] [date] NULL,
	[TO_DATE] [date] NULL,
 CONSTRAINT [PK_T_Print_Seikyu_Detail] PRIMARY KEY CLUSTERED 
(
	[Print_Seikyu_ID] ASC,
	[データ区分] ASC,
	[Anken_ID] ASC,
	[Anken_ID_Detail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Print_Seikyu_Detail] ADD  CONSTRAINT [DF_T_Print_Seikyu_Detail_Uriage_Unchin_ID]  DEFAULT ((0)) FOR [Uriage_Unchin_ID]
GO


