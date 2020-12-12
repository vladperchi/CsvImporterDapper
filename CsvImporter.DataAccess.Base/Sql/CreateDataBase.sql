declare @sql nvarchar(MAX)
set @sql= N'IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '''+@dataBaseName+''')
  BEGIN
    CREATE DATABASE ['+@dataBaseName+'];
  END'
Execute sp_executesql @sql

