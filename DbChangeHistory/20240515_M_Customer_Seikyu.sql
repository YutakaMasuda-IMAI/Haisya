/****** Object:  Table [dbo].[M_Customer_Seikyu]    Script Date: 2024/05/15 9:53:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_Customer_Seikyu](
	[Customer_ID] [int] NOT NULL,
	[SeikyuUnchin_Name] [nvarchar](10) NULL,
	[Tatekaekin_Name] [nvarchar](10) NULL,
	[Warimashi1_Name] [nvarchar](10) NULL,
	[Warimashi2_Name] [nvarchar](10) NULL,
	[Warimashi3_Name] [nvarchar](10) NULL,
	[Warimashi4_Name] [nvarchar](10) NULL,
	[Warimashi5_Name] [nvarchar](10) NULL,
	[SeikyuTotal_Name] [nvarchar](10) NULL,
	[Warimashi1_Visible] [bit] NULL,
	[Warimashi2_Visible] [bit] NULL,
	[Warimashi3_Visible] [bit] NULL,
	[Warimashi4_Visible] [bit] NULL,
	[Warimashi5_Visible] [bit] NULL,
	[Warimashi1_Calc] [nvarchar](50) NULL,
	[Warimashi2_Calc] [nvarchar](50) NULL,
	[Warimashi3_Calc] [nvarchar](50) NULL,
	[Warimashi4_Calc] [nvarchar](50) NULL,
	[Warimashi5_Calc] [nvarchar](50) NULL,
	[SeikyuTotal_Calc] [nvarchar](50) NULL,
 CONSTRAINT [PK_M_Customer_Seikyu] PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_M_Customer_Seikyu_Warimashi1_Visible]  DEFAULT ((0)) FOR [Warimashi1_Visible]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Visible1]  DEFAULT ((0)) FOR [Warimashi2_Visible]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Visible2]  DEFAULT ((0)) FOR [Warimashi3_Visible]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Visible3]  DEFAULT ((0)) FOR [Warimashi4_Visible]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Visible4]  DEFAULT ((0)) FOR [Warimashi5_Visible]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Visible1_1]  DEFAULT ((0)) FOR [Warimashi1_Calc]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Calc1]  DEFAULT ((0)) FOR [Warimashi2_Calc]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Calc2]  DEFAULT ((0)) FOR [Warimashi3_Calc]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Calc3]  DEFAULT ((0)) FOR [Warimashi4_Calc]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Calc4]  DEFAULT ((0)) FOR [Warimashi5_Calc]
GO

ALTER TABLE [dbo].[M_Customer_Seikyu] ADD  CONSTRAINT [DF_Table_1_Warimashi1_Calc41]  DEFAULT ((0)) FOR [SeikyuTotal_Calc]
GO


