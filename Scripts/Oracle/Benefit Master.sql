SELECT BEN.*,IMP.POCKETID,IMP.POCKETNAME,GEN.CONSTANTNAME STATUSNAME,GEN1.CONSTANTNAME CALCULATIONMETHODNAME ,
GEN2.CONSTANTNAME APPLICABLENAME,UB.UBIPNAME UBIPNAME FROM IM_BENEFIT_CODES BEN  
LEFT JOIN GENCONSTANT GEN1 ON GEN1.CONSTANTVALUE=BEN.CALCULATIONMETHOD  AND GEN1.CATEGORY='VALUETYPE' AND 
GEN1.LANGUAGECODE='en-US'  
LEFT JOIN GENCONSTANT GEN2 ON GEN2.CONSTANTVALUE=BEN.APPLICABLE  AND GEN2.CATEGORY='APPLICABLE' AND 
GEN2.LANGUAGECODE='en-US'  
LEFT JOIN GENCONSTANT GEN ON GEN.CONSTANTVALUE=BEN.BENEFIT_STATUS  AND GEN.CATEGORY='PROVIDERSTATUS' AND 
GEN.LANGUAGECODE='en-US' 
JOIN ADMCOMPANY ADMCOM ON ADMCOM.COMPANYCODE=BEN.COMPANYCODE AND ADMCOM.COMPANYCODE= 1  
LEFT JOIN IM_UBIP UB ON UB.UBIPCODE=BEN.UBIPCODE  
LEFT JOIN IM_POCKET_MASTER IMP ON IMP.POCKETCODE = BEN.POCKETCODE  ORDER BY BEN.BENEFIT_CODE DESC;

DETAIL :

SELECT DET.*,BEN.BENEFIT_ID BENEFITID,BEN.BENEFIT_NAME BENEFITNAME FROM IIM_BENEFIT_CODES_DET DET  
LEFT JOIN IM_BENEFIT_CODES BEN ON BEN.BENEFIT_CODE=DET.SUB_BENEFIT_CODE WHERE DET.BENEFIT_CODE=1