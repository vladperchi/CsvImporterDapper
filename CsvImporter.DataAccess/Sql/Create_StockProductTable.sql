--Creator: Vladimir
--Description: This sql create the stockproduct table
IF OBJECT_ID(N'StockProduct') IS NULL
BEGIN
CREATE TABLE [StockProduct](	
	[PointOfSale] [nvarchar](200) NULL,
	[Product] [nvarchar](200) NULL,
	[Date] [datetime] NULL,
	[Stock] [int] NULL)
END
