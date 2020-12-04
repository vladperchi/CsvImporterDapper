--Created by Vladimir
--Created 04/12/2020
--Description: Crea la Base de Datos en caso de no existir

DECLARE @sql nvarchar(MAX)
set @sql= N'IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '''+@dataBaseName+''')
  BEGIN
    CREATE DATABASE ['+@dataBaseName+'];
  END'
Execute sp_executesql @sql

