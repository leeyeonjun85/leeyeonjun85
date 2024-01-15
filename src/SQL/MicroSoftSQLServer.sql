
-- leeyeonjun.dbo.Table_1 definition

-- Drop table

-- DROP TABLE leeyeonjun.dbo.Table_1;

CREATE TABLE leeyeonjun.dbo.Table_1 (
	name varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	old int NOT NULL
);


DROP TABLE leeyeonjun.dbo.Table_1;

-- Tabel List
SELECT * 
FROM sys.tables;

SELECT *
FROM leeyeonjun.dbo.Table_1;

INSERT INTO leeyeonjun.dbo.Table_1 (name, old) VALUES('이연준', 39);
INSERT INTO leeyeonjun.dbo.Table_1 (name, old) VALUES('윤석렬', 59);


CREATE PROCEDURE SelectAllTable_1
AS
	SELECT *
	FROM leeyeonjun.dbo.Table_1
;

DROP PROCEDURE SelectAllTable_1;

EXEC SelectAllTable_1;



CREATE PROCEDURE SelectAllName
	@Column_Name NVARCHAR(50)
AS
	SELECT *
	FROM leeyeonjun.dbo.Table_1
	WHERE name = @Column_Name
;
DROP PROCEDURE SelectAllName;

EXEC SelectAllName @Column_Name = '이연준';



SELECT
	FLOOR(RAND()*(6)+1) AS '1부터 6까지 랜덤 정수값',
	FLOOR(RAND()*(100)+1) AS '1부터 100까지 랜덤 정수값';


SELECT FLOOR(RAND()*10);


DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CAST(((@max + 1) - @min) * RAND() + @min AS INT) AS '랜덤정수';


DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CONVERT(INT, ((@max + 1) - @min) * RAND()  + @min) AS '랜덤정수';


CREATE TABLE leeyeonjun.dbo.Table_int (IntNumber int NOT NULL)
SELECT * FROM leeyeonjun.dbo.Table_int;
DROP TABLE leeyeonjun.dbo.Table_int;

-- 반복문에 사용될 변수 선언
DECLARE @max INT SET @max = 10
DECLARE @idx INT SET @idx = 1
WHILE (@idx < @max)
BEGIN
    INSERT INTO leeyeonjun.dbo.Table_int (IntNumber) values (@idx)
    SET @idx = @idx + 1
END
SELECT * FROM leeyeonjun.dbo.Table_int;

