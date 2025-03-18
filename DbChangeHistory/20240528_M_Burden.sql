USE [Haisya]
GO

/****** Object:  Table [dbo].[M_Burden]    Script Date: 2024/05/28 11:11:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_Burden](
	[Burden_ID] [int] IDENTITY(1,1) NOT NULL,
	[Burden_Group_ID] [int] NOT NULL,
	[Company_ID] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Burden_Name] [nvarchar](50) NOT NULL,
	[Unit_Name] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Del_Flg] [bit] NOT NULL,
	[Insert_Datetime] [datetime] NULL,
	[Insert_User] [int] NULL,
	[Update_Datetime] [datetime] NULL,
	[Update_User] [int] NULL,
 CONSTRAINT [PK_M_Burden] PRIMARY KEY CLUSTERED 
(
	[Burden_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[M_Burden] ADD  CONSTRAINT [DF_M_Burden_SortOrder]  DEFAULT ((0)) FOR [SortOrder]
GO

ALTER TABLE [dbo].[M_Burden] ADD  CONSTRAINT [DF_M_Burden_Del_Flg]  DEFAULT ((0)) FOR [Del_Flg]
GO

ALTER TABLE [dbo].[M_Burden] ADD  CONSTRAINT [DF_M_Burden_Insert_User]  DEFAULT ((0)) FOR [Insert_User]
GO

ALTER TABLE [dbo].[M_Burden] ADD  CONSTRAINT [DF_M_Burden_Update_User]  DEFAULT ((0)) FOR [Update_User]
GO


