SELECT COUNT(*)TOTALCLAIMS,TO_CHAR(HDR.TRANSACTIONDATE,'MONTH') MONTH,EXTRACT(YEAR FROM HDR.TRANSACTIONDATE)YEAR,
PRO.PROVIDERID,PRO.PROVIDERNAME,SUM(NVL(HDR.REQUEST_AMOUNT,0)) CLAIMAMOUNT,PROVIDERMAINPHONE PROVIDERMOBNO
FROM IM_CLAIM_PROCESS_HEADER HDR
JOIN IM_PROVIDERS PRO ON PRO.PROVIDERCODE = HDR.PROVIDER_CODE
WHERE CLAIM_STATUS = 1 AND NVL(ISRESUBMISSION,0) = 0 AND TRANSACTIONDATE BETWEEN :SDATE AND :EDATE
AND HDR.PROVIDER_CODE = Nvl(:CLAIMPROVIDERCODE,HDR.PROVIDER_CODE) 
GROUP BY HDR.TRANSACTIONDATE,PRO.PROVIDERID,PRO.PROVIDERNAME,PROVIDERMAINPHONE 
ORDER BY  EXTRACT(MONTH FROM HDR.TRANSACTIONDATE) ASC;

                                                       

:CLAIMPROVIDERCODE

SELECT PROVIDERCODE,PROVIDERID,PROVIDERNAME FROM IM_PROVIDERS