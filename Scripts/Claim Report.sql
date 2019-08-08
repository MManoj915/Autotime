SELECT JOBID,JOBDATE,GEN1.CONSTANTNAME REPORTNAME,F.US_NAME USERNAME,H.FILENAME,GEN2.CONSTANTNAME STATUS FROM  
IM_CLAIMS_JOB H
LEFT JOIN FND_USER F ON F.US_ID = H.USERCODE
LEFT JOIN GENCONSTANT GEN1 ON GEN1.CATEGORY = 'IMREPORTTYPE' AND GEN1.CONSTANTVALUE = H.REPORTTYPE AND Upper(GEN1.LANGUAGECODE) = 'EN-US'
LEFT JOIN GENCONSTANT GEN2 ON GEN2.CATEGORY = 'IMREPORTSTATUS' AND GEN2.CONSTANTVALUE = H.REPORTSTATUS AND Upper(GEN2.LANGUAGECODE) = 'EN-US'

