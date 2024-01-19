-- DROP SCHEMA dbo;

-- CREATE SCHEMA dbo;



-- Tabel List
SELECT * FROM sys.tables;

-- Total objects
SELECT * FROM sys.objects;

-- Procedures
SELECT [type] , name , type_desc , create_date , modify_date
FROM sys.objects
WHERE [type] IN  ('P', 'FN') 
ORDER BY [type] , name 
;













--------------------------------------------
--------------------------------------------
--
--		leeyeonjun.dbo 에 Stored Procedure 생성
--
--------------------------------------------
--------------------------------------------





--SP 기본
CREATE PROCEDURE SelectAllTable_1
AS
	SELECT *
	FROM leeyeonjun.dbo.Table_1
;

--SP 파라미터 사용하기
CREATE PROCEDURE SelectAllName
	@Column_Name NVARCHAR(50)
AS
	SELECT *
	FROM leeyeonjun.dbo.Table_1
	WHERE name = @Column_Name
;

--SP 파라미터 이용하여 테이블이름 전달하기
CREATE PROCEDURE SelectAllTable
	@TableName NVARCHAR(500) = null
AS
BEGIN
	DECLARE @SQLString NVARCHAR(500)
	SET @SQLString = CONCAT('SELECT * FROM ', @TableName)
	
	EXEC sp_executesql @SQLString
END;






DROP PROCEDURE SelectAllTable_1;

EXEC SelectAllTable_1;

DROP PROCEDURE SelectAllName;

EXEC SelectAllName @Column_Name = '이연준';

DROP PROCEDURE SelectAllTable;

EXEC SelectAllTable @TableName = 'Table_1';
EXECUTE SelectAllTable 'Table_int';



--------------------------------------------
--------------------------------------------
--
--		leeyeonjun.dbo 테이블생성
--
--------------------------------------------
--------------------------------------------


DECLARE @TableName NVARCHAR(100)
SET @TableName = '[00_TestTable_1]'
CREATE TABLE @TableName (
	name varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	[old] int NOT NULL)
INSERT INTO @TableName (name, old) VALUES('이연준', 39)
INSERT INTO @TableName (name, old) VALUES('윤석렬', 59)
SELECT * FROM @TableName;


DROP TABLE @TableName;

SELECT * FROM @TableName;



CREATE TABLE [00_TestTable_1] (
	name varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	[old] int NOT NULL);




CREATE TABLE leeyeonjun.dbo.Table_int (IntNumber int NOT NULL)
DECLARE @max INT SET @max = 10
DECLARE @idx INT SET @idx = 1
WHILE (@idx <= @max)
BEGIN
    INSERT INTO leeyeonjun.dbo.Table_int (IntNumber) values (@idx)
    SET @idx = @idx + 1
END
SELECT * FROM leeyeonjun.dbo.Table_int;
DROP TABLE leeyeonjun.dbo.Table_int;




CREATE TABLE Table_2 (
	name varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	gender bit,
	old int NOT NULL
)
INSERT INTO Table_2 (name, gender, old) VALUES('이연준', 0, 39)
INSERT INTO Table_2 (name, gender, old) VALUES('김건희', 1, 52)
INSERT INTO Table_2 (name, gender, old) VALUES('윤석렬', 0, 59);

DROP TABLE Table_2;

SELECT * FROM [leeyeonjun].[dbo].[Table_2];






--------------------------------------------
--------------------------------------------
--
--		MS SQL Server : Cursor Practice
--
--------------------------------------------
--------------------------------------------





--------------------------------------------
--------------------------------------------
--
--		Comment(설명) Table, Column
--
--------------------------------------------
--------------------------------------------



/* 테이블 코멘트 추가 */
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '이름ㅁㅁㅁ', 'USER', dbo, 'TABLE', Table_1;

/* 테이블 코멘트 수정 */
EXEC SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '이름', 'USER', dbo, 'TABLE', Table_1;

/* 테이블 코멘트 삭제 */
EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', Table_1;


/* 컬럼 코멘트 추가 */
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '이름ㅁㅁㅁ', 'USER', dbo, 'TABLE', Table_1, 'COLUMN', name;
EXECUTE SP_ADDEXTENDEDPROPERTY 'MS_Description', '나잉ㅇㅇㅇ', 'USER', dbo, 'TABLE', Table_1, 'COLUMN', old;

/* 컬럼 코멘트 수정 */
EXEC SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '이름', 'USER', dbo, 'TABLE', Table_1, 'COLUMN', name;
EXECUTE SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '나이', 'USER', dbo, 'TABLE', Table_1, 'COLUMN', old;

/* 컬럼 코멘트 삭제 */
EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', Table_1, 'COLUMN', name;
EXECUTE SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', Table_1, 'COLUMN', old;






/* 테이블 코멘트 추가 */
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '이름ㅁㅁㅁ', 'USER', dbo, 'TABLE', Table_1
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '컬럼3개ㅐ', 'USER', dbo, 'TABLE', Table_2;

/* 테이블 코멘트 수정 */
EXEC SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '컬럼3개', 'USER', dbo, 'TABLE', Table_2;

/* 테이블 코멘트 삭제 */
EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', Table_2;



--테이블 코멘트 조회
SELECT * FROM ::FN_LISTEXTENDEDPROPERTY (NULL, 'SCHEMA', 'DBO', 'TABLE', DEFAULT, DEFAULT, DEFAULT);

--테이블 조회하여 테이블 Comment 삭제
BEGIN TRAN
	--임시테이블 생성
	DECLARE @TempTable TABLE(
			 SEQ int IDENTITY
			 ,objname VARCHAR(100)
			)
	INSERT @TempTable (objname)
	SELECT objname FROM ::FN_LISTEXTENDEDPROPERTY (NULL, 'SCHEMA', 'DBO', 'TABLE', DEFAULT, DEFAULT, DEFAULT)
	--반복문에 필요한 변수생성
	DECLARE @i INT, @j INT
		SELECT @i = 1, @j = @@rowcount
	WHILE @i <= @j
	BEGIN
		DECLARE @objname VARCHAR(100)
			SELECT @objname		= objname
			FROM @TempTable
			WHERE seq = @i
		--반복작업
		EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', @objname;
		SET @i = @i + 1
	END
COMMIT TRAN;



--컬럼 코멘트 조회
SELECT * FROM ::FN_LISTEXTENDEDPROPERTY(NULL, 'SCHEMA', 'DBO', 'TABLE', 'Table_1', 'COLUMN', DEFAULT);






--------------------------------------------
--------------------------------------------
--
--		Random : RAND()
--
--------------------------------------------
--------------------------------------------


-- 랜덤
SELECT
	FLOOR(RAND()*6+1) AS '1부터 6까지 랜덤 정수값',
	FLOOR(RAND()*(100)+1) AS '1부터 100까지 랜덤 정수값';


SELECT FLOOR(RAND()*10);


DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CAST(((@max + 1) - @min) * RAND() + @min AS INT) AS '랜덤정수';


DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CONVERT(INT, ((@max + 1) - @min) * RAND()  + @min) AS '랜덤정수';




-- 반복문에 사용될 변수 선언
DECLARE @max INT SET @max = 10
DECLARE @idx INT SET @idx = 1
WHILE (@idx < @max)
BEGIN
    INSERT INTO leeyeonjun.dbo.Table_int (IntNumber) values (@idx)
    SET @idx = @idx + 1
END
SELECT * FROM leeyeonjun.dbo.Table_int;































