--Created by Vladimir
--Created 03/12/2020
--Description: Guarda la data de forma masiva

declare @sql nvarchar(MAX)
declare @format varchar(10)
--declare @path varchar(250)
--set @path = 'D:\FileExcel\Stock.csv'
set @format='char'

set @sql= N'BULK INSERT [StockProduct] 
FROM '''+@path+''' WITH (DATAFILETYPE='''+@format+''',FIRSTROW = 2,
    FIELDTERMINATOR = '+''';'''+',ROWTERMINATOR='+'''\n'''+');'
Execute sp_executesql @sql

