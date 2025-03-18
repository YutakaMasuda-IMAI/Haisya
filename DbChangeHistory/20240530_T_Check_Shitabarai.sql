USE [Haisya]
GO

/****** Object:  Table [dbo].[T_Check_Shitabarai]    Script Date: 2024/05/30 18:18:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[T_Check_Shitabarai]
GO

CREATE TABLE [dbo].[T_Check_Shitabarai](
	[Check_Shitabarai_ID] [int] IDENTITY(1,1) NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Print_Pattern] [int] NOT NULL,
	[Check_Kubun] [int] NOT NULL,
	[Check_Status] [int] NOT NULL,
	[Customer_ID] [int] NOT NULL,
	[Shiharai_Month] [date] NOT NULL,
	[Shime_Day] [int] NOT NULL,
	[Del_Datetime] [datetime] NULL,
	[Print_Datetime] [datetime] NOT NULL,
	[Print_Date] [date] NOT NULL,
	[Print_To_Date] [date] NULL,
	[Mail_Address1] [nvarchar](50) NULL,
	[Mail_Address2] [nvarchar](50) NULL,
	[Insert_Datetime] [datetime] NOT NULL,
	[Insert_User] [int] NOT NULL,
	[Update_Datetime] [datetime] NOT NULL,
	[Update_User] [int] NOT NULL,
 CONSTRAINT [PK_T_Check_Shitabarai] PRIMARY KEY CLUSTERED 
(
	[Check_Shitabarai_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Company_ID]  DEFAULT ((0)) FOR [Company_ID]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Print_Pattern]  DEFAULT ((0)) FOR [Print_Pattern]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Check_Kubun]  DEFAULT ((0)) FOR [Check_Kubun]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Check_Status]  DEFAULT ((0)) FOR [Check_Status]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Print_ShimeDay]  DEFAULT ((0)) FOR [Shime_Day]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Print_Sort]  DEFAULT (getdate()) FOR [Print_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Insert_Datetime]  DEFAULT (getdate()) FOR [Insert_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Update_Datetime]  DEFAULT (getdate()) FOR [Update_Datetime]
GO

ALTER TABLE [dbo].[T_Check_Shitabarai] ADD  CONSTRAINT [DF_T_Check_Shitabarai_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：WEB、2：帳票' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Check_Shitabarai', @level2type=N'COLUMN',@level2name=N'Check_Kubun'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：発行済み、1：確認中、2：確認済、3：未定、4：承認済' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Check_Shitabarai', @level2type=N'COLUMN',@level2name=N'Check_Status'
GO


