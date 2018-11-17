
-- SELECT * FROM dbo.Orders
-- SELECT * FROM dbo.OrderLines
-- SELECT * FROM dbo.Orders o LEFT OUTER JOIN dbo.OrderLines ol ON o.Id = ol.OrderId
-- DELETE dbo.Orders
-- DROP TABLE dbo.Orders
-- DROP TABLE dbo.OrderLines
-- CREATE TABLE Orders ( Id INT IDENTITY PRIMARY KEY NOT NULL, DistributorId INT NOT NULL, ContactName NVARCHAR(32) NULL)
-- CREATE TABLE OrderLines ( Id INT IDENTITY PRIMARY KEY NOT NULL, OrderId INT NOT NULL, ProductId INT NOT NULL, Quantity SMALLINT NOT NULL)
