SELECT H.*,Round(TOTALAMOUNT/NOOFVISIT,2) AVGAMOUNT FROM
(SELECT Decode(HDR.TREATMENT_TYPE,1,'Outpatient','Inpatient') TREATMENTTYPE,
PRO.PROVIDERID,PRO.PROVIDERNAME,Count(*) NOOFVISIT,Sum(REQUEST_AMOUNT) TOTALAMOUNT
FROM IM_CLAIM_PROCESS_HEADER HDR
LEFT JOIN IM_PROVIDERS PRO ON PRO.PROVIDERCODE = HDR.PROVIDER_CODE
WHERE HDR.REQUEST_TYPE IN (1,2) AND HDR.TRANSACTIONDATE BETWEEN 
To_Date('01/07/2016','DD/MM/RRRR') AND To_Date('30/07/2016','DD/MM/RRRR')
GROUP BY PRO.PROVIDERID,PRO.PROVIDERNAME,HDR.TREATMENT_TYPE)H ORDER BY TREATMENTTYPE