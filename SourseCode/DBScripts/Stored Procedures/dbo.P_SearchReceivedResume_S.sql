SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


-- =============================================
-- 查询收到的简历
-- create by chand
-- create date 2015-2-25
-- =============================================
CREATE PROCEDURE [dbo].[P_SearchReceivedResume_S]
	(
	@CompanyInterviewJobID nvarchar(20),	--职位编号
	@Keyword nvarchar(50),		--关键字
	@DataType nvarchar(2) ,				--数据来源 1猎头推荐 2内部匹配
	@SysPersonID nvarchar(500),	--归属人
	@StartDate varchar(20),		--投递开始时间
	@EndDate varchar(20),		--投递结束时间
	@sys_PageIndex int,      --当前页数
	@sys_PageSize int ,        --页大小
	@PCount int output,    --总页数输出
	@RCount int output    --总记录数输出
	)
AS
BEGIN
declare @sql VARCHAR(MAX)
set @sql=' select c.ImportResumeID,c.ResumeName,c.OfferUserID,c.GenderCode,c.PhoneNumber,c.EmailAddress,c.ResumeSource,c.WorkYearCode,
	e.MyName as SysPersonName,code1.CodeValue as EducationName,code2.CodeValue as ProvinceCode,code3.CodeValue as CityCode,
	code4.CodeValue as DistrictCode,c.SysPersonID,a.CreatedTime,a.CreatedBy
	from srv_lnk.bridgehr_dev.dbo.JobhunterResumeInterview a left join srv_lnk.bridgehr_dev.dbo.Resume b on a.ResumeID=b.ResumeID
	left join TB_ImportResume c on b.UserID=c.OfferUserID 
	left join TB_Address d on c.AddressID=d.AddressID
	left join SysPerson e on e.Id=c.SysPersonID	
	left join srv_lnk.bridgehr_dev.dbo.CodeTable code1 on code1.CodeID=c.EducationCode
	left join srv_lnk.bridgehr_dev.dbo.CodeTable code2 on code1.CodeID=d.ProvinceCode
	left join srv_lnk.bridgehr_dev.dbo.CodeTable code3 on code1.CodeID=d.CityCode
	left join srv_lnk.bridgehr_dev.dbo.CodeTable code4 on code1.CodeID=d.DistrictCode
	where a.IsDelete=0 and a.CompanyInterviewJobID='+@CompanyInterviewJobID 
		PRINT @sql
	if isnull(@Keyword,'') !=''
	begin 
		--关键字
		set @sql+= '  and (c.ResumeName like ''%'+@Keyword+'%'' or c.PhoneNumber like ''%'+@Keyword+'%'' or c.EmailAddress like ''%'+@Keyword+'%'' )'
	end
	
	if @DataType='1'
	begin 
		--猎头推荐
		set @sql+=' and c.ResumeSource=1'
	end
	else if @DataType='2'
	begin 
		--内部匹配
		set @sql+=' and c.ResumeSource=2';
	end
	
	if isnull(@SysPersonID,'')!=''
	begin 

		set @SysPersonID+=','
		declare @str nvarchar(100)
		declare @demo nvarchar(max)		
		set @demo=''
		--归属人 ","分割
		while(@SysPersonID<>'')
		begin
			set @str=left(@SysPersonID,charindex(',',@SysPersonID,1)-1)
			set @SysPersonID=stuff(@SysPersonID,1,charindex(',',@SysPersonID,1),'') 
				 
			if @str<>'' 
			begin 
				set @demo+=' c.SysPersonID='''+@str+''' or '
			end
		end
		if @demo<>''
		begin 
		set @sql+= ' and ('+left(@demo,len(@demo)-3)+') '
		end
	end
	
	if @StartDate<>'' and @EndDate<>''
	begin 
		set @sql+=' and a.CreatedTime >='''+@StartDate+''' and a.CreatedTime <='''+@EndDate+''''
	end
	 
	PRINT @sql
	DECLARE @TotalPage1 INT ,@TotalRecordNum1 INT

		EXEC	[dbo].[P_PageShow]
				@Sql = @sql,
				@Order = N' CreatedTime desc',
				@CurrentPage = @sys_PageIndex,
				@PageSize = @sys_PageSize,
				@TotalPage = @TotalPage1 OUTPUT,
				@TotalCount = @TotalRecordNum1 OUTPUT 

		SET @PCount=@TotalPage1
		SET @RCount=@TotalRecordNum1
		RETURN @@ERROR
END


 
GO
