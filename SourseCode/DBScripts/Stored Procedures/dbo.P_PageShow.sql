SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[P_PageShow]
( 
@Sql varchar(max) , 
@Order varchar(4000) , 
@CurrentPage int , 
@PageSize int, 
@TotalPage int OUTPUT, 
@TotalCount int output 
) 
As 
/*Andy 2012-2-28 */ 
Declare @Exec_sql nvarchar(max) 
Set @Exec_sql='Set @TotalCount=(Select Count(1) From ('+@Sql+') As a)' 
Exec sp_executesql @Exec_sql,N'@TotalCount int output',@TotalCount output 
SET @TotalPage=CEILING((@TotalCount+0.0)/@PageSize) 

Set @Order=isnull(' Order by '+nullif(@Order,''),' Order By getdate()') 
if @CurrentPage=1 /*经常会调用第1页，这里做特殊处理，少一层子查询*/ 
Set @Exec_sql=' 
;With CTE_Exec As 
( 
'+@Sql+' 
) 
Select Top(@pagesize) *,row_number() Over('+@Order+') As r From CTE_Exec Order By r 
' 
Else 
Set @Exec_sql=' 
;With CTE_Exec As 
( 
Select *,row_number() Over('+@Order+') As r From ('+@Sql+') As a 
) 
Select * From CTE_Exec Where r Between (@CurrentPage-1)*@pagesize+1 And @CurrentPage*@pagesize Order By r 
' 
Exec sp_executesql @Exec_sql,N'@CurrentPage int,@PageSize int',@CurrentPage,@PageSize

GO
