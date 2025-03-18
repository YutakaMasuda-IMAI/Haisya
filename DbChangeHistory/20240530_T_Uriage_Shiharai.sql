USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Uriage_Shiharai]    Script Date: 2024/05/30 18:14:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Uriage_Shiharai]
GO

CREATE TABLE [dbo].[T_Uriage_Shiharai](
	[Uriage_Shiharai_ID] [int] IDENTITY(1,1) NOT NULL,
	[Uriage_ID] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Default_Kubun] [int] NOT NULL,
	[Shiharai_Date] [date] NOT NULL,
	[Shime_Day] [int] NOT NULL,
	[Yosya_ID] [int] NOT NULL,
	[Tsumi] [nvarchar](20) NULL,
	[Oroshi] [nvarchar](20) NULL,
	[Luggage] [nvarchar](20) NULL,
	[Qty] [float] NOT NULL,
	[Unit] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[CalcPrice] [money] NOT NULL,
	[ShiharaiPrice] [money] NOT NULL,
	[WarimashiPrice] [money] NOT NULL,
	[Tatekaekin] [money] NOT NULL,
	[Zei_Kubun] [int] NOT NULL,
	[Remarks] [nvarchar](255) NULL,
	[Del_Flg] [bit] NOT NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Uriage_Shiharai] PRIMARY KEY CLUSTERED 
(
	[Uriage_Shiharai_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Sort]  DEFAULT ((0)) FOR [Sort]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Default_Kubun]  DEFAULT ((0)) FOR [Default_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Shime_Day]  DEFAULT ((0)) FOR [Shime_Day]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Qty]  DEFAULT ((0)) FOR [Qty]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Unit]  DEFAULT ((0)) FOR [Unit]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_UnitPrice]  DEFAULT ((0)) FOR [UnitPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_CalcPrice]  DEFAULT ((0)) FOR [CalcPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_Table_1_UnitPrice]  DEFAULT ((0)) FOR [ShiharaiPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_Table_1_CalcPrice]  DEFAULT ((0)) FOR [WarimashiPrice]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_Table_1_SeikyuUnchin]  DEFAULT ((0)) FOR [Tatekaekin]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Zei_Kubun]  DEFAULT ((0)) FOR [Zei_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Del_Flg]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Shiharai] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'êîó ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'Qty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'íPà ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'Unit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'íPâø' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'åvéZâ^í¿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'CalcPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'íPâø' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'ShiharaiPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'åvéZâ^í¿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'WarimashiPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:â€ê≈ÅA1:îÒâ€ê≈' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Shiharai', @level2type=N'COLUMN',@level2name=N'Zei_Kubun'
GO


