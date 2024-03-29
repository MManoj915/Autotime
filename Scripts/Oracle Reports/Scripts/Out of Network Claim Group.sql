SELECT NVL(IMM.AMEMBERNAME,'REIMBURSEMENT CLAIMS') MEMBERNAME,
NVL(HDR.CARDNO,MP.APOLICYID) CARDNO,
DECODE(A_POLICYNAME,'ALL','ALL',MP.APOLICYID) POLICYID,
DECODE(A_POLICYNAME,'ALL','ALL',(SUBSTR(MP.APOLICYID,INSTR(MP.APOLICYID,'-')+1))) POLICYSERIAL,
DECODE(A_POLICYNAME,'ALL','ALL',SUBSTR(MP.APOLICYID,0,4)) PRODUCT,
DECODE(A_POLICYNAME,'ALL','ALL REIMBURSEMENT CLAIMS',MP.APOLICYNAME) POLICYNAME,
HDR.CPNO CLAIMNO,HDR.ACR_FORM_NO CLAIMFORMNO,
HDR.ENCOUNTER_START_DATE TREATMENTDATE,
NVL((SELECT LISTAGG (IMVD.SHORTDESC, ',') WITHIN GROUP (ORDER BY IMVD.SHORTDESC) FROM IM_CLAIM_PROCESS_DETAIL DTL
LEFT JOIN IM_ACTIVITY_TYPES ACTTYP ON ACTTYP.TYPECODE=DTL.ACTIVITY_CODE
JOIN IM_VERSION_DETALIS IMVD ON IMVD.DETAILCODE = DTL.VERSIONDETAILCODE
WHERE ACTTYP.VALUE = -1 AND DTL.CLAIM_CODE = HDR.CLAIM_CODE GROUP BY DTL.CLAIM_CODE ),'PRINCIPAL') ICDCODES,
NVL(HDR.REQUEST_AMOUNT,0) FEEAMOUNT,NVL(HDR.DEDUCTABLEVALUE,0) DEDUCTABLEVALUE,NVL(HDR.CO_INS_VALUE,0)CO_INS_VALUE,NVL(HDR.DENAILVALUE,0) DENAILVALUE,
NVL(HDR.APPROVED_AMOUNT,0) APPROVED_AMOUNT,         
TO_DATE(SDATE,'DD/MM/RRRR')  FROMDATE,
TO_DATE(EDATE,'DD/MM/RRRR')   TODATE,
CUR.CURRENCYNAME,HDR.OUTOFNETWORKPROVIDERNAME PROVIDERNAME,
ICB.RECEIVEDON ACKDATE,
(SELECT LISTAGG (IMVD.SHORTDESC, ',') WITHIN GROUP (ORDER BY IMVD.SHORTDESC) FROM IM_CLAIM_PROCESS_DETAIL_DTL SDTL
JOIN IM_CLAIM_PROCESS_DETAIL DTL ON DTL.ACTIVITY_DETAIL_CODE = SDTL.ACTIVITY_DETAIL_CODE
JOIN IM_VERSION_DETALIS IMVD ON IMVD.DETAILCODE = DTL.DENIAL_REASON
WHERE DTL.CLAIM_CODE = HDR.CLAIM_CODE GROUP BY DTL.CLAIM_CODE) DENIALREASON
FROM
(SELECT * FROM IM_CLAIM_PROCESS_HEADER WHERE REQUEST_TYPE=3 
AND  LASTMODIFIEDON BETWEEN :SDATE AND :EDATE
) HDR
JOIN IM_MEMBERS_MAT_VW IMM ON IMM.MEMBERCODE = HDR.MEMBER_CODE AND IMM.POLICYCODE = HDR.POLICYCODE AND IMM.CATEGORYCODE = HDR.CATEGORY_CODE
JOIN IM_CORDPRINT ICC ON ICC.MEMBERPOLICYCODE = IMM.MEMBERPOLICYCODE
JOIN IM_CLAIMBATCH ICB ON ICB.CLAIMBATCHCODE = HDR.BATCHCODE
JOIN  IM_CLAIMMEMBERBATCH ICMB ON ICMB.CLAIMBATCHCODE = HDR.BATCHCODE AND ICMB.MEMBERCODE = HDR.MEMBER_CODE
AND ICMB.POLICYCODE = HDR.POLICYCODE AND ICMB.CATEGORYCODE = HDR.CATEGORY_CODE AND UPPER(ICMB.ACR_FORM_NO) = UPPER(HDR.ACR_FORM_NO)
JOIN IM_MEMBERPOLICY MP ON MP.MEMBERPOLICYCODE=HDR.MEMBERPOLICYCODE  AND MP.TYPEE=1
 JOIN IM_POLICY POL ON POL.POLICYCODE = MP.POLICYCODE
 LEFT JOIN  GENCURRENCY CUR ON CUR.CURRENCYCODE=HDR.CURRENCYCODE      
WHERE POL.POLICYCODE = NVL(:POLICYCODE,POL.POLICYCODE)
AND POL.GROUPCODE = NVL(:GROUPCODE,POL.GROUPCODE) AND HDR.CURRENCYCODE = NVL(CURRENCYCODE,HDR.CURRENCYCODE) 
ORDER BY HDR.CARDNO ASC


:POLICYCODE

SELECT POLICYCODE,POLICYID,POLICYNAME FROM IM_POLICY

:GROUPCODE

SELECT GROUP_CODE,GROUP_ID,GROUP_NAME FROM IM_GROUPS

:CURRENCYCODE

SELECT CURRENCYCODE,CURRENCYNAME FROM GENCURRENCY
