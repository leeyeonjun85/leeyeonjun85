
--오라클 내 모든 테이블 보여주기

--all_tables는 권한을 가진 모든 테이블이 조회되며 통계 정보를 쉽게 확인할 수 있다.
SELECT *
FROM all_tables;

SELECT *
FROM all_tables
WHERE OWNER = 'TESTUSER';

--all_tab_comments는 권한을 가진 모든 테이블 목록과 테이블 설명(COMMENT)을 조회할 때 사용한다.
SELECT *
FROM USER_TAB_COLUMNS;

--all_objects는 권한을 가진 모든 테이블의 최초 생성 일시와 마지막 변경 일시를 확인할 수 있다.
SELECT *
FROM all_objects
WHERE object_type = 'TABLE';




--테이블 삭제
DROP TABLE "LeeyeonjunTestTable1";

--테이블 생성
CREATE TABLE "LeeyeonjunTestTable1"
(
	Id NUMBER(8),
	Name VARCHAR2(16),
	Old NUMBER(7,2),
	CONSTRAINT LeeyeonjunTestTable1_pk PRIMARY KEY (Id)
);


INSERT INTO "LeeyeonjunTestTable1" VALUES (5, '이연준', 12 );

SELECT *
FROM "LeeyeonjunTestTable1";


DROP TABLE SAMPLETABLE;

CREATE TABLE SAMPLETABLE (
	ID INT auto_increment NOT NULL,
	VALUE INT NOT NULL,
	CONSTRAINT SAMPLETABLE_pk PRIMARY KEY (ID)
);

INSERT INTO SAMPLETABLE( VALUE ) VALUES ( 12 );
INSERT INTO SAMPLETABLE( VALUE ) VALUES ( 16 );

SELECT *
FROM SAMPLETABLE;




SELECT * FROM STUDENT
ORDER BY "id"  ;

INSERT INTO STUDENT ( name )
	VALUES ( "문재인" );

DELETE FROM STUDENT
WHERE name = "문재인";

