--Created by Vladimir
--Created 03/12/2020
--Description: Crea la tabla stockproduct

IF OBJECT_ID(N'StockProduct') IS NULL
BEGIN
CREATE TABLE [StockProduct](	
	[PointOfSale] [nvarchar](200) NULL,
	[Product] [nvarchar](200) NULL,
	[Date] [datetime] NULL,
	[Stock] [int] NULL)
END
