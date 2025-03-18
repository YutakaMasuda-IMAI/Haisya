/****** Object:  Table [dbo].[M_Senzoku]    Script Date: 2024/05/16 9:49:08 ******/
SET ANSI_NULLS ON GO

SET QUOTED_IDENTIFIER ON GO

DROP TABLE [dbo].[M_Senzoku] GO 

CREATE TABLE [dbo].[M_Senzoku](
	[SenzokuID] [int] IDENTITY(1,1) NOT NULL,
	[Company_ID] [int] NOT NULL,
	[KokyakuId] [int] NOT NULL,
	[KokyakuCode] [nvarchar](50) NULL,
	[KokyakuTantouId] [int] NOT NULL,
	[Shiharai_Kubun] [int] NOT NULL,
	[Senzoku_Name] [nvarchar](50) NULL,
	[Monthly_Fee] [money] NOT NULL,
	[Daily_Fee] [money] NOT NULL,
	[Calc_Kubun] [int] NOT NULL,
 CONSTRAINT [PK_M_Senzoku] PRIMARY KEY CLUSTERED 
(
	[SenzokuID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_KokyakuId]  DEFAULT ((0)) FOR [KokyakuId]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_KokyakuTantouId]  DEFAULT ((0)) FOR [KokyakuTantouId]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_Shiharai_Kubun_1]  DEFAULT ((0)) FOR [Shiharai_Kubun]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_Monthly_Fee]  DEFAULT ((0)) FOR [Monthly_Fee]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_Daily_Fee]  DEFAULT ((0)) FOR [Daily_Fee]
GO


ALTER TABLE [dbo].[M_Senzoku] ADD  CONSTRAINT [DF_M_Senzoku_Fee_Kubun]  DEFAULT ((0)) FOR [Calc_Kubun]
GO


EXEC sys.sp_addextendedproperty @name = N'MS_Description',
@value = N'0:�Č����ƁA1:���z',
@level0type = N'SCHEMA',
@level0name = N'dbo',
@level1type = N'TABLE',
@level1name = N'M_Senzoku',
@level2type = N'COLUMN',
@level2name = N'Shiharai_Kubun' GO

EXEC sys.sp_addextendedproperty @name = N'MS_Description',
@value = N'0:���z����v�Z�A�P:���z����v�Z',
@level0type = N'SCHEMA',
@level0name = N'dbo',
@level1type = N'TABLE',
@level1name = N'M_Senzoku',
@level2type = N'COLUMN',
@level2name = N'Calc_Kubun' GO