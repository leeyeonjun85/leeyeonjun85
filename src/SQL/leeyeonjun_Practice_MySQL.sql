-- DROP SCHEMA dbo;

-- CREATE SCHEMA dbo;


/* 
 * 대소문자 구분
 * 0 : 테이블과 데이터베이스의 이름을 대소문자 구분
 * 1 : 테이블과 DB 이름을 소문자로 생성
 * 2 : 테이블과 데이터베이스의 이름을 대소문자를 구분해서 생성
 */

SHOW variables like 'lower%';






###############################################
###############################################
##
##		MySQL 기초문법
##
###############################################
###############################################


SHOW databases;

USE sakila;

SHOW TABLES;

DESCRIBE actor;
DESC actor;	-- 짧게 줄여써도 된다

SELECT * FROM actor;
SELECT * FROM sakila.actor;	-- 동일한 결과 조회

SELECT * FROM actor
LIMIT 3;

SELECT * FROM actor
ORDER BY first_name ASC;	-- 오름차순 정렬

SELECT * FROM actor
ORDER BY first_name DESC;	-- 내림차순 정렬

SELECT 	actor_id,
		last_name AS 성,
		first_name 이름,	-- AS 생략가능
		last_update
FROM actor;

SELECT actor_id actorId
		, first_name fName
		, last_name lName
		, last_update
FROM actor
WHERE first_name = 'zero';

SELECT actor_id actorId
		, first_name fName
		, last_name lName
		, last_update
FROM actor
WHERE first_name = 'ZERO';

SELECT actor_id actorId
		, first_name fName
		, last_name lName
		, last_update
FROM actor
WHERE actor_id >= 25 && actor_id < 30;

SELECT actor_id actorId
		, first_name fName
		, last_name lName
		, last_update
FROM actor
WHERE 1=1
AND last_name = 'KILMER'
AND actor_id < 100;






###############################################
###############################################
##
##		MySQL 문자열 다루기
##
###############################################
###############################################

SELECT concat('123', '456', '789');	-- 123456789
SELECT concat_ws('/', 'AB', 'CD', 'EF', 'GH');	-- AB-CD-EF-GH
SELECT concat_ws('-', '010', '1234', '5678');	-- 010-1234-5678
SELECT concat_ws('-', '2022','12','25');	-- 2022/12/25
SELECT format(123.456789, 2);	-- 123.46
SELECT format(123.45, 7);	-- 123.4500000
SELECT insert('korea _word_ fighting', 7, 6, 'FOOTBALL');	-- korea FOOTBALL fighting
SELECT left('korea FOOTBALL fighting', 5);	-- korea
SELECT right('korea FOOTBALL fighting', 8);	-- fighting
SELECT upper('football');	-- FOOTBALL
SELECT lower('FOOTBALL');	-- football
SELECT lpad('i love you', 14, '♡');	-- ♡♡♡♡i love you
SELECT rpad('i love you', 14, '♡♥');	-- i love you♥♥♥♥
SELECT lpad('please don\'t cut me', 6, '!!!');	-- please
SELECT ltrim('       i love you       ');		-- i love you       (좌측만 공백제거)
SELECT rtrim('       i love you       ');		--        i love you(우측만 공백제거)
SELECT ltrim(rtrim('       i love you       '));	-- i love you
SELECT trim('       i love you       ');		-- i love you
SELECT repeat('i love you! ', 3);	-- i love you! i love you! i love you! 
SELECT concat('i love ', repeat('~', 7), ' you');	-- i love ~~~~~~~ you
SELECT length('i love you');	-- 10
SELECT length(concat('i love ', repeat('~', 7), ' you'));	-- 18
SELECT replace('i love her', 'her', 'you');	-- i love you
SELECT reverse('기러기 토마토 스위스 인도인 별똥별 역삼역');	-- 역삼역 별똥별 인도인 스위스 토마토 기러기
SELECT substring('i love you', 3, 4);	-- love
SELECT substring_index('aaa 123 aaa...ENDaaa 456', 'aaa', 3);	-- aaa 123 aaa...END







###############################################
###############################################
##
##		MySQL if() 다루기
##
###############################################
###############################################


SELECT 1 FROM DUAL; -- 1
SELECT 0 FROM DUAL; -- 0
SELECT true FROM DUAL; -- 1
SELECT false FROM DUAL; -- 0
SELECT 12345 FROM DUAL; -- 12345

SELECT if(12 > 15, 'GOOD', 'BAD');	-- BAD
SELECT if(12 % 2 = 0, 'GOOD', 'BAD');	-- GOOD
SELECT if(1 = 0, 'NO1', if(1 != 1, 'NO2', 'YES'));	-- YES

SELECT ifnull(NULL, '12345');	-- 12345 => 뒤에 있는 값 반환
SELECT ifnull('ABC', 'SHOW');	-- ABC => 앞에 있는 값 반환
SELECT ifnull(1 = 1, '222');	-- 1 => 수식1이 true, 즉 1이므로 null이 아님
SELECT ifnull(1 = 0, '222');	-- 0 => 수식1이 false, 즉 0이므로 null이 아님

SELECT nullif(100, 101);	-- 100
SELECT nullif(NULL, '123');	-- NULL
SELECT nullif(1+1, 2);	-- NULL
SELECT nullif(1, '1');	-- NULL
SELECT nullif('TRUE', TRUE);	-- TRUE
SELECT nullif(FALSE, TRUE);	-- 0
SELECT nullif(TRUE, FALSE);	-- 1
SELECT nullif(TRUE, !FALSE);	-- NULL
SELECT nullif(FALSE, !TRUE);	-- NULL









###############################################
###############################################
##
##		test_table_1 테이블생성
##
###############################################
###############################################



CREATE TABLE test_table_1
(
	name varchar(50) NOT NULL,
	gender bit,
	old int NOT null
);
INSERT INTO test_table_1 (name, gender, old) VALUES('이연준', 0, 39);
INSERT INTO test_table_1 (name, gender, old) VALUES('김건희', 1, 52);
INSERT INTO test_table_1 (name, gender, old) VALUES('윤석렬', 0, 59);
SELECT * FROM test_table_1;

# DROP TABLE test_table_1;



CREATE TABLE `Test_Table_2` (
	name varchar(50) NOT NULL,
	gender bit,
	old int NOT null
);

DROP TABLE Test_Table_2;


###############################################
###############################################
##
##		test_table_1 에 Stored Procedure 생성
##
###############################################
###############################################


# SP 기본

CREATE PROCEDURE 'SelectAll_Basic'()
BEGIN
	SELECT * 
	FROM test_table_1;
END
;


CREATE PROCEDURE SelectAll_Basic
AS
	SELECT *
	FROM test_table_1
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












