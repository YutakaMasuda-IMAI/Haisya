USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Uriage]    Script Date: 2024/05/29 16:49:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Uriage]
GO

CREATE TABLE [dbo].[T_Uriage](
	[Uriage_ID] [int] IDENTITY(1,1) NOT NULL,
	[Reg_Status] [int] NOT NULL,
	[Anken_ID] [int] NOT NULL,
	[Haisya_Date] [date] NOT NULL,
	[Nippou_ID] [int] NOT NULL,
	[SeikyuDate_Kubun] [int] NOT NULL,
	[Seikyu_Kubun] [int] NOT NULL,
	[SenzokuID] [int] NOT NULL,
	[Tatekae_Over_Kubun] [int] NOT NULL,
	[Tatekae_Over_Reason] [nvarchar](255) NULL,
	[Shiharai_Over_Kubun] [int] NOT NULL,
	[Shiharai_Over_Reason] [nvarchar](255) NULL,
	[Tatekae_Over_Approval] [int] NOT NULL,
	[Shiharai_Over_Approval] [int] NOT NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Uriage] PRIMARY KEY CLUSTERED 
(
	[Uriage_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Status]  DEFAULT ((0)) FOR [Reg_Status]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_SeikyuDate_Kubun]  DEFAULT ((0)) FOR [SeikyuDate_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_Table_1_Seikyu_Kubun1]  DEFAULT ((0)) FOR [Seikyu_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_SenzokuID]  DEFAULT ((0)) FOR [SenzokuID]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Tatekae_Over_Kubun]  DEFAULT ((0)) FOR [Tatekae_Over_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Over_Kubun]  DEFAULT ((0)) FOR [Shiharai_Over_Kubun]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Tatekae_Over_Approval]  DEFAULT ((0)) FOR [Tatekae_Over_Approval]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Shiharai_Over_Approval]  DEFAULT ((0)) FOR [Shiharai_Over_Approval]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Uriage] ADD  CONSTRAINT [DF_T_Uriage_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：仮登録、1：暫定登録、2：確定登録' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage', @level2type=N'COLUMN',@level2name=N'Reg_Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：配車日（積日）、1：卸日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage', @level2type=N'COLUMN',@level2name=N'SeikyuDate_Kubun'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：案件単位、1：卸し単位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Uriage', @level2type=N'COLUMN',@level2name=N'Seikyu_Kubun'
GO


