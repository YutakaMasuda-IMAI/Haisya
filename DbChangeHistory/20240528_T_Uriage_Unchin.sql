USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Uriage_Unchin]    Script Date: 2024/05/28 11:59:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Uriage_Unchin](
	[Uriage_Unchin_ID] [int] IDENTITY(1,1) NOT NULL,
	[Uriage_ID] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Default_Kubun] [int] NOT NULL,
	[Seikyu_Date] [date] NOT NULL,
	[Shime_Day] [int] NOT NULL,
	[Customer_ID] [int] NOT NULL,
	[Tsumi] [nvarchar](20) NULL,
	[Oroshi] [nvarchar](20) NULL,
	[Luggage] [nvarchar](20) NULL,
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
	[Del_Flg] [bit] NOT NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Uriage_Unchin] PRIMARY KEY CLUSTERED 
(
	[Uriage_Unchin_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Sort]  DEFAULT ((0)) FOR [Sort]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Default_Kubun]  DEFAULT ((0)) FOR [Default_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Shime_Day]  DEFAULT ((0)) FOR [Shime_Day]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Customer_Tantou_ID]  DEFAULT ((0)) FOR [Customer_ID]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Qty]  DEFAULT ((0)) FOR [Qty]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Unit]  DEFAULT ((0)) FOR [Unit]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_CalcPrice]  DEFAULT ((0)) FOR [CalcPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin]  DEFAULT ((0)) FOR [SeikyuUnchin]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin1]  DEFAULT ((0)) FOR [Tatekaekin]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin2]  DEFAULT ((0)) FOR [Warimashi1]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin3]  DEFAULT ((0)) FOR [Warimashi2]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin4]  DEFAULT ((0)) FOR [Warimashi3]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin5]  DEFAULT ((0)) FOR [Warimashi4]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_SeikyuUnchin6]  DEFAULT ((0)) FOR [Warimashi5]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Warimashi51]  DEFAULT ((0)) FOR [SeikyuTotal]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Del_Flg]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Unchin] ADD  CONSTRAINT [DF_T_Uriage_Unchin_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'êîó ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unchin', @level2type=N'COLUMN',@level2name=N'Qty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'íPà ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unchin', @level2type=N'COLUMN',@level2name=N'Unit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'íPâø' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unchin', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'åvéZâ^í¿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unchin', @level2type=N'COLUMN',@level2name=N'CalcPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:â€ê≈ÅA1:îÒâ€ê≈' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unchin', @level2type=N'COLUMN',@level2name=N'Zei_Kubun'
GO


