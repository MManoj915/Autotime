Policy TYPE:
SELECT * FROM GENCONSTANT WHERE CATEGORY = 'MEMBERPOLICYTYPE' AND LANGUAGECODE = 'en-US';

Policy ID:
SELECT * FROM(SELECT  PO.POLICYCODE POLICYCODE,PO.POLICYID, 
PO.POLICYNAME,PO.CATEGORYCODE,1 AS TYPECODE  FROM IM_POLICY PO WHERE PO.AUTHORIZEDSTATUS=1   
UNION ALL 
SELECT IPO.INDIVIDUALPOLICYCODE POLICYCODE,IPO.POLICYID,
IPO.POLICYNAME,IPO.CATEGORYCODE,2 AS TYPECODE FROM IM_INDIVIDUALPOLICY IPO  WHERE  IPO.AUTHORIZEDSTATUS=1)
WHERE TYPECODE = 1;

Member ID:
SELECT MEM.* FROM (SELECT MEM.MEMBER_CODE MEMBERCODE,MEM.MEMBER_ID MEMBERID,
(MEM.FIRST_NAME||' '||MEM.LAST_NAME) MEMBER_NAME,MEM.ARABIC_FIRST_NAME ALTERNATENAME, 
MEM.CATEGORY_CODE,MEM.RELATION,MEM.POLICYCODE,MEM.MEMBERPOLICYCODE,
MEM.POLICYTYPE ,BRODTL.ACCOUNTNUMBER ACCOUNTNO,BRODTL.PINCODE ,
BRODTL.CURRENCYCODE,GEC.CURRENCYNAME CURRENCYNAME,MEM.HAADFINE FINEAMOUNT,MEM.WAIVEAMOUNT,
MEM.MEMBER_TYPE FROM IM_MEMBERS MEM JOIN ADMCOMPANY ADM ON ADM.COMPANYCODE=MEM.COMPANYCODE 
AND MEM.COMPANYCODE = 1  
LEFT JOIN IM_MEMBER_BILLING_DETAIL BRODTL ON BRODTL.MEMBER_CODE=MEM.MEMBER_CODE AND BRODTL.ENDDATE IS NULL  
LEFT JOIN GENCURRENCY GEC ON GEC.CURRENCYCODE=BRODTL.CURRENCYCODE) MEM WHERE MEM.POLICYCODE=100000000000019663;

Category Code:
SELECT CAT.* FROM (SELECT CAT.CATEGORY_CODE, CAT.CATEGORY_NAME FROM IM_CATEGORIES CAT)CAT;

Report TYPE:
SELECT * FROM GENCONSTANT WHERE CATEGORY = 'CARDPRINTREPORTTYPE' AND LANGUAGECODE = 'en-US';

Status:
SELECT * FROM GENCONSTANT WHERE CATEGORY = 'CUSSTATUS' AND LANGUAGECODE = 'en-US';

Detail:
SELECT CP.*,(IM.FIRST_NAME||' '||IM.LAST_NAME) MEMBERNAME,IM.MEMBER_ID MEMBERID,GEN4.CONSTANTNAME GENDERNAME,
GEN5.CONSTANTNAME RELATIONNAME,G4.CONSTANTNAME MARITAL_STATUSNAME,'' SELECTT,0 AS SELECTCH,GEN.CONSTANTNAME AS STATUSNAME,
IM.STATUS,NVL2(IM.PICTURE,1,0) AS ISPICTURE,IM.PICTURE,IG.GROUP_NAME,SUB.GROUP_NAME AS SUBGROUPNAME  FROM IM_CORDPRINT CP  
LEFT JOIN IM_MEMBERS IM ON IM.MEMBER_CODE=CP.MEMBERCODE  
LEFT  JOIN IM_GROUPS IG ON IG.GROUP_CODE=IM.GROUP_CODE  
LEFT  JOIN IM_GROUPS SUB ON SUB.GROUP_CODE=IM.PARENTGROUPCODE  
LEFT JOIN GENCONSTANT GEN4 ON GEN4.CONSTANTVALUE=IM.GENDER AND GEN4.CATEGORY='GENDER' AND GEN4.LANGUAGECODE='en-US' 
LEFT JOIN GENCONSTANT GEN5 ON GEN5.CONSTANTVALUE=IM.RELATION  AND GEN5.CATEGORY='MEMBERRELATION' AND GEN5.LANGUAGECODE='en-US' 
LEFT  JOIN GENCONSTANT   G4 ON  G4.CATEGORY='MARITAL_STATUS' AND G4.CONSTANTVALUE=IM.MARITAL_STATUS AND G4.LANGUAGECODE='en-US'  
LEFT JOIN IM_POLICY POL ON POL.POLICYCODE=CP.POLICYCODE  
LEFT JOIN GENCONSTANT GEN ON GEN.CONSTANTVALUE=IM.STATUS AND GEN.CATEGORY='CUSSTATUS' AND GEN.LANGUAGECODE='en-US'  
WHERE CP.AUTHORIZEDSTATUS=1  AND  CP.POLICYCODE=100000000000019663 AND  GEN.CONSTANTVALUE=0 AND  
IM.GROUP_CODE IS NOT NULL AND CP.POLICYTYPE=1 AND IM.MEMBER_CODE=10000000000215310 AND CP.CATEGORYCODE=1 
ORDER BY  CP.CATEGORYCODE,NVL(IM.PARENT_ID,IM.MEMBER_CODE),IM.MEMBER_ID ASC

