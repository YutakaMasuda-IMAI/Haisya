USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Uriage_Unsyu]    Script Date: 2024/05/28 11:59:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Uriage_Unsyu](
	[Uriage_Unsyu_ID] [int] IDENTITY(1,1) NOT NULL,
	[Uriage_ID] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Default_Kubun] [int] NOT NULL,
	[Unsyu_Date] [date] NOT NULL,
	[SyaryoManagement_ID] [int] NOT NULL,
	[Driver_ID] [int] NOT NULL,
	[Unsyu_Kubun] [int] NOT NULL,
	[Seisan] [money] NOT NULL,
	[KojinFutan] [money] NOT NULL,
	[KojinUnsyu] [money] NOT NULL,
	[Route_Teate] [money] NOT NULL,
	[Route_OverTime] [money] NOT NULL,
	[Route_Midnight] [money] NOT NULL,
	[Del_Flg] [bit] NOT NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Uriage_Unsyu] PRIMARY KEY CLUSTERED 
(
	[Uriage_Unsyu_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Sort]  DEFAULT ((0)) FOR [Sort]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Default_Kubun]  DEFAULT ((0)) FOR [Default_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Driver_ID]  DEFAULT ((0)) FOR [Driver_ID]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Unsyu_Kubun]  DEFAULT ((0)) FOR [Unsyu_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Seisan]  DEFAULT ((0)) FOR [Seisan]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Seisan1]  DEFAULT ((0)) FOR [KojinFutan]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_KojinUnsyu]  DEFAULT ((0)) FOR [KojinUnsyu]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Route_Teate]  DEFAULT ((0)) FOR [Route_Teate]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Route_OverTime]  DEFAULT ((0)) FOR [Route_OverTime]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Route_Midnight]  DEFAULT ((0)) FOR [Route_Midnight]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Del_Flg]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage_Unsyu] ADD  CONSTRAINT [DF_T_Uriage_Unsyu_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'àƒåèÅAãÛé‘âÒëóÅAîëÇ‹ÇËìô' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage_Unsyu', @level2type=N'COLUMN',@level2name=N'Unsyu_Kubun'
GO


