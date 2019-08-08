SELECT
SUBSTR(POL.POLICYID,0,4)  PRODUCTCODE,
POL.POLICYNAME PRODUCTNAME,
HDR.TRANSACTIONDATE PROCESSINGSTARTDATE ,
POS.REFDATE PROCESSINGENDDATE ,
NVL(SUM(HDR.APPROVED_AMOUNT),0) AMOUNTPAID
FROM IM_ENDORSEMENTPOSTING POS
JOIN IM_ENDORESMENTPOSTINGSUBDTL DTL ON DTL.ENDORESMENTCODE=POS.ENDORESMENTCODE AND DTL.GROUPCODE IS NOT NULL AND  
TO_DATE(POS.REFDATE,'DD/MM/RRRR') BETWEEN :SDATE AND :EDATE
JOIN IM_ENDPOSTCLAIMDTL CLMDET ON POS.ENDORESMENTCODE=CLMDET.ENDORESMENTCODE
JOIN IM_CLAIM_PROCESS_HEADER HDR ON CLMDET.CLAIMCODE=HDR.CLAIM_CODE
LEFT JOIN IM_MEMBERPOLICY IMPOL ON IMPOL.POLICYCODE = HDR.POLICYCODE AND IMPOL.MEMBERCODE = HDR.MEMBER_CODE  AND IMPOL.CATEGORYCODE = HDR.CATEGORY_CODE
JOIN IM_MEMBERS IMEM ON IMEM.MEMBER_CODE = HDR.MEMBER_CODE
JOIN IM_POLICY POL ON POL.POLICYCODE=NVL(NVL(IMPOL.POLICYCODE,HDR.POLICYCODE),IMEM.POLICYCODE) AND NVL(IMPOL.TYPEE,IMEM.POLICYTYPE) = 1
JOIN IM_GROUPS GRP ON GRP.GROUP_CODE=POL.GROUPCODE WHERE
POL.POLICYCODE = NVL(:POLICYCODE,POL.POLICYCODE)
AND GRP.GROUP_CODE = Nvl(:GROUP_CODE,GRP.GROUP_CODE)GROUP BY
SUBSTR(POL.POLICYID,0,4)  ,
POL.POLICYNAME ,
HDR.TRANSACTIONDATE ,
POS.REFDATE ,
HDR.CLAIM_STATUS
HAVING SUM(HDR.APPROVED_AMOUNT)<>0 AND SUM(HDR.APPROVED_AMOUNT) >=:SNO AND SUM(HDR.APPROVED_AMOUNT)<=DECODE(:ENO,0,SUM(HDR.APPROVED_AMOUNT),:ENO)
;

POLICYCODE

SELECT POLICYCODE,POLICYID,POLICYNAME FROM IM_POLICY

:GROUP_CODE

SELECT GROUP_CODE,GROUP_ID,GROUP_NAME FROM IM_GROUPS