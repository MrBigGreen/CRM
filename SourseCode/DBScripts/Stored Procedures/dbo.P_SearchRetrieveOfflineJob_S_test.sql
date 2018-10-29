SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[P_SearchRetrieveOfflineJob_S_test]
(
  @conditionType varchar(50),
  @condition varchar(50),
  @workAddress varchar(5000), --工作地点
  @jobType varchar(5000), --职位类别
  @vocationType varchar(5000), --行业类别
  @publishTime varchar(50), --发布时间
  @InterviewDate varchar(50),
  @WorkExperenceCode varchar(50),
  @EducationCode varchar(50),
  @CompanyNatureCode varchar(50),
  @PositionTypeCode varchar(50),
  @sys_PageIndex int,      --当前页数
  @sys_PageSize int ,        --页大小
  @PCount int output,    --总页数输出
  @RCount int output    --总记录数输出
)
AS

SET XACT_ABORT ON

BEGIN            
	exec srv_lnk.bridgehr_dev.dbo.P_SearchRetrieveOfflineJob_S_test @conditionType=N'0',@condition=N'',@workAddress=N'1adebba9-81bd-4860-8eb8-104f348451d1,0f6fe6f0-d1f3-47c2-bb61-fe607aa772ab',@jobType=N'fe9cc11f-b1df-4abf-a751-37fb87945839,1d397551-0e01-4972-aad1-92793ed2c840',@vocationType=N'1fb9c298-8997-41e1-a5d4-bea55a6ddff1',@publishTime=N'',@InterviewDate=N'',@WorkExperenceCode=N'',@EducationCode=N'',@CompanyNatureCode=N'',
	@PositionTypeCode=N'',@sys_PageIndex=1,@sys_PageSize=7,@PCount=@PCount output,@RCount=@RCount output

END
SET XACT_ABORT OFF


GO
