-- DROP SCHEMA dbo;

-- CREATE SCHEMA dbo;


/* 
 * 대소문자 구분
 * 0 : 테이블과 데이터베이스의 이름을 대소문자 구분
 * 1 : 테이블과 DB 이름을 소문자로 생성
 * 2 : 테이블과 데이터베이스의 이름을 대소문자를 구분해서 생성
 */

SHOW variables like 'lower%';

SHOW databases;

USE sakila;




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
##		MySQL 각종 연산자와 함수
##
###############################################
###############################################


##		산술 연산자(arithmetic operator)

SELECT 	504.7 + 13,
		504.7 * 0.9,
		504.7 / 2,
		504.7 DIV 2,
		504.7 % 2,
		504.7 MOD 2;

##		비교 연산자(comparison operator)
SELECT 	3 = 3,      		-- 3과 3이 같은지를 비교
		4 = 5,          	-- 1과 2가 같은지를 비교
		0 = NULL,          	-- 0과 NULL이 같은지를 비교
		3 IS TRUE,         	-- 1과 TRUE가 같은지를 비교
		4 IS NULL,         	-- 1과 NULL이 같은지를 비교
		4 BETWEEN 5 AND 7, 	-- 4이 5보다 크거나 같고, 7보다 작거나 같은지를 비교
		5 BETWEEN 5 AND 7, 	-- 5이 5보다 크거나 같고, 7보다 작거나 같은지를 비교
		6 BETWEEN 5 AND 7, 	-- 6이 5보다 크거나 같고, 7보다 작거나 같은지를 비교
		7 BETWEEN 5 AND 7, 	-- 7이 5보다 크거나 같고, 7보다 작거나 같은지를 비교
		8 BETWEEN 5 AND 7, 	-- 8이 5보다 크거나 같고, 7보다 작거나 같은지를 비교
		5 IN (2, 3, 4, 5); 	-- 5가 2, 3, 4, 5중에 포함되는지를 비교
	
##		논리 연산자(logical operator)
SELECT 	NOT 0, 		-- 피연산자가 0이면 1을 반환하고, 1이면 0을 반환함.
		NOT TRUE,
		1 AND 1,    -- 피연산자가 모두 1일때만 1을 반환하고, 나머지 경우에는 0을 반환함.
		1 && TRUE,
		0 OR 0,     -- 피연산자가 모두 0일때만 0을 반환하고, 나머지 경우에는 1을 반환함.
		1 XOR 0;    -- 피연산자가 서로 다르면 1을 반환하고, 서로 같으면 0을 반환함.

##		비트 연산자(bitwise operator)
SELECT 	b'1001' & b'1111', 		-- 연산 결과는 b'1001' = 9
		b'1000' | b'1111',      -- 모든 비트에 하나라도 1이 포함되어 있으므로, 연산 결과는 b'1111'이 됨.
		b'1000' ^ b'1111',      -- 첫 번째 비트를 제외한 모든 비트가 서로 다르므로, 연산 결과는 b'0111'이 됨.
		b'1100' >> 1,           -- 모든 비트를 1비트씩 오른쪽으로 이동시키므로, 연산 결과는 b'0110'이 됨.
		b'10' << 1,
		1 << 1,
		1 << 2,
		1 << 3,
		3 >> 1,
		4 >> 1,
		5 >> 1,
		6 >> 1,
		(1 << 1) << 1,
		b'11' & b'11',
		b'1100' >> 2;           -- 모든 비트를 2비트씩 오른쪽으로 이동시키므로, 연산 결과는 b'0011'이 됨.

##		흐름 제어
SELECT 
	CASE 1
	WHEN 0 THEN 'zero'
    WHEN 1 THEN 'one'
    ELSE 'more' 
    END,
    CASE 3
	WHEN 0 THEN 'zero'
    WHEN 1 THEN 'one'
    ELSE 'more'
	END;

SELECT 	IF(0 < 1, 'yes', 'no'),	-- true이면 'yes', false이면 'no'
		IF(0 = 1, 'yes', 'no');

SELECT 	IFNULL(NULL, '전달받은 값이 null입니다.'),
		IFNULL(3, '전달받은 값이 null입니다.');

SELECT 	NULLIF(3, 3),
		NULLIF(3, 4);
	
##		패턴 매칭(pattern matching)
SELECT *
FROM film
WHERE title LIKE 'love%';

SELECT *
FROM film
WHERE title LIKE '%love';

SELECT *
FROM film
WHERE title NOT LIKE '%love%';

SELECT *
FROM film
WHERE title LIKE 'A__ %';

SELECT *
FROM film
WHERE title REGEXP '^love | love$';

SELECT *
FROM film
WHERE title not REGEXP '^love | love$';

##		타입 변환(type casting)
SELECT 	BINARY 'a' = 'A', 		-- BINARY 연산자는 뒤에 오는 문자열을 바이너리 문자열로 변환
		'a' = 'A';

SELECT 	4 / '2',
		4 / 2,
		4 / CAST('2' AS UNSIGNED);

SELECT CONVERT('abc' USING utf8);


SELECT CAST('[1,2,3]' as JSON);
SELECT CAST('{"opening":"Sicilian","variations":["pelikan","dragon","najdorf"]}' as JSON);

		
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













