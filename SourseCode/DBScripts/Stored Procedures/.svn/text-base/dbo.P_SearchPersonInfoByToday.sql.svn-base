SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_SearchPersonInfoByToday]
	(
	@searchDay NVARCHAR(50),--呼叫日期
	@typeValue NVARCHAR(50),--会员级别
	@levelValue NVARCHAR(50),--简历层次
	@jobHope NVARCHAR(50),--求职意向
	@eduValue NVARCHAR(50),--学历
	@ageValue NVARCHAR(50),--年龄
	@sexValue  NVARCHAR(50),--性别
	@prov  NVARCHAR(50),--省
	@city NVARCHAR(50),--市
	@area NVARCHAR(50),--区
	@searchName   NVARCHAR(50),--查询条件	
	@type NVARCHAR(10),--状态：（全部 ：0；时间：1；条件：2） 
	@userid   NVARCHAR(50)--用户ID
	)
AS
BEGIN
DECLARE
	@SQL NVARCHAR (MAX)
SET @SQL = 'SELECT t.ImportResumeID,t.SysPersonID,t.OfferUserID, t.ResumeName,t.PhoneNumber,t.GenderCode,t.BOB, t.EmailAddress
      ,t.QQID, t.JobSalaryCode,t.CVLevel
      ,t.JobStatusCode ,t.ResumeContent ,t.IsDel
      ,t.CreatedBy
      ,t.LastUpdatedTime
      ,t.LastUpdatedBy,
e.CodeValue AS EducationCode,s.CodeValue+c.CodeValue+q.CodeValue AS AddressID,t.ResumeSource,
t.CallCount,k.CallTime as CreatedTime
FROM  TB_ImportResume t,
TB_Address a,
srv_lnk.bridgehr_dev.dbo.CodeTable e,
srv_lnk.bridgehr_dev.dbo.CodeTable s,
 srv_lnk.bridgehr_dev.dbo.CodeTable c,
 srv_lnk.bridgehr_dev.dbo.CodeTable q,
 TB_CallTask k '
IF @type='0'
BEGIN
SET @SQL=@SQL+	N'WHERE t.EducationCode=e.CodeID AND t.AddressID=a.AddressID AND a.ProvinceCode=s.CodeID
 AND a.CityCode=c.CodeID AND a.DistrictCode=q.CodeID AND t.SysPersonID=k.SysPersonID
 AND k.CallStatus=1 AND k.CallTime<=GETDATE()'

END
ELSE IF @type='1'
BEGIN
SET @SQL=@SQL+	N'WHERE t.EducationCode=e.CodeID AND t.AddressID=a.AddressID AND a.ProvinceCode=s.CodeID
 AND a.CityCode=c.CodeID AND a.DistrictCode=q.CodeID AND t.SysPersonID=k.SysPersonID
 AND k.CallStatus=1'
IF @searchDay <> ''
SET @SQL =@SQL + N'and k.CallTime=''' +@searchDay + ''' '
IF @searchDay = ''
SET @SQL =@SQL + N'and k.CallTime=GETDATE()'
END
ELSE IF  @type='2'
BEGIN
IF @jobHope <> ''
SET @SQL =@SQL + N',TB_JobPost j WHERE t.[ImportResumeID]=j.[ImportResumeID] AND j.[JobPostName] IN (''' +@jobHope + ''') '
IF @levelValue <> '0'
SET @SQL =@SQL + N'and t.CVLevel=''' +@levelValue + ''' '
IF @eduValue <> '0'
SET @SQL =@SQL + N'and t.EducationCode=''' +@eduValue + ''' '
IF @sexValue <> '0'
SET @SQL =@SQL + N'and t.GenderCode=''' +@sexValue + ''' '
IF @ageValue = '1'
SET @SQL =@SQL + N'and DATEDIFF(YEAR,t.BOB,GETDATE())<18 '
IF @ageValue = '2'
SET @SQL =@SQL + N'and 18 between DATEDIFF(YEAR,t.BOB,GETDATE()) and 30'
IF @ageValue = '3'
SET @SQL =@SQL + N'and DATEDIFF(YEAR,t.BOB,GETDATE())>30'
IF @prov <> ''
SET @SQL =@SQL + N'and s.CodeID=''' +@prov + ''' '
IF @city <> ''
SET @SQL =@SQL + N'and c.CodeID=''' +@city + ''' '
IF @city <> ''
SET @SQL =@SQL + N'and q.CodeID=''' +@area + ''' '
IF @searchName <> ''
SET @SQL =@SQL + N'and (t.ResumeName like ''%' +@searchName + '%'' or t.PhoneNumber like ''%' +@searchName + '%'')'
SET @SQL =@SQL + N' and t.EducationCode=e.CodeID AND t.AddressID=a.AddressID AND a.ProvinceCode=s.CodeID
 AND a.CityCode=c.CodeID AND a.DistrictCode=q.CodeID AND t.SysPersonID=k.SysPersonID
 AND k.CallStatus=1 AND k.CallTime<=GETDATE()'
END

SET @SQL =@SQL + N' AND t.SysPersonID=''' +@userid + ''' and t.IsDel=0 ORDER BY k.CallTime DESC,t.CallCount ASC'
PRINT @SQL
	EXEC (@SQL)
	
END
GO
