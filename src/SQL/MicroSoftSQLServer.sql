
-- leeyeonjun.dbo.Table_1 definition

-- Drop table

-- DROP TABLE leeyeonjun.dbo.Table_1;

CREATE TABLE leeyeonjun.dbo.Table_1 (
	test1 varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	test2 int NOT NULL
);


DROP TABLE leeyeonjun.dbo.Table_1;

-- Tabel List
SELECT * 
FROM sys.tables;

SELECT *
FROM leeyeonjun.dbo.Table_1;

INSERT INTO leeyeonjun.dbo.Table_1 (test1, test2)
VALUES('이연준', 39);


CREATE PROCEDURE SelectAllTable_1
AS
	SELECT *
	FROM leeyeonjun.dbo.Table_1
;

DROP PROCEDURE SelectAllTable_1;

EXEC SelectAllTable_1;



CREATE PROCEDURE SelectAll
	@P_TableName NVARCHAR(500)
AS
	SELECT *
	FROM @P_TableName
GO;

EXEC SelectAllCustomers1 @P_TableName = 'leeyeonjun.dbo.Table_1';

