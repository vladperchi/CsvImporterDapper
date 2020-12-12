--Created by Vladimir
--Created 12/12/2020

declare @sql nvarchar(MAX)
declare @format varchar(10)
--declare @path varchar(250)
--set @path = 'D:\FileExcel\Stock.csv'
set @format='char'

set @sql= N'BULK INSERT [StockProduct] 
FROM '''+@path+''' WITH (DATAFILETYPE='''+@format+''',FIRSTROW = 2,
    FIELDTERMINATOR = '+''';'''+',ROWTERMINATOR='+'''\n'''+');'
Execute sp_executesql @sql

