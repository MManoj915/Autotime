MASTER :

SELECT MED.*, GEN.CONSTANTNAME AUTHORIZEDSTATUSNAME, EXT.CONSTANTNAME EXTRACTIONTYPENAME,
'' UPLOADEXCEL,'' VIEWEXCEL,'' VIEWINTEXT,0 TOTALRISHARE,0 TOTALAMOUNT, AUSE.US_NAME A_CREATEDBY, 
AUSE1.US_NAME A_LASTMODIFIEDBY FROM IM_DATAWAREHOUSE_EXT  MED   
LEFT JOIN GENCONSTANT EXT ON EXT.CONSTANTVALUE=MED.EXTRACTIONTYPE AND EXT.CATEGORY='EXTRACTIONTYPE' AND 
Upper(EXT.LANGUAGECODE)='EN-US'   
JOIN (SELECT CONSTANTVALUE,CONSTANTNAME FROM GENCONSTANT WHERE CATEGORY='AUTHORIZEDSTATUS' AND 
Upper(LANGUAGECODE)='EN-US')GEN ON GEN.CONSTANTVALUE=MED.AUTHORIZEDSTATUS  
LEFT JOIN FND_USER AUSE ON AUSE.US_ID=MED.CREATEDBY  
LEFT JOIN FND_USER AUSE1 ON AUSE1.US_ID=MED.LASTMODIFIEDBY   
ORDER BY MED.DATAWAREHOUSE_EXT_CODE DESC
     



DETAIL :

SELECT *  FROM IM_DATAWAREHOUSE_DTL  WHERE DATAWAREHOUSE_EXT_CODE=100000000000002639