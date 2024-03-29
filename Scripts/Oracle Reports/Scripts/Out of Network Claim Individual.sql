SELECT NVL(IMP.AMEMBERNAME ,'REIMBURSEMENT CLAIMS') MEMBERNAME,
NVL(HDR.CARDNO,IPOL.POLICYID) CARDNO,  
DECODE(AINDPOLICYNAME,'ALL','ALL',IPOL.POLICYID) POLICYID,
DECODE(AINDPOLICYNAME,'ALL','ALL',(SUBSTR(IPOL.POLICYID,INSTR(IPOL.POLICYID,'-')+1))) POLICYSERIAL,
DECODE(AINDPOLICYNAME,'ALL','ALL',SUBSTR(IPOL.POLICYID,0,4)) PRODUCT,
DECODE(AINDPOLICYNAME,'ALL','ALL REIMBURSEMENT CLAIMS',IPOL.POLICYNAME) POLICYNAME,   
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
CUR.CURRENCYNAME
FROM
(SELECT * FROM IM_CLAIM_PROCESS_HEADER WHERE REQUEST_TYPE=3 AND   AUTHORIZEDSTATUS=1  
AND LASTMODIFIEDON    BETWEEN :SDATE AND :EDATE
) HDR
--JOIN IM_MEMBERS IMP ON IMP.MEMBER_CODE = HDR.MEMBER_CODE
JOIN IM_MEMBERPOLICY IMP ON IMP.MEMBERPOLICYCODE=HDR.MEMBERPOLICYCODE  AND IMP.TYPEE=2
JOIN IM_INDIVIDUALPOLICY IPOL ON IPOL.INDIVIDUALPOLICYCODE = IMP.POLICYCODE -- AND HDR.POLICYTYPECODE=2
LEFT JOIN  GENCURRENCY CUR ON CUR.CURRENCYCODE=HDR.CURRENCYCODE
WHERE IPOL.INDIVIDUALPOLICYCODE = NVL(:INDIVIDUALPOLICYCODE,IPOL.INDIVIDUALPOLICYCODE) 
AND  HDR.CURRENCYCODE = NVL(CURRENCYCODE,HDR.CURRENCYCODE) 
ORDER BY HDR.CARDNO ASC


:POLICYCODE

SELECT IM_INDIVIDUALPOLICY,POLICYID,POLICYNAME FROM IM_INDIVIDUALPOLICY 

:CURRENCYCODE

SELECT CURRENCYCODE,CURRENCYNAME FROM GENCURRENCY