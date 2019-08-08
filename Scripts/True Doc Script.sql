PROMPT CREATE TABLE IM_TREUDOC_ICD 
CREATE TABLE IM_TRUEDOC_ICD (
TRUEDOCCODE  NUMBER(19,0) NOT NULL,
ACTIVITYTYPE NUMBER(1,0)  NOT NULL,
DETAILCODE   NUMBER(19,0) NOT NULL,
DOCTYPE      VARCHAR2(10)  NOT NULL,
CREATEDON    DATE         NULL,
CREATEDBY    NUMBER       NULL
)
STORAGE (
INITIAL    2048 K
NEXT       1024 K
)
/

PROMPT ALTER TABLE IM_TREUDOC_ICD ADD CONSTRAINT IM_TREUDOC_ICD_PK PRIMARY KEY
ALTER TABLE IM_TRUEDOC_ICD
ADD CONSTRAINT IM_TRUEDOC_ICD_PK PRIMARY KEY (
TRUEDOCCODE
)
USING INDEX
TABLESPACE SBS_INDX
STORAGE (
NEXT       1024 K
)
/

CREATE VIEW IM_TRUEDOC_ICD_VW
AS 
SELECT H.TRUEDOCCODE,Decode(H.ACTIVITYTYPE,1,'ICD',2,'CPT') ACTIVITYTYPE,IVD.CODE,IVD.SHORTDESC,FND.VALUE FROM IM_TRUEDOC_ICD H
JOIN IM_VERSION_DETALIS IVD ON IVD.DETAILCODE = H.DETAILCODE
JOIN FND_LOOKUP_VALUES FND ON FND.LOOKUP_TYPE = 'IM_TRUEDOC_TYPE' AND FND.LOOKUP_CODE = H.DOCTYPE




INSERT INTO FND_LOOKUP_VALUES
SELECT 'IM_TRUEDOC_TYPE','1','Cancer',NULL,NULL,NULL,0 FROM DUAL;

INSERT INTO FND_LOOKUP_VALUES
SELECT 'IM_TRUEDOC_TYPE','2','Hepatitis C',NULL,NULL,NULL,0 FROM DUAL;

INSERT INTO FND_LOOKUP_TYPES
SELECT 'IM_TRUEDOC_TYPE','IM_TRUEDOC_TYPE',NULL FROM DUAL;


PROMPT CREATE TABLE IM_HEP_CAN_MASTER
CREATE TABLE IM_HEP_CAN_MASTER (
  HEPCANCODE  NUMBER(19,0) NOT NULL,
  BENEFITCODE NUMBER(19,0)  NOT NULL,
  PREMIUMVALUE   NUMBER  NOT NULL,     
  STARTDATE   DATE,
  ENDDATE     DATE,
  CREATEDON    DATE         NULL,
  CREATEDBY    NUMBER       NULL
)
  STORAGE (
    INITIAL    2048 K
    NEXT       1024 K
  )
/

PROMPT ALTER TABLE IM_HEP_CAN_MASTER ADD CONSTRAINT IM_HEP_CAN_MASTER_PK PRIMARY KEY
ALTER TABLE IM_HEP_CAN_MASTER
  ADD CONSTRAINT IM_HEP_CAN_MASTER_PK PRIMARY KEY (
    HEPCANCODE
  )
  USING INDEX
    TABLESPACE SBS_INDX
    STORAGE (
      NEXT       1024 K
    )
/

CREATE VIEW IM_HEP_CAN_VW
AS SELECT HEPCANCODE,BENEFIT_ID,BENEFIT_NAME,H.PREMIUMVALUE,H.STARTDATE,H.ENDDATE FROM IM_HEP_CAN_MASTER  H
JOIN IM_BENEFIT_CODES BEN ON BEN.BENEFIT_CODE = H.BENEFITCODE;