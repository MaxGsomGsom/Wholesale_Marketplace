INSERT INTO [dbo].[Picture] ([ItemID], [Image])
SELECT 7, BulkColumn 
FROM Openrowset( Bulk 'C:\Users\Max\Desktop\1.jpg', Single_Blob) as image
