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
--		leeyeonjun.dbo 테이블생성
--
--------------------------------------------
--------------------------------------------

CREATE TABLE [leeyeonjun].[dbo].[TestTable_1] (
	name varchar(50) COLLATE Korean_Wansung_CI_AS NOT NULL,
	gender bit,
	[old] int NOT NULL)
INSERT INTO [leeyeonjun].[dbo].[TestTable_1] (name, gender, old) VALUES('이연준', 0, 39)
INSERT INTO [leeyeonjun].[dbo].[TestTable_1] (name, gender, old) VALUES('김건희', 1, 52)
INSERT INTO [leeyeonjun].[dbo].[TestTable_1] (name, gender, old) VALUES('윤석렬', 0, 59)
SELECT * FROM [leeyeonjun].[dbo].[TestTable_1];

--DROP TABLE [leeyeonjun].[dbo].[TestTable_1];

CREATE TABLE TestTable_2 (IntNumber int NOT NULL)
DECLARE @max INT SET @max = 10
DECLARE @idx INT SET @idx = 1
WHILE (@idx <= @max)
BEGIN
    INSERT INTO TestTable_2 (IntNumber) values (@idx)
    SET @idx = @idx + 1
END
SELECT * FROM TestTable_2;
--DROP TABLE TestTable_2;







--------------------------------------------
--------------------------------------------
--
--		leeyeonjun.dbo 에 Stored Procedure 생성
--
--------------------------------------------
--------------------------------------------





--SP 기본
CREATE PROCEDURE SelectAllTable_Basic
AS
	SELECT *
	FROM [leeyeonjun].[dbo].[TestTable_1]
;
--DROP PROCEDURE SelectAllTable_Basic;

--SP 파라미터 사용하기
CREATE PROCEDURE SelectAllName
	@Column_Name NVARCHAR(50)
AS
	SELECT *
	FROM [leeyeonjun].[dbo].[TestTable_1]
	WHERE name = @Column_Name
;
--DROP PROCEDURE SelectAllName;

--SP 파라미터 이용하여 테이블이름 전달하기
CREATE PROCEDURE SelectAllTable
	@TableName NVARCHAR(500) = null
AS
BEGIN
	DECLARE @SQLString NVARCHAR(500)
	SET @SQLString = CONCAT(N'SELECT * FROM ', @TableName)
	
	EXEC sp_executesql @SQLString
END;
--DROP PROCEDURE SelectAllTable;


EXEC SelectAllTable_Basic;

EXEC SelectAllName '이연준';

EXEC SelectAllTable @TableName = '[TestTable_1]';




--------------------------------------------
--------------------------------------------
--
--		Comment(설명) Table, Column
--
--------------------------------------------
--------------------------------------------


/* 테이블 코멘트 추가 */
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '연습테이블ㄹㄹㄹ', 'USER', dbo, 'TABLE', [TestTable_1];

/* 테이블 코멘트 조회 */
SELECT * FROM ::FN_LISTEXTENDEDPROPERTY (NULL, 'SCHEMA', 'DBO', 'TABLE', DEFAULT, DEFAULT, DEFAULT);

/* 테이블 코멘트 수정 */
EXEC SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '연습테이블1', 'USER', dbo, 'TABLE', [TestTable_1];

/* 테이블 코멘트 삭제 */
EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', [TestTable_1];


/* 컬럼 코멘트 추가 */
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '이름ㅁㅁㅁ', 'USER', dbo, 'TABLE', [TestTable_1], 'COLUMN', name
EXEC SP_ADDEXTENDEDPROPERTY 'MS_Description', '나잉ㅇㅇㅇ', 'USER', dbo, 'TABLE', [TestTable_1], 'COLUMN', [old];

/* 테이블 코멘트 조회 */
SELECT * FROM ::FN_LISTEXTENDEDPROPERTY(NULL, 'SCHEMA', 'DBO', 'TABLE', 'TestTable_1', 'COLUMN', DEFAULT);

/* 컬럼 코멘트 수정 */
EXEC SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '이름', 'USER', dbo, 'TABLE', [TestTable_1], 'COLUMN', name
EXECUTE SP_UPDATEEXTENDEDPROPERTY 'MS_Description', '나이', 'USER', dbo, 'TABLE', [TestTable_1], 'COLUMN', 'old';

/* 컬럼 코멘트 삭제 */
EXEC SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', [TestTable_1], 'COLUMN', name
EXECUTE SP_DROPEXTENDEDPROPERTY 'MS_Description', 'SCHEMA', dbo, 'TABLE', [TestTable_1], 'COLUMN', 'old';





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





--------------------------------------------
--------------------------------------------
--
--		Random : RAND()
--
--------------------------------------------
--------------------------------------------




BEGIN TRAN
	BEGIN
	--변수지정
		DECLARE @min1 INT SET @min1 = 3
		DECLARE @max1 INT SET @max1 = 6 -- 3~6 사이의 임의의 정수 
		DECLARE @min2 INT SET @min2 = 5
		DECLARE @max2 INT SET @max2 = 10 -- 5~10 사이의 임의의 정수
		DECLARE @varString1 INT SET @min1 = 3
		DECLARE @max1 INT SET @max1 = 6 -- 3~6 사이의 임의의 정수 
		DECLARE @idxStart INT SET @idxStart = 1
		DECLARE @idxEnd INT SET @idxEnd = 12
		DECLARE @test1 TABLE(
			RandInt1 INT,
			RandInt2 INT
		);
		
	--반복문
		WHILE (@idxStart <= @idxEnd)
		BEGIN
		    INSERT INTO @test1 (RandInt1, RandInt2) values (
		    	CONVERT(INT, (@max1 - @min1) * RAND() + @min1),
		    	CONVERT(INT, (@max2 - @min2) * RAND() + @min2)
		    	)
		    SET @idxStart = @idxStart + 1
		END;
		
	--화면표시
		SELECT 
			RandInt1 AS '3~6 사이의 임의의 정수',
			RandInt2 AS '5~10 사이의 임의의 정수'
		FROM @test1;
	END;
COMMIT TRAN;



SELECT FLOOR(RAND()*10);

-- min <= rand < max
DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CONVERT(INT, (@max  - @min) * RAND()  + @min) AS '랜덤정수';

DECLARE @max INT SET @max = 3
DECLARE @min INT SET @min = 1
SELECT CAST((@max  - @min) * RAND()  + @min AS INT) AS '랜덤정수';

-- Create Stored Procedure to Get Random Int
CREATE PROCEDURE spGetRandIntMinMax
	@return INT OUTPUT,
	@min INT = 0,
	@max INT = 100
AS
	SELECT @return = CONVERT(INT, (@max  - @min) * RAND()  + @min)
RETURN @return;
--DROP PROCEDURE spGetRandIntMinMax;

DECLARE @answer INT
EXEC spGetRandIntMinMax @answer OUTPUT, 2, 5
SELECT @answer AS '랜덤정수';


-- 테이블에 랜덤숫자 입력
DECLARE @min INT SET @min = 3
DECLARE @max INT SET @max = 5
DECLARE @idx INT SET @idx = 1
WHILE (@idx <= 10)
	BEGIN
	    INSERT INTO [00_TestTable_2] (IntNumber) values (CONVERT(INT, (@max  - @min) * RAND()  + @min))
	    SET @idx = @idx + 1
	END
SELECT * FROM [00_TestTable_2];







--------------------------------------------
--------------------------------------------
--
--		Date : GETDATE()
--
--------------------------------------------
--------------------------------------------

--현재 날짜 출력--
SELECT GETDATE() AS 시스템일자;

--현재 날짜의 연,월,일 출력--
SELECT 
	CONCAT(YEAR(GETDATE()), '-',MONTH(GETDATE()), '-', DAY(GETDATE())) AS NowISO, 
	YEAR(GETDATE()) AS 년,
	MONTH(GETDATE()) AS 월,
	DAY(GETDATE()) AS 일;

--YYYYMMDD--
SELECT CONVERT(varchar(10), Getdate(), 112);

--HH:MM:SS--
SELECT CONVERT(varchar(8), Getdate(), 108);

--HH:MM:SS:mmm--
Select Convert(varchar(12),Getdate(),114)

--HHMMSS--
Select Replace(Convert(varchar(8),Getdate(),108),':','')

--HHMMSSmmm--
Select Replace(Convert(varchar(12),Getdate(),114),':','')

--YYYY/MM/DD HH:MM:SS--
Select Replace(Convert(varchar(30),Getdate(),120),'-','/')

--YYYY/MM/DD HH:MM:SS--
Select Replace(Convert(varchar(30),Getdate(),121),'-','/')

--YYYY/MM/DD HH:MM:SS--
Select Convert(varchar(10),Getdate(),111) + Space(1) + Convert(varchar(8),Getdate(),108)

--YYYYMMDDHHMMSS--
Select Convert(varchar(10),Getdate(),112) + Replace(Convert(varchar(8),Getdate(),108),':','')





CREATE PROC spToday
	@Today varchar(4) OUTPUT
	AS
		SELECT @Today=CONVERT(varchar(2), DATEPART(dd, GETDATE()))
RETURN @Today;

DECLARE @answer varchar(4)
EXEC spToday @answer OUTPUT
SELECT @answer AS 오늘날짜;





-- 반복문에 사용될 변수 선언
DECLARE @max INT SET @max = 10
DECLARE @idx INT SET @idx = 1
WHILE (@idx < @max)
BEGIN
    INSERT INTO leeyeonjun.dbo.Table_int (IntNumber) values (@idx)
    SET @idx = @idx + 1
END
SELECT * FROM leeyeonjun.dbo.Table_int;

















--------------------------------------------
--------------------------------------------
--
--		MS SQL Server : Cursor Practice
--
--------------------------------------------
--------------------------------------------













