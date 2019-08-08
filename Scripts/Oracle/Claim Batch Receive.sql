SELECT CB.*,PRO.PROVIDERID,PRO.PROVIDERNAME,GEN.CONSTANTNAME STATUSNAME,GEN1.CONSTANTNAME AS AUTHORIZEDSTATUSNAME,
ADMU.USERNAME FROM IM_CLAIMBATCH CB LEFT JOIN IM_PROVIDERS PRO ON PRO.PROVIDERCODE=CB.PROVIDERCODE 
LEFT JOIN GENCONSTANT GEN ON GEN.CONSTANTVALUE=CB.STATUSCODE AND GEN.CATEGORY='CALIMBATCHSTATUS' 
AND GEN.LANGUAGECODE='en-US' 
JOIN (SELECT CONSTANTVALUE,CONSTANTNAME FROM GENCONSTANT WHERE CATEGORY='AUTHORIZEDSTATUS' AND LANGUAGECODE='en-US')GEN1 
ON GEN1.CONSTANTVALUE=CB.AUTHORIZEDSTATUS 
LEFT JOIN ADMUSER ADMU ON ADMU.USERCODE=CB.LASTMODIFIEDBY WHERE CB.PROVIDERCODE IS NOT NULL ORDER BY CB.SERIALNO DESC