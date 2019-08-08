Policy Category Pop up Master :

SELECT * FROM (SELECT IP.*,GRO.GROUP_NAME GROUPNAME,POL.POLICYID ,POL.POLICYNAME,PLA.PLAN_NAME PLANNAME,
GC.CATEGORY_NAME CATEGORYNAME,II.INS_NAME FRONTINGSETTINGNAME, (MEM.FIRST_NAME||' '||MEM.LAST_NAME)  MEMBERNAME,
MEM.MEMBER_ID MEMBERID,MEM.ARABIC_FIRST_NAME ALTERNATENAME,'' LOADCOUNTRYSTATECITYBUTTON,LOK1.LOOKUPNAME ROOMTYPENAME ,
GEN1.CONSTANTNAME EMGDETECTWINETWTYPENAME,GEN2.CONSTANTNAME EMGDETECTOONETWTYPENAME,UB.UBIPID,'' RECOMMENDEDUBPID  ,
GEN3.CONSTANTNAME LIMITTYPENAME ,GEN4.CONSTANTNAME COINTYPENAME,GEN8.CONSTANTNAME OONDEDECTABLETYPENAME,
GEN7.CONSTANTNAME OONCOINTYPENAME,GEN9.CONSTANTNAME DEDECTABLETYPENAME,PRT.REPORT_NAME REPORTTEMPLATENAME,
TEMP.TEMPLATE_NAME_EN ELEMENTTEMPLATENAME,'' AS CARDTEXTAREA,'' AS CARDTEXT FROM IM_POLICY_CATEGORYDTL IP   
LEFT JOIN IM_PLANS PLA ON PLA.PLAN_CODE=IP.PLANCODE LEFT 
JOIN IM_CATEGORIES GC ON GC.CATEGORY_CODE=IP.CATEGORYCODE 
LEFT JOIN IM_INSURER II ON II.INSCODE=IP.FRONTINGSETTING 
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE=IP.POLICYCODE 
LEFT JOIN ADMLOOKUPDETAIL LOK1 ON LOK1.LOOKUPDETAILCODE=IP.ROOMTYPE  
LEFT JOIN IM_GROUPS GRO ON GRO.GROUP_CODE=IP.GROUPCODE 
LEFT JOIN IM_MEMBERS MEM ON MEM.MEMBER_CODE=IP.MEMBERCODE 
LEFT JOIN GENCONSTANT GEN1 ON GEN1.CONSTANTVALUE=IP.EMGDETECTWINETWTYPE AND GEN1.CATEGORY='CALCMETHOD' 
AND Upper(GEN1.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN2 ON GEN2.CONSTANTVALUE=IP.EMGDETECTOONETWTYPE AND GEN2.CATEGORY='CALCMETHOD' AND 
Upper(GEN2.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN3 ON GEN3.CONSTANTVALUE=IP.LIMITTYPE  AND GEN3.CATEGORY='CALCMETHOD' AND 
Upper(GEN3.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN4 ON GEN4.CONSTANTVALUE=IP.CO_IN_TYPE AND GEN4.CATEGORY='CALCMETHOD' AND 
Upper(GEN4.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN8 ON GEN8.CONSTANTVALUE=IP.OONDEDECTABLETYPE  AND GEN8.CATEGORY='CALCMETHOD' AND 
Upper(GEN8.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN7 ON GEN7.CONSTANTVALUE=IP.OONCO_IN_TYPE AND GEN7.CATEGORY='CALCMETHOD' AND 
Upper(GEN7.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN GENCONSTANT GEN9 ON GEN9.CONSTANTVALUE=IP.DEDECTABLETYPE  AND GEN9.CATEGORY='CALCMETHOD' AND 
Upper(GEN9.LANGUAGECODE)=Upper('EN-US') 
LEFT JOIN IM_UBIP UB ON UB.UBIPCODE=IP.UBIPCODE 
LEFT JOIN PM_REPORT_TEMPLATE PRT ON  PRT.REPORT_TEMPLATE_CODE=IP.REPORTTEMPLATECODE  
LEFT JOIN IM_ELEMENTTEMPLATE TEMP ON TEMP.ELEMENTTEMPLATECODE =IP.ELEMENTTEMPLATECODE);

Network Area Detail:

SELECT PCN.*,NET.NETWORKNAME,NET.NETWORKID,GEN.CONSTANTNAME APPLICABLEFORNAME FROM IM_POLICYCOVEREDNETWORK PCN 
LEFT JOIN IM_NETWORKS NET ON NET.NETWORKCODE=PCN.NETWORKCODE  
LEFT JOIN GENCONSTANT GEN ON GEN.CONSTANTVALUE = PCN.APPLICABLEFORCODE AND GEN.CATEGORY='APPLICABLE' AND 
Upper(GEN.LANGUAGECODE)=Upper('EN-US'); 

Geographic Detail : 

SELECT AR.*,IGA.GANAME,IGA.GAID FROM IM_PEGEOGRAFICAREA AR  
LEFT JOIN IM_GEOGRAPHIC_AREAS IGA ON IGA.GACODE=AR.GACODE;


Country Subdetail : 

SELECT PEC.*,GEN.COUNTRYNAME FROM IM_POLICYEXCLUDEDCOUNTRY PEC 
LEFT JOIN GENCOUNTRY GEN ON GEN.COUNTRYCODE=PEC.COUNTRYCODE;

State Subdetail :

SELECT pes.*,GPR.PROVINCENAME  FROM IM_POLICYEXCLUDEDSTATES pes 
left join  genprovince gpr on GPR.PROVINCECODE=pes.PROVINCECODE;


Benefit Detail : 

SELECT QRBEN.*,BEN.BENEFIT_ID BENEFITID, BEN.BENEFIT_NAME BENEFITNAME,GEN.CONSTANTNAME TYPENAME,  
(SELECT BEN1.BENEFIT_NAME BENEFITNAME FROM IIM_BENEFIT_CODES_DET SUBDET 
LEFT JOIN IM_BENEFIT_CODES BEN1 ON BEN1.BENEFIT_CODE=SUBDET.BENEFIT_CODE 
WHERE SUBDET.SUB_BENEFIT_CODE= QRBEN.BENEFIT_CODE AND ROWNUM=1)PARENTNAME FROM IM_POLICYBEBEFITS QRBEN 
LEFT JOIN IM_BENEFIT_CODES BEN ON BEN.BENEFIT_CODE=QRBEN.BENEFIT_CODE 
LEFT JOIN (SELECT CONSTANTVALUE,CONSTANTNAME FROM GENCONSTANT WHERE CATEGORY='NGIBENEFITSTYPE' AND Upper(LANGUAGECODE)=Upper('EN-US'))GEN ON GEN.CONSTANTVALUE=QRBEN.BENEFIT_TYPE;


Excluded PRovider Detail :

SELECT PEP.*,IP.PROVIDERNAME FROM IM_POLICYEXCLUDEDPROVIDER PEP 
LEFT JOIN IM_PROVIDERS IP ON IP.PROVIDERCODE=PEP.PROVIDERCODE;

Included Provider Detail : 

SELECT PIP.*,IP.PROVIDERNAME FROM IM_POLICYINCLUDEPROVIDER PIP 
LEFT JOIN IM_PROVIDERS IP ON IP.PROVIDERCODE=PIP.PROVIDERCODE;

Exclusion Detail :

SELECT EX.*,EXGRO.EXC_GROUP_NAME EXCLUTIONGROUPNAME FROM IM_POLICY_EXCLUSIONDTL EX   
LEFT JOIN IM_EXCLUSIONS_GROUPS_MASTER  EXGRO ON EXGRO.EXC_GRP_CODE=EX.EXC_GROUP_CODE;