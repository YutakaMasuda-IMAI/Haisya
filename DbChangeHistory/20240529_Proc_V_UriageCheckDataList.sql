


CREATE PROC [Proc_V_UriageCheckDataList]
(
@PRINT_DATE AS DATE
,@FROM_TOKUISAKI AS NVARCHAR(8)
,@TO_TOKUISAKI AS NVARCHAR(8)
,@SEIKYU_NENGETSU AS DATE
,@SHIME_DAY AS INT = 0
,@ZEI_KUBUN AS INT = 0
,@SEIKYUDATE_TO AS DATE = '1900/01/01'
,@SEIKYU_TANTOU AS INT = 0
)
AS

BEGIN


--DECLARE @PRINT_DATE AS DATE
--SET @PRINT_DATE = '2024/04/26'

--DECLARE @FROM_TOKUISAKI AS NVARCHAR(8)
--DECLARE @TO_TOKUISAKI AS NVARCHAR(8)

----SET @FROM_TOKUISAKI = '10577000'
----SET @TO_TOKUISAKI = '99999000'

--SET @FROM_TOKUISAKI = '70001001'
--SET @TO_TOKUISAKI = @FROM_TOKUISAKI


--DECLARE @SEIKYU_NENGETSU AS DATE
--SET @SEIKYU_NENGETSU = '2024/03/01'
--DECLARE @SHIME_DAY AS INT
--SET @SHIME_DAY = 0

--DECLARE @ZEI_KUBUN AS INT = 0


--DECLARE @SEIKYUDATE_TO AS DATE = '1900/01/01'
--SET @SEIKYUDATE_TO  = '2024/03/31'

--DECLARE @SEIKYU_TANTOU AS INT = 0



--------売上日ＴＯ指定は得意先一つ---------
IF (YEAR(@SEIKYUDATE_TO) != 1900)
BEGIN
	IF @FROM_TOKUISAKI <> @TO_TOKUISAKI
		BEGIN
			
			RETURN
		END
END


---------売上日条件の作成-------------
DECLARE @SHIME_DATE AS NVARCHAR(8)
SET @SHIME_DATE = CAST(YEAR(@SEIKYU_NENGETSU) AS NVARCHAR(4)) + CAST(FORMAT(@SEIKYU_NENGETSU,'MM') AS NVARCHAR(2)) + CAST(@SHIME_DAY AS NVARCHAR(2))


DECLARE @FROM_DATE AS DATE
IF @SHIME_DAY = 31
BEGIN
	SET @FROM_DATE = DATEFROMPARTS(YEAR(@SEIKYU_NENGETSU),MONTH(@SEIKYU_NENGETSU),1)
END
ELSE IF @SHIME_DAY = 0
BEGIN
	SET @FROM_DATE = DATEFROMPARTS(YEAR(@SEIKYU_NENGETSU),MONTH(@SEIKYU_NENGETSU),1)
END
ELSE
BEGIN
	IF (DAY(DATEADD(DAY,-1,@SEIKYU_NENGETSU)) >= @SHIME_DAY) AND (DAY(DATEADD(DAY,-1,DATEADD(MONTH,1,@SEIKYU_NENGETSU))) >= @SHIME_DAY)
		BEGIN
			SET @FROM_DATE = DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEFROMPARTS(YEAR(@SEIKYU_NENGETSU), MONTH(@SEIKYU_NENGETSU), @SHIME_DAY)))
		END
	ELSE
		BEGIN
			SET @FROM_DATE = @SEIKYU_NENGETSU
		END
END


DECLARE @TO_DATE AS DATE
IF @SHIME_DAY = 31
BEGIN
	SET @TO_DATE = DATEADD(DAY,-1,DATEADD(MONTH,1,@SEIKYU_NENGETSU))
END
ELSE IF @SHIME_DAY = 0
BEGIN
	SET @TO_DATE = DATEADD(DAY,-1,DATEADD(MONTH,1,@SEIKYU_NENGETSU))
END
ELSE
BEGIN
	IF DAY(DATEADD(DAY,-1,DATEADD(MONTH,1,@SEIKYU_NENGETSU))) >= @SHIME_DAY
	BEGIN
		SET @TO_DATE = DATEFROMPARTS(YEAR(@SEIKYU_NENGETSU),MONTH(@SEIKYU_NENGETSU),@SHIME_DAY)
	END
	ELSE
	BEGIN
		SET @TO_DATE = DATEADD(DAY,-1,DATEADD(MONTH,1,@SEIKYU_NENGETSU))
	END
END


IF (YEAR(@SEIKYUDATE_TO) != 1900)
BEGIN
	IF NOT @SEIKYUDATE_TO BETWEEN @FROM_DATE AND @TO_DATE
	BEGIN

		RETURN 
	END
	SET @TO_DATE = @SEIKYUDATE_TO 
END



PRINT @FROM_DATE
PRINT @TO_DATE




BEGIN

IF OBJECT_ID(N'tempdb..[#SEIKYU_LIST]') IS NOT NULL
BEGIN
	DROP TABLE [#SEIKYU_LIST]
END



CREATE TABLE [#SEIKYU_LIST]
(
[Seikyu_Month] DATE
,[Seikyu_Date] DATE
,[Shime_Day] INT
,[Customer_ID] INT
,[Customer_Name] [nvarchar](40) NULL
,[Seikyu_TantouID] INT
,[Seikyu_Tantou] NVARCHAR(50)
,[Mail_Title] [nvarchar](50) NULL
,[Mail_Address1] [nvarchar](50) NULL
,[Mail_Address2] [nvarchar](50) NULL
,[Phone1] [nvarchar](13) NULL
,[Phone2] [nvarchar](13) NULL
,[Fax1] [nvarchar](13) NULL
,[Fax2] [nvarchar](13) NULL

, [Zei_Kubun] INT
, [FROM_DATE] DATE
, [TO_DATE] DATE
, [Inquiry_Status] INT
, [Check_Status] INT
, [Seikyu_Changed] INT

, [Meisai_Count] MONEY
, [SeikyuUnchin] MONEY
, [Tatekaekin] MONEY
, [Anken_Count] INT
, [Kakutei_Count] INT
, [Zantei_Count] INT
, [Kari_Count] INT



)



END

BEGIN


INSERT INTO [#SEIKYU_LIST]
(
[Seikyu_Month]
,[Seikyu_Date]
,[Shime_Day]
,[Customer_ID]
,[Customer_Name]
,[Seikyu_TantouID]
,[Seikyu_Tantou]
,[Mail_Title]
,[Mail_Address1]
,[Mail_Address2]
,[Phone1]
,[Phone2]
,[Fax1]
,[Fax2]
,[Zei_Kubun]
,[FROM_DATE]
,[TO_DATE]
,[Inquiry_Status]
, [Check_Status] 
, [Seikyu_Changed] 

,[SeikyuUnchin]
,[Tatekaekin]

,[Meisai_Count]
,[Anken_Count]
,[Kakutei_Count]
,[Zantei_Count]
,[Kari_Count]


)


SELECT 
	@SEIKYU_NENGETSU
    ,UU.[Seikyu_Date]
    ,UU.[Shime_Day]
    ,UU.[Customer_ID]
	,C.[Customer_Name_Abbr]
	,C.[SeikyuTantouID]
	,CUG.[Display_Name] AS [Seikyu_Tantou_Name]
	,C.[Mail_Title]
    ,C.[Mail_Address1]
    ,C.[Mail_Address2]
	,C.[Phone1]
    ,C.[Phone2]
    ,C.[Fax1]
    ,C.[Fax2]
	,UU.[Zei_Kubun]
	,@FROM_DATE
	,@TO_DATE
	,0 AS [Inquiry_Status]
	,0 AS [Check_Status] 
	,0 AS [Seikyu_Changed] 


	,0 AS [Meisai_Count]
	,0 AS [SeikyuUnchin]
	,0 AS [Tatekaekin]
	,0 AS [Anken_Count]
	,0 AS [Kakutei_Count]
	,0 AS [Zantei_Count]
	,0 AS [Kari_Count]

  FROM 
  [dbo].[T_Uriage_Unchin] UU
  INNER JOIN [dbo].[M_Customer] C ON UU.Customer_ID = C.Customer_ID
  LEFT JOIN [dbo].[M_CompanyUser_Group] CUG ON C.[SeikyuTantouID] = CUG.[Group_ID]

  WHERE
  UU.[Del_Flg] = 0
  AND
  C.Customer_Code BETWEEN @FROM_TOKUISAKI AND @TO_TOKUISAKI
  AND
  CASE WHEN @SHIME_DAY = 0 THEN 0 ELSE UU.[Shime_Day]  END = @SHIME_DAY
  AND
  CASE WHEN @ZEI_KUBUN = 0 THEN 0 ELSE UU.[Zei_Kubun]  END = @ZEI_KUBUN
  AND
  CASE WHEN @SEIKYU_TANTOU = 0 THEN 0 ELSE C.[SeikyuTantouID] END = @SEIKYU_TANTOU

  
  -----Check_Kubun				1	WEB		2	帳票	
  ----Check_Status					0	発行済み			1	確認中			2	確認済			4	承認済
  UPDATE
  [#SEIKYU_LIST]
  SET
  [Inquiry_Status] = CS.[Check_Kubun]
  ,[Check_Status] = CS.[Check_Status]
  FROM
　[dbo].[T_Check_Seikyu] CS
  WHERE
  [#SEIKYU_LIST].[Customer_ID] = CS.[Customer_ID]
  AND
  [#SEIKYU_LIST].[Seikyu_Month] = CS.[Seikyu_Month]
  AND
  [#SEIKYU_LIST].[Shime_Day] = CS.[Shime_Day]

  ----[Seikyu_Changed]
  UPDATE
  [#SEIKYU_LIST]
  SET
  [Seikyu_Changed] = CSD.[Change_Flg]
  FROM
　[dbo].[T_Check_Seikyu] CS
  INNER JOIN [dbo].[T_Check_Seikyu_Done] CSD ON
  CS.[Check_Seikyu_ID] = CSD.[Check_Seikyu_ID]
  WHERE
  [#SEIKYU_LIST].[Customer_ID] = CS.[Customer_ID]
  AND
  [#SEIKYU_LIST].[Seikyu_Month] = CS.[Seikyu_Month]
  AND
  [#SEIKYU_LIST].[Shime_Day] = CS.[Shime_Day]



  -----明細件数
  UPDATE
  [#SEIKYU_LIST]
  SET
  [Meisai_Count] = X.AA
  ,[SeikyuUnchin] = X.[SeikyuUnchin]
  ,[Tatekaekin] = X.[Tatekaekin]
  FROM
  (
  SELECT
  UU.[Customer_ID]
  ,UU.[Shime_Day]
  ,COUNT(*) AS AA
  ,SUM(UU.[SeikyuUnchin]) AS [SeikyuUnchin]
  ,SUM(UU.[Tatekaekin]) AS [Tatekaekin]
  FROM
  [#SEIKYU_LIST] TEMP

  INNER JOIN [dbo].[T_Uriage_Unchin] UU ON
  TEMP.[Customer_ID] = UU.[Customer_ID]
  AND TEMP.[Shime_Day] = UU.[Shime_Day]

  WHERE
  UU.[Seikyu_Date] BETWEEN [FROM_DATE] AND [TO_DATE]
  GROUP BY
  UU.[Customer_ID]
  ,UU.[Shime_Day]
  )X
  WHERE
  [#SEIKYU_LIST].[Customer_ID] = X.[Customer_ID]
  AND
  [#SEIKYU_LIST].[Shime_Day] = X.[Shime_Day]


  -----案件件数、請求確定件数、暫定件数、仮件数
  UPDATE
  [#SEIKYU_LIST]
  SET
  [Anken_Count] = AA
  ,[Kakutei_Count] = [KAKUTEI]
  ,[Zantei_Count] = [ZANTEI]
  ,[Kari_Count] = [KARI]
  FROM
  (
  SELECT
  W.[Customer_ID]
  ,W.[Shime_Day]
  ,COUNT(*) AS AA
  ,SUM([KARI]) AS [KARI]
  ,SUM([ZANTEI]) AS [ZANTEI]
  ,SUM([KAKUTEI]) AS [KAKUTEI]
  FROM
  (
  SELECT
  UU.[Customer_ID]
  ,UU.[Shime_Day]
  ,U.[Anken_ID]
  ,CASE WHEN U.[Reg_Status] = 0 THEN 1 ELSE 0 END AS [KARI]
  ,CASE WHEN U.[Reg_Status] = 1 THEN 1 ELSE 0 END AS [ZANTEI]
  ,CASE WHEN U.[Reg_Status] = 2 THEN 1 ELSE 0 END AS [KAKUTEI]
  FROM
  [#SEIKYU_LIST] TEMP

  INNER JOIN [dbo].[T_Uriage_Unchin] UU ON
  TEMP.[Customer_ID] = UU.[Customer_ID]
  AND TEMP.[Shime_Day] = UU.[Shime_Day]

  INNER JOIN [dbo].[T_Uriage] U ON
  UU.[Uriage_ID] = U.[Uriage_ID]

  WHERE
  UU.[Seikyu_Date] BETWEEN [FROM_DATE] AND [TO_DATE]
  GROUP BY
  UU.[Customer_ID]
  ,UU.[Shime_Day]
  ,U.[Anken_ID]
  ,U.[Reg_Status]
  )W
  GROUP BY
  W.[Customer_ID]
  ,W.[Shime_Day]
  )X
  WHERE
  [#SEIKYU_LIST].[Customer_ID] = X.[Customer_ID]
  AND
  [#SEIKYU_LIST].[Shime_Day] = X.[Shime_Day]









END



SELECT * FROM [#SEIKYU_LIST]


END